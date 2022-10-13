using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Runtime.CompilerServices;

namespace RelEcs
{
    public sealed class Entities
    {
        internal EntityMeta[] Metas = Array.Empty<EntityMeta>();

        internal int EntityCount;

        internal readonly Queue<Identity> UnusedIds = new();

        internal readonly Dictionary<StorageType, IStorage> Storages = new();

        internal readonly Dictionary<int, Query> Queries = new();

        readonly Dictionary<StorageType, List<Query>> _queriesByType = new();

        readonly List<DelayedOperation> _delayedOperations = new();

        readonly Dictionary<Type, Entity> _typeEntities = new();

        int _lockCount;
        bool _isLocked;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Entity Spawn()
        {
            var identity = UnusedIds.Count > 0 ? UnusedIds.Dequeue() : new Identity(++EntityCount);

            if (Metas.Length <= EntityCount)
            {
                Array.Resize(ref Metas, EntityCount << 1);
                foreach (var storage in Storages.Values)
                {
                    storage.Resize(EntityCount << 1);
                }
            }

            Metas[identity.Number] = new EntityMeta
            {
                Identity = identity,
                Components = new SortedSet<StorageType>(),
            };

            var entity = new Entity(identity);

            AddComponent(StorageType.Create<Entity>(), identity, entity);

            return entity;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Despawn(Identity identity)
        {
            if (!IsAlive(identity)) return;

            if (_isLocked)
            {
                _delayedOperations.Add(new DelayedOperation { Despawn = true, Identity = identity });
                return;
            }

            ref var meta = ref Metas[identity.Number];

            var list = new List<StorageType>(meta.Components);
            foreach (var type in list)
            {
                RemoveComponent(type, identity);
            }

            meta.Components.Clear();
            meta.Identity = Identity.None;

            UnusedIds.Enqueue(identity);
            EntityCount--;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void AddComponent(StorageType type, Identity identity, object data = default)
        {
            ref var meta = ref Metas[identity.Number];

            if (meta.Components.Contains(type)) throw new Exception($"Entity already has component of type {type}");

            if (_isLocked)
            {
                _delayedOperations.Add(new DelayedOperation
                {
                    Add = true, Identity = identity, Type = type, Data = data
                });

                return;
            }

            meta.Components.Add(type);
            OnComponentAdded(type, identity);

            if (type.IsTag) return;

            var storage = GetStorage(type);
            storage.AddRaw(identity.Number, data);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public object GetComponent(StorageType type, Identity identity)
        {
            var storage = GetStorage(type);
            return storage.GetRaw(identity.Number);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool HasComponent(StorageType type, Identity identity)
        {
            ref var meta = ref Metas[identity.Number];
            return meta.Components.Contains(type);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void RemoveComponent(StorageType type, Identity identity)
        {
            if (_isLocked)
            {
                _delayedOperations.Add(new DelayedOperation { Add = false, Identity = identity, Type = type });
                return;
            }

            ref var meta = ref Metas[identity.Number];
            meta.Components.Remove(type);
            OnComponentRemoved(type, identity);

            if (type.IsTag) return;

            var storage = GetStorage(type);
            storage.Remove(identity.Number);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        void OnComponentAdded(StorageType type, Identity identity)
        {
            if (!_queriesByType.TryGetValue(type, out var queries)) return;

            foreach (var query in queries)
            {
                if (IsMaskCompatibleWith(query.Mask, identity))
                {
                    query.AddIdentity(identity);
                }
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        void OnComponentRemoved(StorageType type, Identity identity)
        {
            if (!_queriesByType.TryGetValue(type, out var queries)) return;

            foreach (var query in queries)
            {
                query.RemoveIdentity(identity);
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Storage<T> GetStorage<T>() where T : class
        {
            var type = StorageType.Create<T>();
            return (Storage<T>)GetStorage(type);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IStorage GetStorage(StorageType type)
        {
            if (Storages.TryGetValue(type, out var storage)) return storage;

            var st = typeof(Storage<>);
            var stg = st.MakeGenericType(type.Type);

            storage = (IStorage)Activator.CreateInstance(stg);
            storage.Type = type;
            storage.Resize(EntityCount << 1);

            Storages.Add(type, storage);

            return storage;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Query GetQuery(Mask mask, Func<Entities, Mask, List<Identity>, Query> createQuery)
        {
            var hash = mask.GetHashCode();

            if (Queries.TryGetValue(hash, out var query)) return query;

            var matchingEntities = new List<Identity>();

            foreach (var meta in Metas)
            {
                if (!IsAlive(meta.Identity)) continue;
                if (!IsMaskCompatibleWith(mask, meta.Identity)) continue;
                matchingEntities.Add(meta.Identity);
            }

            query = createQuery(this, mask, matchingEntities);
            Queries.Add(hash, query);

            foreach (var type in mask.HasTypes)
            {
                if (!_queriesByType.TryGetValue(type, out var list))
                {
                    Console.WriteLine($"Query by Type: {type}");
                    list = new List<Query>();
                    _queriesByType.Add(type, list);
                }

                list.Add(query);
            }

            return query;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal bool IsMaskCompatibleWith(Mask mask, Identity identity)
        {
            var meta = Metas[identity.Number];
            var has = mask.HasTypes.IsSubsetOf(meta.Components);
            var not = mask.NotTypes.Count == 0 || !mask.NotTypes.Overlaps(meta.Components);
            var any = mask.AnyTypes.Count == 0 || mask.AnyTypes.Overlaps(meta.Components);
            Console.WriteLine($"{has}, {not}, {any}");
            var compatible = any && has && not;
            return compatible;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal bool IsAlive(Identity identity)
        {
            return Metas[identity.Number].Identity != Identity.None;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal ref EntityMeta GetEntityMeta(Identity identity)
        {
            return ref Metas[identity.Number];
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal Entity GetTarget(StorageType type, Identity identity)
        {
            // var meta = Metas[identity.Number];
            // var table = Tables[meta.TableId];
            //
            // foreach (var storageType in table.Types)
            // {
            //     if (!storageType.IsRelation || storageType.TypeId != type.TypeId) continue;
            //     return new Entity(storageType.Identity);
            // }

            return Entity.None;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal Entity[] GetTargets(StorageType type, Identity identity)
        {
            // var meta = Metas[identity.Number];
            // var table = Tables[meta.TableId];
            //
            // var list = ListPool<Entity>.Get();
            //
            // foreach (var storageType in table.Types)
            // {
            //     if (!storageType.IsRelation || storageType.TypeId != type.TypeId) continue;
            //     list.Add(new Entity(storageType.Identity));
            // }
            //
            // var targetEntities = list.ToArray();
            // ListPool<Entity>.Add(list);

            return Array.Empty<Entity>();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal (StorageType, object)[] GetComponents(Identity identity)
        {
            var meta = Metas[identity.Number];
            var list = new List<(StorageType, object)>();

            foreach (var type in meta.Components)
            {
                list.Add((type, GetStorage(type)));
            }

            return list.ToArray();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal Entity GetTypeEntity(Type type)
        {
            if (_typeEntities.TryGetValue(type, out var entity)) return entity;

            entity = Spawn();
            _typeEntities.Add(type, entity);

            return entity;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        void ApplyDelayedOperations()
        {
            foreach (var op in _delayedOperations)
            {
                if (!IsAlive(op.Identity)) continue;

                if (op.Despawn) Despawn(op.Identity);
                else if (op.Add) AddComponent(op.Type, op.Identity, op.Data);
                else RemoveComponent(op.Type, op.Identity);
            }

            _delayedOperations.Clear();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Lock()
        {
            _lockCount++;
            _isLocked = true;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Unlock()
        {
            _lockCount--;
            if (_lockCount != 0) return;
            _isLocked = false;

            ApplyDelayedOperations();
        }

        struct DelayedOperation
        {
            public bool Despawn;
            public bool Add;
            public StorageType Type;
            public Identity Identity;
            public object Data;
        }
    }
}