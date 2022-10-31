using System;
using System.Runtime.CompilerServices;

namespace RelEcs
{
    public struct StorageType : IComparable<StorageType>
    {
        public Type Type { get; private set; }
        public ulong Value { get; private set; }
        public bool IsTag { get; private set; }
        public bool IsRelation { get; private set; }

        public ushort TypeId
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => TypeIdConverter.Type(Value);
        }

        public Identity Identity
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => TypeIdConverter.Identity(Value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static StorageType Create<T>(Identity identity)
        {
            return new StorageType
            {
                Value = TypeIdConverter.Value<T>(identity),
                Type = typeof(T),
                IsTag = TypeIdConverter.IsTag<T>(),
                IsRelation = identity.Id > 0,
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static StorageType CreateFromType(Type type, Identity identity)
        {
            var createMethod = typeof(StorageType).GetMethod("Create");
            var genericCreateMethod = createMethod?.MakeGenericMethod(type);
            return (StorageType)genericCreateMethod?.Invoke(null, new object[] { identity });
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int CompareTo(StorageType other)
        {
            return Value.CompareTo(other.Value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override bool Equals(object obj)
        {
            return (obj is StorageType other) && Value == other.Value;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override int GetHashCode()
        {
            return Value.GetHashCode();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override string ToString()
        {
            return IsRelation ? $"{GetHashCode()} {Type.Name}::{Identity}" : $"{GetHashCode()} {Type.Name}";
        }

        public static bool operator ==(StorageType left, StorageType right) => left.Equals(right);
        public static bool operator !=(StorageType left, StorageType right) => !left.Equals(right);
    }
}