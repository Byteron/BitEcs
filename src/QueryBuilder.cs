using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace RelEcs
{
    public class QueryBuilder
    {
        internal readonly Entities Entities;
        protected readonly Mask Mask;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public QueryBuilder(Entities entities)
        {
            Entities = entities;
            Mask = new Mask();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public QueryBuilder Has<T>()
        {
            var typeIndex = StorageType.Create<T>();
            Mask.Has(typeIndex);
            return this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public QueryBuilder Not<T>()
        {
            var typeIndex = StorageType.Create<T>();
            Mask.Not(typeIndex);
            return this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public QueryBuilder Any<T>()
        {
            var typeIndex = StorageType.Create<T>();
            Mask.Any(typeIndex);
            return this;
        }
    }

    public sealed class QueryBuilder<C> : QueryBuilder
        where C : class
    {
        static readonly Func<Entities, Mask, List<Identity>, Query> CreateQuery =
            (archetypes, mask, identities) => new Query<C>(archetypes, mask, identities);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public QueryBuilder(Entities entities) : base(entities)
        {
            Has<C>();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public new QueryBuilder<C> Has<T>()
        {
            return (QueryBuilder<C>)base.Has<T>();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public new QueryBuilder<C> Not<T>()
        {
            return (QueryBuilder<C>)base.Not<T>();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public new QueryBuilder<C> Any<T>()
        {
            return (QueryBuilder<C>)base.Any<T>();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Query<C> Build()
        {
            return (Query<C>)Entities.GetQuery(Mask, CreateQuery);
        }
    }

    public sealed class QueryBuilder<C1, C2> : QueryBuilder
        where C1 : class
        where C2 : class
    {
        static readonly Func<Entities, Mask, List<Identity>, Query> CreateQuery =
            (archetypes, mask, identities) => new Query<C1, C2>(archetypes, mask, identities);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public QueryBuilder(Entities entities) : base(entities)
        {
            Has<C1>().Has<C2>();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public new QueryBuilder<C1, C2> Has<T>()
        {
            return (QueryBuilder<C1, C2>)base.Has<T>();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public new QueryBuilder<C1, C2> Not<T>()
        {
            return (QueryBuilder<C1, C2>)base.Not<T>();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public new QueryBuilder<C1, C2> Any<T>()
        {
            return (QueryBuilder<C1, C2>)base.Any<T>();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Query<C1, C2> Build()
        {
            return (Query<C1, C2>)Entities.GetQuery(Mask, CreateQuery);
        }
    }

    public sealed class QueryBuilder<C1, C2, C3> : QueryBuilder
        where C1 : class
        where C2 : class
        where C3 : class
    {
        static readonly Func<Entities, Mask, List<Identity>, Query> CreateQuery =
            (archetypes, mask, identities) => new Query<C1, C2, C3>(archetypes, mask, identities);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public QueryBuilder(Entities entities) : base(entities)
        {
            Has<C1>().Has<C2>().Has<C3>();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public new QueryBuilder<C1, C2, C3> Has<T>()
        {
            return (QueryBuilder<C1, C2, C3>)base.Has<T>();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public new QueryBuilder<C1, C2, C3> Not<T>()
        {
            return (QueryBuilder<C1, C2, C3>)base.Not<T>();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public new QueryBuilder<C1, C2, C3> Any<T>()
        {
            return (QueryBuilder<C1, C2, C3>)base.Any<T>();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Query<C1, C2, C3> Build()
        {
            return (Query<C1, C2, C3>)Entities.GetQuery(Mask, CreateQuery);
        }
    }

    public sealed class QueryBuilder<C1, C2, C3, C4> : QueryBuilder
        where C1 : class
        where C2 : class
        where C3 : class
        where C4 : class
    {
        static readonly Func<Entities, Mask, List<Identity>, Query> CreateQuery =
            (archetypes, mask, matchingTables) => new Query<C1, C2, C3, C4>(archetypes, mask, matchingTables);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public QueryBuilder(Entities entities) : base(entities)
        {
            Has<C1>().Has<C2>().Has<C3>().Has<C4>();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public new QueryBuilder<C1, C2, C3, C4> Has<T>()
        {
            return (QueryBuilder<C1, C2, C3, C4>)base.Has<T>();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public new QueryBuilder<C1, C2, C3, C4> Not<T>()
        {
            return (QueryBuilder<C1, C2, C3, C4>)base.Not<T>();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public new QueryBuilder<C1, C2, C3, C4> Any<T>()
        {
            return (QueryBuilder<C1, C2, C3, C4>)base.Any<T>();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Query<C1, C2, C3, C4> Build()
        {
            return (Query<C1, C2, C3, C4>)Entities.GetQuery(Mask, CreateQuery);
        }
    }

    public sealed class QueryBuilder<C1, C2, C3, C4, C5> : QueryBuilder
        where C1 : class
        where C2 : class
        where C3 : class
        where C4 : class
        where C5 : class
    {
        static readonly Func<Entities, Mask, List<Identity>, Query> CreateQuery =
            (archetypes, mask, identities) => new Query<C1, C2, C3, C4, C5>(archetypes, mask, identities);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public QueryBuilder(Entities entities) : base(entities)
        {
            Has<C1>().Has<C2>().Has<C3>().Has<C4>().Has<C5>();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public new QueryBuilder<C1, C2, C3, C4, C5> Has<T>()
        {
            return (QueryBuilder<C1, C2, C3, C4, C5>)base.Has<T>();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public new QueryBuilder<C1, C2, C3, C4, C5> Not<T>()
        {
            return (QueryBuilder<C1, C2, C3, C4, C5>)base.Not<T>();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public new QueryBuilder<C1, C2, C3, C4, C5> Any<T>()
        {
            return (QueryBuilder<C1, C2, C3, C4, C5>)base.Any<T>();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Query<C1, C2, C3, C4, C5> Build()
        {
            return (Query<C1, C2, C3, C4, C5>)Entities.GetQuery(Mask, CreateQuery);
        }
    }

    public sealed class QueryBuilder<C1, C2, C3, C4, C5, C6> : QueryBuilder
        where C1 : class
        where C2 : class
        where C3 : class
        where C4 : class
        where C5 : class
        where C6 : class
    {
        static readonly Func<Entities, Mask, List<Identity>, Query> CreateQuery =
            (archetypes, mask, identities) => new Query<C1, C2, C3, C4, C5, C6>(archetypes, mask, identities);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public QueryBuilder(Entities entities) : base(entities)
        {
            Has<C1>().Has<C2>().Has<C3>().Has<C4>().Has<C5>().Has<C6>();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public new QueryBuilder<C1, C2, C3, C4, C5, C6> Has<T>()
        {
            return (QueryBuilder<C1, C2, C3, C4, C5, C6>)base.Has<T>();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public new QueryBuilder<C1, C2, C3, C4, C5, C6> Not<T>()
        {
            return (QueryBuilder<C1, C2, C3, C4, C5, C6>)base.Not<T>();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public new QueryBuilder<C1, C2, C3, C4, C5, C6> Any<T>()
        {
            return (QueryBuilder<C1, C2, C3, C4, C5, C6>)base.Any<T>();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Query<C1, C2, C3, C4, C5, C6> Build()
        {
            return (Query<C1, C2, C3, C4, C5, C6>)Entities.GetQuery(Mask, CreateQuery);
        }
    }

    public sealed class QueryBuilder<C1, C2, C3, C4, C5, C6, C7> : QueryBuilder
        where C1 : class
        where C2 : class
        where C3 : class
        where C4 : class
        where C5 : class
        where C6 : class
        where C7 : class
    {
        static readonly Func<Entities, Mask, List<Identity>, Query> CreateQuery =
            (archetypes, mask, identities) => new Query<C1, C2, C3, C4, C5, C6, C7>(archetypes, mask, identities);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public QueryBuilder(Entities entities) : base(entities)
        {
            Has<C1>().Has<C2>().Has<C3>().Has<C4>().Has<C5>().Has<C6>().Has<C7>();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public new QueryBuilder<C1, C2, C3, C4, C5, C6, C7> Has<T>()
        {
            return (QueryBuilder<C1, C2, C3, C4, C5, C6, C7>)base.Has<T>();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public new QueryBuilder<C1, C2, C3, C4, C5, C6, C7> Not<T>()
        {
            return (QueryBuilder<C1, C2, C3, C4, C5, C6, C7>)base.Not<T>();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public new QueryBuilder<C1, C2, C3, C4, C5, C6, C7> Any<T>()
        {
            return (QueryBuilder<C1, C2, C3, C4, C5, C6, C7>)base.Any<T>();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Query<C1, C2, C3, C4, C5, C6, C7> Build()
        {
            return (Query<C1, C2, C3, C4, C5, C6, C7>)Entities.GetQuery(Mask, CreateQuery);
        }
    }

    public sealed class QueryBuilder<C1, C2, C3, C4, C5, C6, C7, C8> : QueryBuilder
        where C1 : class
        where C2 : class
        where C3 : class
        where C4 : class
        where C5 : class
        where C6 : class
        where C7 : class
        where C8 : class
    {
        static readonly Func<Entities, Mask, List<Identity>, Query> CreateQuery =
            (archetypes, mask, identities) => new Query<C1, C2, C3, C4, C5, C6, C7, C8>(archetypes, mask, identities);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public QueryBuilder(Entities entities) : base(entities)
        {
            Has<C1>().Has<C2>().Has<C3>().Has<C4>().Has<C5>().Has<C6>().Has<C7>().Has<C8>();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public new QueryBuilder<C1, C2, C3, C4, C5, C6, C7, C8> Has<T>()
        {
            return (QueryBuilder<C1, C2, C3, C4, C5, C6, C7, C8>)base.Has<T>();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public new QueryBuilder<C1, C2, C3, C4, C5, C6, C7, C8> Not<T>()
        {
            return (QueryBuilder<C1, C2, C3, C4, C5, C6, C7, C8>)base.Not<T>();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public new QueryBuilder<C1, C2, C3, C4, C5, C6, C7, C8> Any<T>()
        {
            return (QueryBuilder<C1, C2, C3, C4, C5, C6, C7, C8>)base.Any<T>();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Query<C1, C2, C3, C4, C5, C6, C7, C8> Build()
        {
            return (Query<C1, C2, C3, C4, C5, C6, C7, C8>)Entities.GetQuery(Mask, CreateQuery);
        }
    }

    public sealed class QueryBuilder<C1, C2, C3, C4, C5, C6, C7, C8, C9> : QueryBuilder
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
        static readonly Func<Entities, Mask, List<Identity>, Query> CreateQuery =
            (archetypes, mask, identities) =>
                new Query<C1, C2, C3, C4, C5, C6, C7, C8, C9>(archetypes, mask, identities);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public QueryBuilder(Entities entities) : base(entities)
        {
            Has<C1>().Has<C2>().Has<C3>().Has<C4>().Has<C5>().Has<C6>().Has<C7>().Has<C8>().Has<C9>();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public new QueryBuilder<C1, C2, C3, C4, C5, C6, C7, C8, C9> Has<T>()
        {
            return (QueryBuilder<C1, C2, C3, C4, C5, C6, C7, C8, C9>)base.Has<T>();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public new QueryBuilder<C1, C2, C3, C4, C5, C6, C7, C8, C9> Not<T>()
        {
            return (QueryBuilder<C1, C2, C3, C4, C5, C6, C7, C8, C9>)base.Not<T>();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public new QueryBuilder<C1, C2, C3, C4, C5, C6, C7, C8, C9> Any<T>()
        {
            return (QueryBuilder<C1, C2, C3, C4, C5, C6, C7, C8, C9>)base.Any<T>();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Query<C1, C2, C3, C4, C5, C6, C7, C8, C9> Build()
        {
            return (Query<C1, C2, C3, C4, C5, C6, C7, C8, C9>)Entities.GetQuery(Mask, CreateQuery);
        }
    }
}