using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace RelEcs
{
    public sealed class Mask
    {
        internal readonly SortedSet<StorageType> HasTypes = new();
        internal readonly SortedSet<StorageType> NotTypes = new();
        internal readonly SortedSet<StorageType> AnyTypes = new();

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Has(StorageType type)
        {
            HasTypes.Add(type);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Not(StorageType type)
        {
            NotTypes.Add(type);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Any(StorageType type)
        {
            AnyTypes.Add(type);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override int GetHashCode()
        {
            var hash = HasTypes.Count + AnyTypes.Count + NotTypes.Count;

            unchecked
            {
                foreach (var type in HasTypes) hash = hash * 314159 + type.Id.GetHashCode();
                foreach (var type in NotTypes) hash = hash * 314159 - type.Id.GetHashCode();
                foreach (var type in AnyTypes) hash *= 314159 * type.Id.GetHashCode();
            }

            return hash;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Clear()
        {
            HasTypes.Clear();
            NotTypes.Clear();
            AnyTypes.Clear();
        }
    }
}