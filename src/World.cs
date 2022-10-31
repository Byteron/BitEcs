using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace RelEcs
{
    public sealed class World
    {
        static int worldCount;

        readonly Entity _world;
        readonly WorldInfo _worldInfo;

        readonly Archetypes _archetypes = new();

        internal readonly List<(Type, TimeSpan)> SystemExecutionTimes = new();

        readonly TriggerLifeTimeSystem _triggerLifeTimeSystem = new();

        public WorldInfo Info => _worldInfo;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public World()
        {
            _world = _archetypes.Spawn();
            _worldInfo = new WorldInfo(++worldCount);
            _archetypes.AddComponent(StorageType.Create<WorldInfo>(Identity.None), _world.Identity, _worldInfo);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public EntityBuilder Spawn()
        {
            return new EntityBuilder(this, _archetypes.Spawn());
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public EntityBuilder On(Entity entity)
        {
            return new EntityBuilder(this, entity);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Despawn(Entity entity)
        {
            _archetypes.Despawn(entity.Identity);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void DespawnAllWith<T>() where T : class
        {
            var query = QueryBuilder<Entity>().Has<T>().Build();
            foreach (var entity in query) Despawn(entity);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool IsAlive(Entity entity)
        {
            return entity is not null && _archetypes.IsAlive(entity.Identity);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public T GetComponent<T>(Entity entity) where T : class
        {
            var type = StorageType.Create<T>(Identity.None);
            return (T)_archetypes.GetComponent(type, entity.Identity);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool TryGetComponent<T>(Entity entity, out T component) where T : class
        {
            var type = StorageType.Create<T>(Identity.None);
            if (!HasComponent<T>(entity))
            {
                component = null;
                return false;
            }

            component = (T)_archetypes.GetComponent(type, entity.Identity);
            return true;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool HasComponent<T>(Entity entity) where T : class
        {
            var type = StorageType.Create<T>(Identity.None);
            return _archetypes.HasComponent(type, entity.Identity);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void AddComponent<T>(Entity entity) where T : class
        {
            var type = StorageType.Create<T>(Identity.None);
            if (!type.IsTag) throw new Exception($"{type} is tag component, use Add(new Component()) instead");
            _archetypes.AddComponent(type, entity.Identity);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void AddComponent<T>(Entity entity, T component) where T : class
        {
            var type = StorageType.Create<T>(Identity.None);
            if (type.IsTag) throw new Exception($"{type} is tag component, Add<T>() instead");
            _archetypes.AddComponent(type, entity.Identity, component);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void RemoveComponent<T>(Entity entity) where T : class
        {
            var type = StorageType.Create<T>(Identity.None);
            _archetypes.RemoveComponent(type, entity.Identity);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IEnumerable<(StorageType, object)> GetComponents(Entity entity)
        {
            return _archetypes.GetComponents(entity.Identity);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public T GetComponent<T>(Entity entity, Entity target) where T : class
        {
            var type = StorageType.Create<T>(target.Identity);
            return (T)_archetypes.GetComponent(type, entity.Identity);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool TryGetComponent<T>(Entity entity, out T component, Entity target) where T : class
        {
            var type = StorageType.Create<T>(target.Identity);
            if (!HasComponent<T>(entity))
            {
                component = null;
                return false;
            }

            component = (T)_archetypes.GetComponent(type, entity.Identity);
            return true;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool HasComponent<T>(Entity entity, Entity target) where T : class
        {
            var type = StorageType.Create<T>(target.Identity);
            return _archetypes.HasComponent(type, entity.Identity);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void AddComponent<T>(Entity entity, Entity target) where T : class
        {
            var type = StorageType.Create<T>(target.Identity);
            if (!type.IsTag) throw new Exception();
            _archetypes.AddComponent(type, entity.Identity);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void AddComponent<T>(Entity entity, T component, Entity target) where T : class
        {
            var type = StorageType.Create<T>(target.Identity);
            if (type.IsTag) throw new Exception();
            _archetypes.AddComponent(type, entity.Identity, component);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void RemoveComponent<T>(Entity entity, Entity target) where T : class
        {
            var type = StorageType.Create<T>(target.Identity);
            _archetypes.RemoveComponent(type, entity.Identity);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Entity GetTarget<T>(Entity entity) where T : class
        {
            var type = StorageType.Create<T>(Identity.None);
            return _archetypes.GetTarget(type, entity.Identity);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IEnumerable<Entity> GetTargets<T>(Entity entity) where T : class
        {
            var type = StorageType.Create<T>(Identity.None);
            return _archetypes.GetTargets(type, entity.Identity);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public T GetElement<T>() where T : class
        {
            var type = StorageType.Create<T>(Identity.None);
            return (T)_archetypes.GetComponent(type, _world.Identity);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool TryGetElement<T>(out T element) where T : class
        {
            var type = StorageType.Create<T>(Identity.None);
            if (!HasElement<T>())
            {
                element = null;
                return false;
            }

            element = (T)_archetypes.GetComponent(type, _world.Identity);
            return true;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool HasElement<T>() where T : class
        {
            var type = StorageType.Create<T>(Identity.None);
            return _archetypes.HasComponent(type, _world.Identity);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void AddElement<T>(T element) where T : class
        {
            var type = StorageType.Create<T>(Identity.None);
            if (type.IsTag) throw new Exception();
            _archetypes.AddComponent(type, _world.Identity, element);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void ReplaceElement<T>(T element) where T : class
        {
            var type = StorageType.Create<T>(Identity.None);
            _archetypes.RemoveComponent(type, _world.Identity);
            _archetypes.AddComponent(type, _world.Identity, element);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void AddOrReplaceElement<T>(T element) where T : class
        {
            var type = StorageType.Create<T>(Identity.None);

            if (_archetypes.HasComponent(type, _world.Identity))
            {
                _archetypes.RemoveComponent(type, _world.Identity);
            }

            _archetypes.AddComponent(type, _world.Identity, element);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void RemoveElement<T>() where T : class
        {
            var type = StorageType.Create<T>(Identity.None);
            _archetypes.RemoveComponent(type, _world.Identity);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Send<T>(T trigger) where T : class
        {
            if (trigger is null) throw new Exception("trigger cannot be null");
        
            var entity = _archetypes.Spawn();
            _archetypes.AddComponent(StorageType.Create<SystemList>(Identity.None), entity.Identity, new SystemList());
            _archetypes.AddComponent(StorageType.Create<LifeTime>(Identity.None), entity.Identity, new LifeTime());
            _archetypes.AddComponent(StorageType.Create<Trigger<T>>(Identity.None), entity.Identity,
                new Trigger<T> { Value = trigger });
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public TriggerQuery<T> Receive<T>(ISystem system) where T : class
        {
            var mask = new Mask();
        
            mask.Has(StorageType.Create<Trigger<T>>(Identity.None));
            mask.Has(StorageType.Create<SystemList>(Identity.None));
        
            var matchingTables = new List<Table>();
        
            var type = mask.HasTypes[0];
            if (!_archetypes.TablesByType.TryGetValue(type, out var typeTables))
            {
                typeTables = new List<Table>();
                _archetypes.TablesByType[type] = typeTables;
            }
        
            foreach (var table in typeTables)
            {
                if (!_archetypes.IsMaskCompatibleWith(mask, table)) continue;
        
                matchingTables.Add(table);
            }
        
            return new TriggerQuery<T>(_archetypes, mask, matchingTables, system.GetType());
        }
        
        public sealed class NoFilter { }

        public NewQuery<Q, NoFilter> NewQuery<Q>()
        {
            return NewQuery<Q, NoFilter>();
        }
        
        public NewQuery<Q, F> NewQuery<Q, F>()
        {
            var q = typeof(Q);
            var types = new List<StorageType>();
            
            for (var i = 1; i <= 8; i++)
            {
                if (q.Name != $"ValueTuple`{i}") continue;
                
                foreach(var type in q.GetGenericArguments())
                {
                    types.Add(StorageType.CreateFromType(type, Identity.None));
                }
                
                break;
            }

            var mask = new Mask();

            foreach (var type in types)
            {
                mask.Has(type);
            }
            
            return _archetypes.GetNewQuery<Q, F>(mask);
        }
        
        public Query<Entity> Query()
        {
            return new QueryBuilder<Entity>(_archetypes).Build();
        }

        public Query<C> Query<C>() where C : class
        {
            return new QueryBuilder<C>(_archetypes).Build();
        }

        public Query<C1, C2> Query<C1, C2>() where C1 : class where C2 : class
        {
            return new QueryBuilder<C1, C2>(_archetypes).Build();
        }

        public Query<C1, C2, C3> Query<C1, C2, C3>() where C1 : class where C2 : class where C3 : class
        {
            return new QueryBuilder<C1, C2, C3>(_archetypes).Build();
        }

        public Query<C1, C2, C3, C4> Query<C1, C2, C3, C4>()
            where C1 : class where C2 : class where C3 : class where C4 : class
        {
            return new QueryBuilder<C1, C2, C3, C4>(_archetypes).Build();
        }

        public Query<C1, C2, C3, C4, C5> Query<C1, C2, C3, C4, C5>() where C1 : class
            where C2 : class
            where C3 : class
            where C4 : class
            where C5 : class
        {
            return new QueryBuilder<C1, C2, C3, C4, C5>(_archetypes).Build();
        }

        public Query<C1, C2, C3, C4, C5, C6> Query<C1, C2, C3, C4, C5, C6>() where C1 : class
            where C2 : class
            where C3 : class
            where C4 : class
            where C5 : class
            where C6 : class
        {
            return new QueryBuilder<C1, C2, C3, C4, C5, C6>(_archetypes).Build();
        }

        public Query<C1, C2, C3, C4, C5, C6, C7> Query<C1, C2, C3, C4, C5, C6, C7>() where C1 : class
            where C2 : class
            where C3 : class
            where C4 : class
            where C5 : class
            where C6 : class
            where C7 : class
        {
            return new QueryBuilder<C1, C2, C3, C4, C5, C6, C7>(_archetypes).Build();
        }

        public Query<C1, C2, C3, C4, C5, C6, C7, C8> Query<C1, C2, C3, C4, C5, C6, C7, C8>() where C1 : class
            where C2 : class
            where C3 : class
            where C4 : class
            where C5 : class
            where C6 : class
            where C7 : class
            where C8 : class
        {
            return new QueryBuilder<C1, C2, C3, C4, C5, C6, C7, C8>(_archetypes).Build();
        }

        public QueryBuilder<Entity> QueryBuilder()
        {
            return new QueryBuilder<Entity>(_archetypes);
        }

        public QueryBuilder<C> QueryBuilder<C>() where C : class
        {
            return new QueryBuilder<C>(_archetypes);
        }

        public QueryBuilder<C1, C2> QueryBuilder<C1, C2>() where C1 : class where C2 : class
        {
            return new QueryBuilder<C1, C2>(_archetypes);
        }

        public QueryBuilder<C1, C2, C3> QueryBuilder<C1, C2, C3>() where C1 : class where C2 : class where C3 : class
        {
            return new QueryBuilder<C1, C2, C3>(_archetypes);
        }

        public QueryBuilder<C1, C2, C3, C4> QueryBuilder<C1, C2, C3, C4>() where C1 : class
            where C2 : class
            where C3 : class
            where C4 : class
        {
            return new QueryBuilder<C1, C2, C3, C4>(_archetypes);
        }

        public QueryBuilder<C1, C2, C3, C4, C5> QueryBuilder<C1, C2, C3, C4, C5>() where C1 : class
            where C2 : class
            where C3 : class
            where C4 : class
            where C5 : class
        {
            return new QueryBuilder<C1, C2, C3, C4, C5>(_archetypes);
        }

        public QueryBuilder<C1, C2, C3, C4, C5, C6> QueryBuilder<C1, C2, C3, C4, C5, C6>() where C1 : class
            where C2 : class
            where C3 : class
            where C4 : class
            where C5 : class
            where C6 : class
        {
            return new QueryBuilder<C1, C2, C3, C4, C5, C6>(_archetypes);
        }

        public QueryBuilder<C1, C2, C3, C4, C5, C6, C7> QueryBuilder<C1, C2, C3, C4, C5, C6, C7>() where C1 : class
            where C2 : class
            where C3 : class
            where C4 : class
            where C5 : class
            where C6 : class
            where C7 : class
        {
            return new QueryBuilder<C1, C2, C3, C4, C5, C6, C7>(_archetypes);
        }

        public QueryBuilder<C1, C2, C3, C4, C5, C6, C7, C8> QueryBuilder<C1, C2, C3, C4, C5, C6, C7, C8>()
            where C1 : class
            where C2 : class
            where C3 : class
            where C4 : class
            where C5 : class
            where C6 : class
            where C7 : class
            where C8 : class
        {
            return new QueryBuilder<C1, C2, C3, C4, C5, C6, C7, C8>(_archetypes);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Tick()
        {
            _worldInfo.EntityCount = _archetypes.EntityCount;
            _worldInfo.UnusedEntityCount = _archetypes.UnusedIds.Count;
            _worldInfo.AllocatedEntityCount = _archetypes.Meta.Length;
            _worldInfo.ArchetypeCount = _archetypes.Tables.Count;
            // info.RelationCount = relationCount;
            _worldInfo.ElementCount = _archetypes.Tables[_archetypes.Meta[_world.Identity.Id].TableId].Types.Count;
            _worldInfo.CachedQueryCount = _archetypes.Queries.Count;

            _worldInfo.SystemCount = SystemExecutionTimes.Count;

            _worldInfo.SystemExecutionTimes.Clear();
            _worldInfo.SystemExecutionTimes.AddRange(SystemExecutionTimes);

            _triggerLifeTimeSystem.Run(this);

            SystemExecutionTimes.Clear();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal Entity GetTypeEntity(Type type)
        {
            return _archetypes.GetTypeEntity(type);
        }
    }

    public sealed class WorldInfo
    {
        public readonly int WorldId;
        public int EntityCount;
        public int UnusedEntityCount;
        public int AllocatedEntityCount;

        public int ArchetypeCount;

        // public int RelationCount;
        public int ElementCount;
        public int SystemCount;
        public List<(Type, TimeSpan)> SystemExecutionTimes;
        public int CachedQueryCount;

        public WorldInfo(int id)
        {
            WorldId = id;
            SystemExecutionTimes = new List<(Type, TimeSpan)>();
        }
    }
}