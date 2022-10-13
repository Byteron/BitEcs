using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace RelEcs
{
    public struct EntityMeta
    {
        public Identity Identity;
        public SortedSet<StorageType> Components;
    }

    public readonly struct Identity
    {
        public static Identity None = default;
        public static Identity Any = new(int.MaxValue, 0);

        public readonly int Number;
        public readonly ushort Generation;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Identity(int number, ushort generation = 1)
        {
            Number = number;
            Generation = generation;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override bool Equals(object obj)
        {
            return (obj is Identity other) && Number == other.Number && Generation == other.Generation;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override int GetHashCode()
        {
            unchecked // Allow arithmetic overflow, numbers will just "wrap around"
            {
                var hashcode = 1430287;
                hashcode = hashcode * 7302013 ^ Number.GetHashCode();
                hashcode = hashcode * 7302013 ^ Generation.GetHashCode();
                return hashcode;
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override string ToString()
        {
            return Number.ToString();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator ==(Identity left, Identity right) => left.Equals(right);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator !=(Identity left, Identity right) => !left.Equals(right);
    }
}