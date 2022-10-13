using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace RelEcs
{
    public class Query
    {
        public readonly List<Identity> Identities;

        internal readonly Entities Entities;
        internal readonly Mask Mask;

        protected IStorage[] Storages = Array.Empty<IStorage>();

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Query(Entities entities, Mask mask, List<Identity> identities)
        {
            Identities = identities;
            Entities = entities;
            Mask = mask;

            UpdateStorages();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool Has(Entity entity)
        {
            return Identities.Contains(entity.Identity);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal void AddIdentity(Identity identity)
        {
            Identities.Add(identity);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal void RemoveIdentity(Identity identity)
        {
            Identities.Remove(identity);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected virtual IStorage[] GetStorages()
        {
            throw new Exception("Invalid Enumerator");
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        void UpdateStorages()
        {
            Storages = GetStorages();
        }
    }

    public class Query<C> : Query
        where C : class
    {
        public Query(Entities entities, Mask mask, List<Identity> identities) : base(entities, mask, identities)
        {
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected override IStorage[] GetStorages()
        {
            return new[] { Entities.GetStorage<C>() };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public C Get(Entity entity)
        {
            var meta = Entities.GetEntityMeta(entity.Identity);
            var storage = (Storage<C>)Storages[0];
            return storage.Get(meta.Identity.Number);
        }

        public Enumerator<C> GetEnumerator()
        {
            return new Enumerator<C>(Entities, Identities, Storages);
        }
    }

    public class Query<C1, C2> : Query
        where C1 : class
        where C2 : class
    {
        public Query(Entities entities, Mask mask, List<Identity> identities) : base(entities, mask, identities)
        {
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected override IStorage[] GetStorages()
        {
            return new IStorage[]
            {
                Entities.GetStorage<C1>(),
                Entities.GetStorage<C2>(),
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public (C1, C2) Get(Entity entity)
        {
            var meta = Entities.GetEntityMeta(entity.Identity);
            var storage1 = (Storage<C1>)Storages[0];
            var storage2 = (Storage<C2>)Storages[1];
            return (storage1.Get(meta.Identity.Number), storage2.Get(meta.Identity.Number));
        }

        public Enumerator<C1, C2> GetEnumerator()
        {
            return new Enumerator<C1, C2>(Entities, Identities, Storages);
        }
    }

    public class Query<C1, C2, C3> : Query
        where C1 : class
        where C2 : class
        where C3 : class
    {
        public Query(Entities entities, Mask mask, List<Identity> identities) : base(entities, mask, identities)
        {
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected override IStorage[] GetStorages()
        {
            return new IStorage[]
            {
                Entities.GetStorage<C1>(),
                Entities.GetStorage<C2>(),
                Entities.GetStorage<C3>(),
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public (C1, C2, C3) Get(Entity entity)
        {
            var meta = Entities.GetEntityMeta(entity.Identity);
            var storage1 = (Storage<C1>)Storages[0];
            var storage2 = (Storage<C2>)Storages[1];
            var storage3 = (Storage<C3>)Storages[2];
            return (storage1.Get(meta.Identity.Number), storage2.Get(meta.Identity.Number),
                storage3.Get(meta.Identity.Number));
        }

        public Enumerator<C1, C2, C3> GetEnumerator()
        {
            return new Enumerator<C1, C2, C3>(Entities, Identities, Storages);
        }
    }

    public class Query<C1, C2, C3, C4> : Query
        where C1 : class
        where C2 : class
        where C3 : class
        where C4 : class
    {
        public Query(Entities entities, Mask mask, List<Identity> identities) : base(entities, mask, identities)
        {
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected override IStorage[] GetStorages()
        {
            return new IStorage[]
            {
                Entities.GetStorage<C1>(),
                Entities.GetStorage<C2>(),
                Entities.GetStorage<C3>(),
                Entities.GetStorage<C4>(),
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public (C1, C2, C3, C4) Get(Entity entity)
        {
            var meta = Entities.GetEntityMeta(entity.Identity);
            var storage1 = (Storage<C1>)Storages[0];
            var storage2 = (Storage<C2>)Storages[1];
            var storage3 = (Storage<C3>)Storages[2];
            var storage4 = (Storage<C4>)Storages[3];
            return (storage1.Get(meta.Identity.Number), storage2.Get(meta.Identity.Number),
                storage3.Get(meta.Identity.Number),
                storage4.Get(meta.Identity.Number));
        }

        public Enumerator<C1, C2, C3, C4> GetEnumerator()
        {
            return new Enumerator<C1, C2, C3, C4>(Entities, Identities, Storages);
        }
    }

    public class Query<C1, C2, C3, C4, C5> : Query
        where C1 : class
        where C2 : class
        where C3 : class
        where C4 : class
        where C5 : class
    {
        public Query(Entities entities, Mask mask, List<Identity> identities) : base(entities, mask, identities)
        {
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected override IStorage[] GetStorages()
        {
            return new IStorage[]
            {
                Entities.GetStorage<C1>(),
                Entities.GetStorage<C2>(),
                Entities.GetStorage<C3>(),
                Entities.GetStorage<C4>(),
                Entities.GetStorage<C5>(),
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public (C1, C2, C3, C4, C5) Get(Entity entity)
        {
            var meta = Entities.GetEntityMeta(entity.Identity);
            var storage1 = (Storage<C1>)Storages[0];
            var storage2 = (Storage<C2>)Storages[1];
            var storage3 = (Storage<C3>)Storages[2];
            var storage4 = (Storage<C4>)Storages[3];
            var storage5 = (Storage<C5>)Storages[4];
            return (storage1.Get(meta.Identity.Number), storage2.Get(meta.Identity.Number),
                storage3.Get(meta.Identity.Number),
                storage4.Get(meta.Identity.Number), storage5.Get(meta.Identity.Number));
        }

        public Enumerator<C1, C2, C3, C4, C5> GetEnumerator()
        {
            return new Enumerator<C1, C2, C3, C4, C5>(Entities, Identities, Storages);
        }
    }

    public class Query<C1, C2, C3, C4, C5, C6> : Query
        where C1 : class
        where C2 : class
        where C3 : class
        where C4 : class
        where C5 : class
        where C6 : class
    {
        public Query(Entities entities, Mask mask, List<Identity> identities) : base(entities, mask, identities)
        {
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected override IStorage[] GetStorages()
        {
            return new IStorage[]
            {
                Entities.GetStorage<C2>(),
                Entities.GetStorage<C1>(),
                Entities.GetStorage<C3>(),
                Entities.GetStorage<C4>(),
                Entities.GetStorage<C5>(),
                Entities.GetStorage<C6>(),
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public (C1, C2, C3, C4, C5, C6) Get(Entity entity)
        {
            var meta = Entities.GetEntityMeta(entity.Identity);
            var storage1 = (Storage<C1>)Storages[0];
            var storage2 = (Storage<C2>)Storages[1];
            var storage3 = (Storage<C3>)Storages[2];
            var storage4 = (Storage<C4>)Storages[3];
            var storage5 = (Storage<C5>)Storages[4];
            var storage6 = (Storage<C6>)Storages[5];
            return (storage1.Get(meta.Identity.Number), storage2.Get(meta.Identity.Number),
                storage3.Get(meta.Identity.Number),
                storage4.Get(meta.Identity.Number), storage5.Get(meta.Identity.Number),
                storage6.Get(meta.Identity.Number));
        }

        public Enumerator<C1, C2, C3, C4, C5, C6> GetEnumerator()
        {
            return new Enumerator<C1, C2, C3, C4, C5, C6>(Entities, Identities, Storages);
        }
    }

    public class Query<C1, C2, C3, C4, C5, C6, C7> : Query
        where C1 : class
        where C2 : class
        where C3 : class
        where C4 : class
        where C5 : class
        where C6 : class
        where C7 : class
    {
        public Query(Entities entities, Mask mask, List<Identity> identities) : base(entities, mask, identities)
        {
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected override IStorage[] GetStorages()
        {
            return new IStorage[]
            {
                Entities.GetStorage<C1>(),
                Entities.GetStorage<C2>(),
                Entities.GetStorage<C3>(),
                Entities.GetStorage<C4>(),
                Entities.GetStorage<C5>(),
                Entities.GetStorage<C6>(),
                Entities.GetStorage<C7>(),
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public (C1, C2, C3, C4, C5, C6, C7) Get(Entity entity)
        {
            var meta = Entities.GetEntityMeta(entity.Identity);
            var storage1 = (Storage<C1>)Storages[0];
            var storage2 = (Storage<C2>)Storages[1];
            var storage3 = (Storage<C3>)Storages[2];
            var storage4 = (Storage<C4>)Storages[3];
            var storage5 = (Storage<C5>)Storages[4];
            var storage6 = (Storage<C6>)Storages[5];
            var storage7 = (Storage<C7>)Storages[6];
            return (storage1.Get(meta.Identity.Number), storage2.Get(meta.Identity.Number),
                storage3.Get(meta.Identity.Number),
                storage4.Get(meta.Identity.Number), storage5.Get(meta.Identity.Number),
                storage6.Get(meta.Identity.Number),
                storage7.Get(meta.Identity.Number));
        }

        public Enumerator<C1, C2, C3, C4, C5, C6, C7> GetEnumerator()
        {
            return new Enumerator<C1, C2, C3, C4, C5, C6, C7>(Entities, Identities, Storages);
        }
    }

    public class Query<C1, C2, C3, C4, C5, C6, C7, C8> : Query
        where C1 : class
        where C2 : class
        where C3 : class
        where C4 : class
        where C5 : class
        where C6 : class
        where C7 : class
        where C8 : class
    {
        public Query(Entities entities, Mask mask, List<Identity> identities) : base(entities, mask, identities)
        {
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected override IStorage[] GetStorages()
        {
            return new IStorage[]
            {
                Entities.GetStorage<C1>(),
                Entities.GetStorage<C2>(),
                Entities.GetStorage<C3>(),
                Entities.GetStorage<C4>(),
                Entities.GetStorage<C5>(),
                Entities.GetStorage<C6>(),
                Entities.GetStorage<C7>(),
                Entities.GetStorage<C8>(),
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public (C1, C2, C3, C4, C5, C6, C7, C8) Get(Entity entity)
        {
            var meta = Entities.GetEntityMeta(entity.Identity);
            var storage1 = (Storage<C1>)Storages[0];
            var storage2 = (Storage<C2>)Storages[1];
            var storage3 = (Storage<C3>)Storages[2];
            var storage4 = (Storage<C4>)Storages[3];
            var storage5 = (Storage<C5>)Storages[4];
            var storage6 = (Storage<C6>)Storages[5];
            var storage7 = (Storage<C7>)Storages[6];
            var storage8 = (Storage<C8>)Storages[7];
            return (storage1.Get(meta.Identity.Number), storage2.Get(meta.Identity.Number),
                storage3.Get(meta.Identity.Number),
                storage4.Get(meta.Identity.Number), storage5.Get(meta.Identity.Number),
                storage6.Get(meta.Identity.Number),
                storage7.Get(meta.Identity.Number), storage8.Get(meta.Identity.Number));
        }

        public Enumerator<C1, C2, C3, C4, C5, C6, C7, C8> GetEnumerator()
        {
            return new Enumerator<C1, C2, C3, C4, C5, C6, C7, C8>(Entities, Identities, Storages);
        }
    }

    public class Query<C1, C2, C3, C4, C5, C6, C7, C8, C9> : Query
        where C1 : class
        where C2 : class
        where C3 : class
        where C4 : class
        where C5 : class
        where C6 : class
        where C7 : class
        where C8 : class
        where C9 : class
    {
        public Query(Entities entities, Mask mask, List<Identity> identities) : base(entities, mask, identities)
        {
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected override IStorage[] GetStorages()
        {
            return new IStorage[]
            {
                Entities.GetStorage<C1>(),
                Entities.GetStorage<C2>(),
                Entities.GetStorage<C3>(),
                Entities.GetStorage<C4>(),
                Entities.GetStorage<C5>(),
                Entities.GetStorage<C6>(),
                Entities.GetStorage<C7>(),
                Entities.GetStorage<C8>(),
                Entities.GetStorage<C9>(),
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public (C1, C2, C3, C4, C5, C6, C7, C8, C9) Get(Entity entity)
        {
            var meta = Entities.GetEntityMeta(entity.Identity);
            var storage1 = (Storage<C1>)Storages[0];
            var storage2 = (Storage<C2>)Storages[1];
            var storage3 = (Storage<C3>)Storages[2];
            var storage4 = (Storage<C4>)Storages[3];
            var storage5 = (Storage<C5>)Storages[4];
            var storage6 = (Storage<C6>)Storages[5];
            var storage7 = (Storage<C7>)Storages[6];
            var storage8 = (Storage<C8>)Storages[7];
            var storage9 = (Storage<C9>)Storages[8];
            return (storage1.Get(meta.Identity.Number), storage2.Get(meta.Identity.Number),
                storage3.Get(meta.Identity.Number),
                storage4.Get(meta.Identity.Number), storage5.Get(meta.Identity.Number),
                storage6.Get(meta.Identity.Number),
                storage7.Get(meta.Identity.Number), storage8.Get(meta.Identity.Number),
                storage9.Get(meta.Identity.Number));
        }

        public Enumerator<C1, C2, C3, C4, C5, C6, C7, C8, C9> GetEnumerator()
        {
            return new Enumerator<C1, C2, C3, C4, C5, C6, C7, C8, C9>(Entities, Identities, Storages);
        }
    }

    public class Enumerator : IEnumerator, IDisposable
    {
        protected readonly List<Identity> Identities;
        protected readonly IStorage[] Storages;

        protected int Index;

        readonly Entities _entities;


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected Enumerator(Entities entities, List<Identity> identities, IStorage[] storages)
        {
            _entities = entities;
            Identities = identities;
            Storages = storages;

            _entities.Lock();

            Reset();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool MoveNext()
        {
            return ++Index < Identities.Count;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Reset()
        {
            Index = -1;
        }

        object IEnumerator.Current
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => throw new Exception("Invalid Enumerator");
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Dispose()
        {
            _entities.Unlock();
        }
    }

    public class Enumerator<C> : Enumerator
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Enumerator(Entities entities, List<Identity> identities, IStorage[] storages) : base(entities,
            identities,
            storages)
        {
        }

        public C Current
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => (C)Storages[0].GetRaw(Identities[Index].Number);
        }
    }

    public class Enumerator<C1, C2> : Enumerator
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Enumerator(Entities entities, List<Identity> identities, IStorage[] storages) : base(entities,
            identities,
            storages)
        {
        }

        public (C1, C2) Current
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => ((C1)Storages[0].GetRaw(Identities[Index].Number), (C2)Storages[1].GetRaw(Identities[Index].Number));
        }
    }

    public class Enumerator<C1, C2, C3> : Enumerator
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Enumerator(Entities entities, List<Identity> identities, IStorage[] storages) : base(entities,
            identities,
            storages)
        {
        }

        public (C1, C2, C3) Current
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => ((C1)Storages[0].GetRaw(Identities[Index].Number), (C2)Storages[1].GetRaw(Identities[Index].Number),
                (C3)Storages[2].GetRaw(Identities[Index].Number));
        }
    }

    public class Enumerator<C1, C2, C3, C4> : Enumerator
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Enumerator(Entities entities, List<Identity> identities, IStorage[] storages) : base(entities,
            identities,
            storages)
        {
        }

        public (C1, C2, C3, C4) Current
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => ((C1)Storages[0].GetRaw(Identities[Index].Number), (C2)Storages[1].GetRaw(Identities[Index].Number),
                (C3)Storages[2].GetRaw(Identities[Index].Number), (C4)Storages[3].GetRaw(Identities[Index].Number));
        }
    }

    public class Enumerator<C1, C2, C3, C4, C5> : Enumerator
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Enumerator(Entities entities, List<Identity> identities, IStorage[] storages) : base(entities,
            identities,
            storages)
        {
        }

        public (C1, C2, C3, C4, C5) Current
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => ((C1)Storages[0].GetRaw(Identities[Index].Number), (C2)Storages[1].GetRaw(Identities[Index].Number),
                (C3)Storages[2].GetRaw(Identities[Index].Number), (C4)Storages[3].GetRaw(Identities[Index].Number),
                (C5)Storages[4].GetRaw(Identities[Index].Number));
        }
    }

    public class Enumerator<C1, C2, C3, C4, C5, C6> : Enumerator
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Enumerator(Entities entities, List<Identity> identities, IStorage[] storages) : base(entities,
            identities,
            storages)
        {
        }

        public (C1, C2, C3, C4, C5, C6) Current
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => ((C1)Storages[0].GetRaw(Identities[Index].Number), (C2)Storages[1].GetRaw(Identities[Index].Number),
                (C3)Storages[2].GetRaw(Identities[Index].Number), (C4)Storages[3].GetRaw(Identities[Index].Number),
                (C5)Storages[4].GetRaw(Identities[Index].Number), (C6)Storages[5].GetRaw(Identities[Index].Number));
        }
    }

    public class Enumerator<C1, C2, C3, C4, C5, C6, C7> : Enumerator
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Enumerator(Entities entities, List<Identity> identities, IStorage[] storages) : base(entities,
            identities,
            storages)
        {
        }

        public (C1, C2, C3, C4, C5, C6, C7) Current
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => ((C1)Storages[0].GetRaw(Identities[Index].Number), (C2)Storages[1].GetRaw(Identities[Index].Number),
                (C3)Storages[2].GetRaw(Identities[Index].Number), (C4)Storages[3].GetRaw(Identities[Index].Number),
                (C5)Storages[4].GetRaw(Identities[Index].Number), (C6)Storages[5].GetRaw(Identities[Index].Number),
                (C7)Storages[6].GetRaw(Identities[Index].Number));
        }
    }

    public class Enumerator<C1, C2, C3, C4, C5, C6, C7, C8> : Enumerator
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Enumerator(Entities entities, List<Identity> identities, IStorage[] storages) : base(entities,
            identities,
            storages)
        {
        }

        public (C1, C2, C3, C4, C5, C6, C7, C8) Current
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => ((C1)Storages[0].GetRaw(Identities[Index].Number), (C2)Storages[1].GetRaw(Identities[Index].Number),
                (C3)Storages[2].GetRaw(Identities[Index].Number), (C4)Storages[3].GetRaw(Identities[Index].Number),
                (C5)Storages[4].GetRaw(Identities[Index].Number), (C6)Storages[5].GetRaw(Identities[Index].Number),
                (C7)Storages[6].GetRaw(Identities[Index].Number), (C8)Storages[7].GetRaw(Identities[Index].Number));
        }
    }

    public class Enumerator<C1, C2, C3, C4, C5, C6, C7, C8, C9> : Enumerator
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Enumerator(Entities entities, List<Identity> identities, IStorage[] storages) : base(entities,
            identities,
            storages)
        {
        }

        public (C1, C2, C3, C4, C5, C6, C7, C8, C9) Current
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => ((C1)Storages[0].GetRaw(Identities[Index].Number), (C2)Storages[1].GetRaw(Identities[Index].Number),
                (C3)Storages[2].GetRaw(Identities[Index].Number), (C4)Storages[3].GetRaw(Identities[Index].Number),
                (C5)Storages[4].GetRaw(Identities[Index].Number), (C6)Storages[5].GetRaw(Identities[Index].Number),
                (C7)Storages[6].GetRaw(Identities[Index].Number), (C8)Storages[7].GetRaw(Identities[Index].Number),
                (C9)Storages[8].GetRaw(Identities[Index].Number));
        }
    }
}