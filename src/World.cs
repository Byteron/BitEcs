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

        readonly Entities _entities = new();

        internal readonly List<(Type, TimeSpan)> SystemExecutionTimes = new();

        public WorldInfo Info => _worldInfo;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public World()
        {
            _world = _entities.Spawn();
            _worldInfo = new WorldInfo(++worldCount);
            _entities.AddComponent(StorageType.Create<WorldInfo>(), _world.Identity, _worldInfo);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public EntityBuilder Spawn()
        {
            return new EntityBuilder(this, _entities.Spawn());
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public EntityBuilder On(Entity entity)
        {
            return new EntityBuilder(this, entity);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Despawn(Entity entity)
        {
            _entities.Despawn(entity.Identity);
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
            return entity is not null && _entities.IsAlive(entity.Identity);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public T GetComponent<T>(Entity entity) where T : class
        {
            var type = StorageType.Create<T>();
            return (T)_entities.GetComponent(type, entity.Identity);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool TryGetComponent<T>(Entity entity, out T component) where T : class
        {
            var type = StorageType.Create<T>();
            if (!HasComponent<T>(entity))
            {
                component = null;
                return false;
            }

            component = (T)_entities.GetComponent(type, entity.Identity);
            return true;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool HasComponent<T>(Entity entity) where T : class
        {
            var type = StorageType.Create<T>();
            return _entities.HasComponent(type, entity.Identity);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void AddComponent<T>(Entity entity) where T : class
        {
            var type = StorageType.Create<T>();
            if (!type.IsTag) throw new Exception($"{type} is tag component, use Add(new Component()) instead");
            _entities.AddComponent(type, entity.Identity);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void AddComponent<T>(Entity entity, T component) where T : class
        {
            var type = StorageType.Create<T>();
            if (type.IsTag) throw new Exception($"{type} is tag component, Add<T>() instead");
            _entities.AddComponent(type, entity.Identity, component);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void RemoveComponent<T>(Entity entity) where T : class
        {
            var type = StorageType.Create<T>();
            _entities.RemoveComponent(type, entity.Identity);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IEnumerable<(StorageType, object)> GetComponents(Entity entity)
        {
            return _entities.GetComponents(entity.Identity);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public T GetElement<T>() where T : class
        {
            var type = StorageType.Create<T>();
            return (T)_entities.GetComponent(type, _world.Identity);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool TryGetElement<T>(out T element) where T : class
        {
            var type = StorageType.Create<T>();
            if (!HasElement<T>())
            {
                element = null;
                return false;
            }

            element = (T)_entities.GetComponent(type, _world.Identity);
            return true;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool HasElement<T>() where T : class
        {
            var type = StorageType.Create<T>();
            return _entities.HasComponent(type, _world.Identity);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void AddElement<T>(T element) where T : class
        {
            var type = StorageType.Create<T>();
            if (type.IsTag) throw new Exception();
            _entities.AddComponent(type, _world.Identity, element);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void ReplaceElement<T>(T element) where T : class
        {
            var type = StorageType.Create<T>();
            _entities.RemoveComponent(type, _world.Identity);
            _entities.AddComponent(type, _world.Identity, element);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void AddOrReplaceElement<T>(T element) where T : class
        {
            var type = StorageType.Create<T>();

            if (_entities.HasComponent(type, _world.Identity))
            {
                _entities.RemoveComponent(type, _world.Identity);
            }

            _entities.AddComponent(type, _world.Identity, element);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void RemoveElement<T>() where T : class
        {
            var type = StorageType.Create<T>();
            _entities.RemoveComponent(type, _world.Identity);
        }

        public Query<Entity> Query()
        {
            return new QueryBuilder<Entity>(_entities).Build();
        }

        public Query<C> Query<C>() where C : class
        {
            return new QueryBuilder<C>(_entities).Build();
        }

        public Query<C1, C2> Query<C1, C2>() where C1 : class where C2 : class
        {
            return new QueryBuilder<C1, C2>(_entities).Build();
        }

        public Query<C1, C2, C3> Query<C1, C2, C3>() where C1 : class where C2 : class where C3 : class
        {
            return new QueryBuilder<C1, C2, C3>(_entities).Build();
        }

        public Query<C1, C2, C3, C4> Query<C1, C2, C3, C4>()
            where C1 : class where C2 : class where C3 : class where C4 : class
        {
            return new QueryBuilder<C1, C2, C3, C4>(_entities).Build();
        }

        public Query<C1, C2, C3, C4, C5> Query<C1, C2, C3, C4, C5>() where C1 : class
            where C2 : class
            where C3 : class
            where C4 : class
            where C5 : class
        {
            return new QueryBuilder<C1, C2, C3, C4, C5>(_entities).Build();
        }

        public Query<C1, C2, C3, C4, C5, C6> Query<C1, C2, C3, C4, C5, C6>() where C1 : class
            where C2 : class
            where C3 : class
            where C4 : class
            where C5 : class
            where C6 : class
        {
            return new QueryBuilder<C1, C2, C3, C4, C5, C6>(_entities).Build();
        }

        public Query<C1, C2, C3, C4, C5, C6, C7> Query<C1, C2, C3, C4, C5, C6, C7>() where C1 : class
            where C2 : class
            where C3 : class
            where C4 : class
            where C5 : class
            where C6 : class
            where C7 : class
        {
            return new QueryBuilder<C1, C2, C3, C4, C5, C6, C7>(_entities).Build();
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
            return new QueryBuilder<C1, C2, C3, C4, C5, C6, C7, C8>(_entities).Build();
        }

        public QueryBuilder<Entity> QueryBuilder()
        {
            return new QueryBuilder<Entity>(_entities);
        }

        public QueryBuilder<C> QueryBuilder<C>() where C : class
        {
            return new QueryBuilder<C>(_entities);
        }

        public QueryBuilder<C1, C2> QueryBuilder<C1, C2>() where C1 : class where C2 : class
        {
            return new QueryBuilder<C1, C2>(_entities);
        }

        public QueryBuilder<C1, C2, C3> QueryBuilder<C1, C2, C3>() where C1 : class where C2 : class where C3 : class
        {
            return new QueryBuilder<C1, C2, C3>(_entities);
        }

        public QueryBuilder<C1, C2, C3, C4> QueryBuilder<C1, C2, C3, C4>() where C1 : class
            where C2 : class
            where C3 : class
            where C4 : class
        {
            return new QueryBuilder<C1, C2, C3, C4>(_entities);
        }

        public QueryBuilder<C1, C2, C3, C4, C5> QueryBuilder<C1, C2, C3, C4, C5>() where C1 : class
            where C2 : class
            where C3 : class
            where C4 : class
            where C5 : class
        {
            return new QueryBuilder<C1, C2, C3, C4, C5>(_entities);
        }

        public QueryBuilder<C1, C2, C3, C4, C5, C6> QueryBuilder<C1, C2, C3, C4, C5, C6>() where C1 : class
            where C2 : class
            where C3 : class
            where C4 : class
            where C5 : class
            where C6 : class
        {
            return new QueryBuilder<C1, C2, C3, C4, C5, C6>(_entities);
        }

        public QueryBuilder<C1, C2, C3, C4, C5, C6, C7> QueryBuilder<C1, C2, C3, C4, C5, C6, C7>() where C1 : class
            where C2 : class
            where C3 : class
            where C4 : class
            where C5 : class
            where C6 : class
            where C7 : class
        {
            return new QueryBuilder<C1, C2, C3, C4, C5, C6, C7>(_entities);
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
            return new QueryBuilder<C1, C2, C3, C4, C5, C6, C7, C8>(_entities);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Tick()
        {
            _worldInfo.EntityCount = _entities.EntityCount;
            _worldInfo.UnusedEntityCount = _entities.UnusedIds.Count;
            _worldInfo.AllocatedEntityCount = _entities.Metas.Length;
            _worldInfo.ElementCount = _entities.Metas[_world.Identity.Number].Components.Count;
            _worldInfo.CachedQueryCount = _entities.Queries.Count;

            _worldInfo.SystemCount = SystemExecutionTimes.Count;

            _worldInfo.SystemExecutionTimes.Clear();
            _worldInfo.SystemExecutionTimes.AddRange(SystemExecutionTimes);

            SystemExecutionTimes.Clear();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal Entity GetTypeEntity(Type type)
        {
            return _entities.GetTypeEntity(type);
        }
    }

    public sealed class WorldInfo
    {
        public readonly int WorldId;
        public int EntityCount;
        public int UnusedEntityCount;
        public int AllocatedEntityCount;

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