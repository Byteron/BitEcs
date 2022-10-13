using System.Runtime.CompilerServices;
using System.Collections.Generic;

namespace RelEcs
{
    public static class MaskPool
    {
        static readonly Stack<Mask> Stack = new();

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Mask Get()
        {
            return Stack.Count > 0 ? Stack.Pop() : new Mask();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Add(Mask mask)
        {
            mask.Clear();
            Stack.Push(mask);
        }
    }
}