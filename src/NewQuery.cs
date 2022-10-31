using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace RelEcs;

public class NewQuery<Q, F>
{
    public readonly StorageType[] Types;
    public readonly List<Table> Tables;

    internal readonly Archetypes Archetypes;

    public NewQuery(Archetypes archetypes, StorageType[] types, List<Table> tables)
    {
        Archetypes = archetypes;
        Types = types;
        Tables = tables;
    }

    public NewEnumerator<Q> GetEnumerator()
    {
        return NewEnumerator<Q>.Create(Types, Archetypes, Tables);
    }
}

public class NewEnumerator<Q> : IDisposable
{
    internal Archetypes Archetypes;
    internal List<Table> Tables;

    protected int TableIndex;
    protected int EntityIndex;

    public static NewEnumerator<Q> Create(StorageType[] types, Archetypes archetypes, List<Table> tables)
    {
        var typeArgs = new List<Type> { typeof(Q) };

        foreach (var type in types)
        {
            typeArgs.Add(type.Type);
        }

        var baseType = Type.GetType($"RelEcs.NewEnumerator`{types.Length + 1}");
        var genericType = baseType.MakeGenericType(typeArgs.ToArray());

        var enumerator = (NewEnumerator<Q>)Activator.CreateInstance(genericType);
        enumerator.Archetypes = archetypes;
        enumerator.Tables = tables;
        enumerator.Archetypes.Lock();
        enumerator.Reset();

        return enumerator;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool MoveNext()
    {
        if (TableIndex == Tables.Count) return false;

        if (++EntityIndex < Tables[TableIndex].Count)
        {
            UpdateCurrent();
            return true;
        }

        EntityIndex = 0;
        TableIndex++;

        while (TableIndex < Tables.Count && Tables[TableIndex].Count == 0)
        {
            TableIndex++;
        }

        UpdateStorage();

        var next = TableIndex < Tables.Count && EntityIndex < Tables[TableIndex].Count;
        if (next) UpdateCurrent();
        return next;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Reset()
    {
        TableIndex = 0;
        EntityIndex = -1;

        UpdateStorage();
    }

    public Q Current { get; set; }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Dispose()
    {
        Archetypes.Unlock();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    protected virtual void UpdateCurrent()
    {
        throw new Exception("Invalid Enumerator");
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    protected virtual void UpdateStorage()
    {
        throw new Exception("Invalid Enumerator");
    }
}

public class NewEnumerator<Q, C> : NewEnumerator<Q> where Q : ITuple
{
    C[] _storage;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    protected override void UpdateStorage()
    {
        if (TableIndex == Tables.Count) return;
        _storage = Tables[TableIndex].GetStorage<C>(Identity.None);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    protected override void UpdateCurrent()
    {
        Current = (Q)(ITuple)_storage[0];
    }
}

public class NewEnumerator<Q, C1, C2> : NewEnumerator<Q>
{
    C1[] _storage1;
    C2[] _storage2;
    ValueTuple<C1, C2> _current = new(default, default);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    protected override void UpdateStorage()
    {
        if (TableIndex == Tables.Count) return;
        _storage1 = Tables[TableIndex].GetStorage<C1>(Identity.None);
        _storage2 = Tables[TableIndex].GetStorage<C2>(Identity.None);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    protected override void UpdateCurrent()
    {
        _current.Item1 = _storage1[EntityIndex];
        _current.Item2 = _storage2[EntityIndex];
        Current = (Q)(object)_current;
    }
}

public class NewEnumerator<Q, C1, C2, C3> : NewEnumerator<Q>
{
    C1[] _storage1;
    C2[] _storage2;
    C3[] _storage3;
    
    ValueTuple<C1, C2, C3> _current = new(default, default, default);
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    protected override void UpdateStorage()
    {
        if (TableIndex == Tables.Count) return;
        _storage1 = Tables[TableIndex].GetStorage<C1>(Identity.None);
        _storage2 = Tables[TableIndex].GetStorage<C2>(Identity.None);
        _storage3 = Tables[TableIndex].GetStorage<C3>(Identity.None);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    protected override void UpdateCurrent()
    {
        _current.Item1 = _storage1[EntityIndex];
        _current.Item2 = _storage2[EntityIndex];
        _current.Item3 = _storage3[EntityIndex];
        Current = (Q)(object)_current;
    }
}

// public class NewEnumerator<Q, C1, C2, C3, C4> : NewEnumerator<Q>
// {
//     C1[] _storage1;
//     C2[] _storage2;
//     C3[] _storage3;
//     C4[] _storage4;
//
//     [MethodImpl(MethodImplOptions.AggressiveInlining)]
//     protected override void UpdateStorage()
//     {
//         if (TableIndex == Tables.Count) return;
//         _storage1 = Tables[TableIndex].GetStorage<C1>(Identity.None);
//         _storage2 = Tables[TableIndex].GetStorage<C2>(Identity.None);
//         _storage3 = Tables[TableIndex].GetStorage<C3>(Identity.None);
//         _storage4 = Tables[TableIndex].GetStorage<C4>(Identity.None);
//     }
//
//     public (C1, C2, C3, C4) Current
//     {
//         [MethodImpl(MethodImplOptions.AggressiveInlining)]
//         get => (_storage1[EntityIndex], _storage2[EntityIndex], _storage3[EntityIndex], _storage4[EntityIndex]);
//     }
// }
//
// public class NewEnumerator<Q, C1, C2, C3, C4, C5> : NewEnumerator<Q>
// {
//     C1[] _storage1;
//     C2[] _storage2;
//     C3[] _storage3;
//     C4[] _storage4;
//     C5[] _storage5;
//
//     [MethodImpl(MethodImplOptions.AggressiveInlining)]
//     protected override void UpdateStorage()
//     {
//         if (TableIndex == Tables.Count) return;
//         _storage1 = Tables[TableIndex].GetStorage<C1>(Identity.None);
//         _storage2 = Tables[TableIndex].GetStorage<C2>(Identity.None);
//         _storage3 = Tables[TableIndex].GetStorage<C3>(Identity.None);
//         _storage4 = Tables[TableIndex].GetStorage<C4>(Identity.None);
//         _storage5 = Tables[TableIndex].GetStorage<C5>(Identity.None);
//     }
//
//     public (C1, C2, C3, C4, C5) Current
//     {
//         [MethodImpl(MethodImplOptions.AggressiveInlining)]
//         get =>
//             (_storage1[EntityIndex], _storage2[EntityIndex], _storage3[EntityIndex], _storage4[EntityIndex]
//                 , _storage5[EntityIndex]);
//     }
// }
//
// public class NewEnumerator<Q, C1, C2, C3, C4, C5, C6> : NewEnumerator<Q>
// {
//     C1[] _storage1;
//     C2[] _storage2;
//     C3[] _storage3;
//     C4[] _storage4;
//     C5[] _storage5;
//     C6[] _storage6;
//
//     [MethodImpl(MethodImplOptions.AggressiveInlining)]
//     protected override void UpdateStorage()
//     {
//         if (TableIndex == Tables.Count) return;
//         _storage1 = Tables[TableIndex].GetStorage<C1>(Identity.None);
//         _storage2 = Tables[TableIndex].GetStorage<C2>(Identity.None);
//         _storage3 = Tables[TableIndex].GetStorage<C3>(Identity.None);
//         _storage4 = Tables[TableIndex].GetStorage<C4>(Identity.None);
//         _storage5 = Tables[TableIndex].GetStorage<C5>(Identity.None);
//         _storage6 = Tables[TableIndex].GetStorage<C6>(Identity.None);
//     }
//
//     public (C1, C2, C3, C4, C5, C6) Current
//     {
//         [MethodImpl(MethodImplOptions.AggressiveInlining)]
//         get =>
//             (_storage1[EntityIndex], _storage2[EntityIndex], _storage3[EntityIndex], _storage4[EntityIndex],
//                 _storage5[EntityIndex], _storage6[EntityIndex]);
//     }
// }
//
// public class NewEnumerator<Q, C1, C2, C3, C4, C5, C6, C7> : NewEnumerator<Q>
// {
//     C1[] _storage1;
//     C2[] _storage2;
//     C3[] _storage3;
//     C4[] _storage4;
//     C5[] _storage5;
//     C6[] _storage6;
//     C7[] _storage7;
//
//     [MethodImpl(MethodImplOptions.AggressiveInlining)]
//     protected override void UpdateStorage()
//     {
//         if (TableIndex == Tables.Count) return;
//         _storage1 = Tables[TableIndex].GetStorage<C1>(Identity.None);
//         _storage2 = Tables[TableIndex].GetStorage<C2>(Identity.None);
//         _storage3 = Tables[TableIndex].GetStorage<C3>(Identity.None);
//         _storage4 = Tables[TableIndex].GetStorage<C4>(Identity.None);
//         _storage5 = Tables[TableIndex].GetStorage<C5>(Identity.None);
//         _storage6 = Tables[TableIndex].GetStorage<C6>(Identity.None);
//         _storage7 = Tables[TableIndex].GetStorage<C7>(Identity.None);
//     }
//
//     public (C1, C2, C3, C4, C5, C6, C7) Current
//     {
//         [MethodImpl(MethodImplOptions.AggressiveInlining)]
//         get =>
//             (_storage1[EntityIndex], _storage2[EntityIndex], _storage3[EntityIndex], _storage4[EntityIndex]
//                 , _storage5[EntityIndex], _storage6[EntityIndex], _storage7[EntityIndex]);
//     }
// }
//
// public class NewEnumerator<Q, C1, C2, C3, C4, C5, C6, C7, C8> : NewEnumerator<Q>
// {
//     C1[] _storage1;
//     C2[] _storage2;
//     C3[] _storage3;
//     C4[] _storage4;
//     C5[] _storage5;
//     C6[] _storage6;
//     C7[] _storage7;
//     C8[] _storage8;
//
//     [MethodImpl(MethodImplOptions.AggressiveInlining)]
//     protected override void UpdateStorage()
//     {
//         if (TableIndex == Tables.Count) return;
//         _storage1 = Tables[TableIndex].GetStorage<C1>(Identity.None);
//         _storage2 = Tables[TableIndex].GetStorage<C2>(Identity.None);
//         _storage3 = Tables[TableIndex].GetStorage<C3>(Identity.None);
//         _storage4 = Tables[TableIndex].GetStorage<C4>(Identity.None);
//         _storage5 = Tables[TableIndex].GetStorage<C5>(Identity.None);
//         _storage6 = Tables[TableIndex].GetStorage<C6>(Identity.None);
//         _storage7 = Tables[TableIndex].GetStorage<C7>(Identity.None);
//         _storage8 = Tables[TableIndex].GetStorage<C8>(Identity.None);
//     }
//
//     public (C1, C2, C3, C4, C5, C6, C7, C8) Current
//     {
//         [MethodImpl(MethodImplOptions.AggressiveInlining)]
//         get =>
//             (_storage1[EntityIndex], _storage2[EntityIndex], _storage3[EntityIndex], _storage4[EntityIndex],
//                 _storage5[EntityIndex], _storage6[EntityIndex], _storage7[EntityIndex], _storage8[EntityIndex]);
//     }
// }
//
// public class NewEnumerator<Q, C1, C2, C3, C4, C5, C6, C7, C8, C9> : NewEnumerator<Q>
// {
//     C1[] _storage1;
//     C2[] _storage2;
//     C3[] _storage3;
//     C4[] _storage4;
//     C5[] _storage5;
//     C6[] _storage6;
//     C7[] _storage7;
//     C8[] _storage8;
//     C9[] _storage9;
//
//     [MethodImpl(MethodImplOptions.AggressiveInlining)]
//     protected override void UpdateStorage()
//     {
//         if (TableIndex == Tables.Count) return;
//         _storage1 = Tables[TableIndex].GetStorage<C1>(Identity.None);
//         _storage2 = Tables[TableIndex].GetStorage<C2>(Identity.None);
//         _storage3 = Tables[TableIndex].GetStorage<C3>(Identity.None);
//         _storage4 = Tables[TableIndex].GetStorage<C4>(Identity.None);
//         _storage5 = Tables[TableIndex].GetStorage<C5>(Identity.None);
//         _storage6 = Tables[TableIndex].GetStorage<C6>(Identity.None);
//         _storage7 = Tables[TableIndex].GetStorage<C7>(Identity.None);
//         _storage8 = Tables[TableIndex].GetStorage<C8>(Identity.None);
//         _storage9 = Tables[TableIndex].GetStorage<C9>(Identity.None);
//     }
//
//     public (C1, C2, C3, C4, C5, C6, C7, C8, C9) Current
//     {
//         [MethodImpl(MethodImplOptions.AggressiveInlining)]
//         get =>
//             (_storage1[EntityIndex], _storage2[EntityIndex], _storage3[EntityIndex], _storage4[EntityIndex],
//                 _storage5[EntityIndex], _storage6[EntityIndex], _storage7[EntityIndex], _storage8[EntityIndex],
//                 _storage9[EntityIndex]);
//     }
// }