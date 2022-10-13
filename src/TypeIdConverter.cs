using System.Reflection;
using System.Runtime.CompilerServices;

namespace RelEcs
{
    public static class TypeIdConverter
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int Id<T>()
        {
            return TypeIdAssigner<T>.Id;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsTag<T>()
        {
            return TypeIdAssigner<T>.IsTag;
        }

        class TypeIdAssigner
        {
            protected static ushort Counter;
        }

        // ReSharper disable once ClassNeverInstantiated.Local
        class TypeIdAssigner<T> : TypeIdAssigner
        {
            public static readonly int Id;
            public static readonly bool IsTag;

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            static TypeIdAssigner()
            {
                Id = ++Counter;
                IsTag = typeof(T).GetFields(BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public).Length == 0;
            }
        }
    }
}