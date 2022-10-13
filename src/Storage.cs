using System;
using System.Runtime.CompilerServices;

namespace RelEcs
{
    public interface IStorage
    {
        StorageType Type { get; set; }

        void AddRaw(int id, object data);
        object GetRaw(int id);
        bool Has(int id);
        void Remove(int id);
        void Resize(int capacity);
    }

    public sealed class Storage<T> : IStorage where T : class
    {
        public StorageType Type { get; set; }

        T[] _items;

        public Storage()
        {
        }

        public Storage(StorageType type, int capacity)
        {
            _items = new T[capacity];
            Type = type;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void AddRaw(int id, object data)
        {
            _items[id] = (T)data;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public object GetRaw(int id)
        {
            return _items[id];
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Add(int id, T data)
        {
            _items[id] = data;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public T Get(int id)
        {
            return _items[id];
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Remove(int id)
        {
            _items[id] = null;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool Has(int id)
        {
            return _items[id] == null;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Resize(int capacity)
        {
            Array.Resize(ref _items, capacity);
        }
    }

    public struct StorageType : IComparable<StorageType>
    {
        public Type Type { get; private set; }
        public int Id { get; private set; }
        public bool IsTag { get; private set; }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static StorageType Create<T>()
        {
            return new StorageType()
            {
                Id = TypeIdConverter.Id<T>(),
                Type = typeof(T),
                IsTag = TypeIdConverter.IsTag<T>(),
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int CompareTo(StorageType other)
        {
            return Id.CompareTo(other.Id);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override bool Equals(object obj)
        {
            return (obj is StorageType other) && Id == other.Id;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override string ToString()
        {
            return $"{GetHashCode()} {Type.Name}";
        }

        public static bool operator ==(StorageType left, StorageType right) => left.Equals(right);
        public static bool operator !=(StorageType left, StorageType right) => !left.Equals(right);
    }
}