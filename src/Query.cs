using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace HypEcs;

public class Query
{
    public readonly List<Table> Tables;

    internal readonly Archetypes Archetypes;
    internal readonly Mask Mask;

    protected readonly Dictionary<int, Array[]> Storages = new();

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Query(Archetypes archetypes, Mask mask, List<Table> tables)
    {
        Tables = tables;
        Archetypes = archetypes;
        Mask = mask;

        UpdateStorages();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool Has(Entity entity)
    {
        var meta = Archetypes.GetEntityMeta(entity.Identity);
        return Storages.ContainsKey(meta.TableId);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal void AddTable(Table table)
    {
        Tables.Add(table);
        UpdateStorages();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    protected virtual Array[] GetStorages(Table table)
    {
        throw new Exception("Invalid Enumerator");
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    void UpdateStorages()
    {
        Storages.Clear();

        foreach (var table in Tables)
        {
            var storages = GetStorages(table);
            Storages.Add(table.Id, storages);
        }
    }
}

public class TriggerQuery<C> : Query
    where C : struct
{
    readonly Type _systemType;

    public TriggerQuery(Archetypes archetypes, Mask mask, List<Table> tables, Type systemType) : base(archetypes,
        mask, tables)
    {
        _systemType = systemType;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    protected override Array[] GetStorages(Table table)
    {
        return new Array[] { table.GetStorage<Trigger<C>>(Identity.None) };
    }

    public TriggerEnumerator<C> GetEnumerator()
    {
        return new TriggerEnumerator<C>(Tables, _systemType);
    }
}

public class Query<C> : Query
    where C : struct
{
    public Query(Archetypes archetypes, Mask mask, List<Table> tables) : base(archetypes, mask, tables)
    {
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    protected override Array[] GetStorages(Table table)
    {
        return new Array[] { table.GetStorage<C>(Identity.None) };
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public ref C Get(Entity entity)
    {
        var meta = Archetypes.GetEntityMeta(entity.Identity);
        var storage = (C[])Storages[meta.TableId][0];
        return ref storage[meta.Row];
    }

    public Enumerator<C> GetEnumerator()
    {
        return new Enumerator<C>(Archetypes, Tables);
    }
}

public class Query<C1, C2> : Query
    where C1 : struct
    where C2 : struct
{
    public Query(Archetypes archetypes, Mask mask, List<Table> tables) : base(archetypes, mask, tables)
    {
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    protected override Array[] GetStorages(Table table)
    {
        return new Array[]
        {
            table.GetStorage<C1>(Identity.None),
            table.GetStorage<C2>(Identity.None),
        };
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RefValueTuple<C1, C2> Get(Entity entity)
    {
        var meta = Archetypes.GetEntityMeta(entity.Identity);
        var storages = Storages[meta.TableId];
        var storage1 = (C1[])storages[0];
        var storage2 = (C2[])storages[1];
        return new RefValueTuple<C1, C2>(ref storage1[meta.Row], ref storage2[meta.Row]);
    }

    public Enumerator<C1, C2> GetEnumerator()
    {
        return new Enumerator<C1, C2>(Archetypes, Tables);
    }
}

public class Query<C1, C2, C3> : Query
    where C1 : struct
    where C2 : struct
    where C3 : struct
{
    public Query(Archetypes archetypes, Mask mask, List<Table> tables) : base(archetypes, mask, tables)
    {
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    protected override Array[] GetStorages(Table table)
    {
        return new Array[]
        {
            table.GetStorage<C1>(Identity.None),
            table.GetStorage<C2>(Identity.None),
            table.GetStorage<C3>(Identity.None),
        };
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RefValueTuple<C1, C2, C3> Get(Entity entity)
    {
        var meta = Archetypes.GetEntityMeta(entity.Identity);
        var storages = Storages[meta.TableId];
        var storage1 = (C1[])storages[0];
        var storage2 = (C2[])storages[1];
        var storage3 = (C3[])storages[2];
        return new RefValueTuple<C1, C2, C3>(ref storage1[meta.Row], ref storage2[meta.Row],
            ref storage3[meta.Row]);
    }

    public Enumerator<C1, C2, C3> GetEnumerator()
    {
        return new Enumerator<C1, C2, C3>(Archetypes, Tables);
    }
}

public class Query<C1, C2, C3, C4> : Query
    where C1 : struct
    where C2 : struct
    where C3 : struct
    where C4 : struct
{
    public Query(Archetypes archetypes, Mask mask, List<Table> tables) : base(archetypes, mask, tables)
    {
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    protected override Array[] GetStorages(Table table)
    {
        return new Array[]
        {
            table.GetStorage<C1>(Identity.None),
            table.GetStorage<C2>(Identity.None),
            table.GetStorage<C3>(Identity.None),
            table.GetStorage<C4>(Identity.None),
        };
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RefValueTuple<C1, C2, C3, C4> Get(Entity entity)
    {
        var meta = Archetypes.GetEntityMeta(entity.Identity);
        var storages = Storages[meta.TableId];
        var storage1 = (C1[])storages[0];
        var storage2 = (C2[])storages[1];
        var storage3 = (C3[])storages[2];
        var storage4 = (C4[])storages[3];
        return new RefValueTuple<C1, C2, C3, C4>(ref storage1[meta.Row], ref storage2[meta.Row],
            ref storage3[meta.Row], ref storage4[meta.Row]);
    }

    public Enumerator<C1, C2, C3, C4> GetEnumerator()
    {
        return new Enumerator<C1, C2, C3, C4>(Archetypes, Tables);
    }
}

public class Query<C1, C2, C3, C4, C5> : Query
    where C1 : struct
    where C2 : struct
    where C3 : struct
    where C4 : struct
    where C5 : struct
{
    public Query(Archetypes archetypes, Mask mask, List<Table> tables) : base(archetypes, mask, tables)
    {
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    protected override Array[] GetStorages(Table table)
    {
        return new Array[]
        {
            table.GetStorage<C1>(Identity.None),
            table.GetStorage<C2>(Identity.None),
            table.GetStorage<C3>(Identity.None),
            table.GetStorage<C4>(Identity.None),
            table.GetStorage<C5>(Identity.None),
        };
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RefValueTuple<C1, C2, C3, C4, C5> Get(Entity entity)
    {
        var meta = Archetypes.GetEntityMeta(entity.Identity);
        var storages = Storages[meta.TableId];
        var storage1 = (C1[])storages[0];
        var storage2 = (C2[])storages[1];
        var storage3 = (C3[])storages[2];
        var storage4 = (C4[])storages[3];
        var storage5 = (C5[])storages[4];
        return new RefValueTuple<C1, C2, C3, C4, C5>(ref storage1[meta.Row], ref storage2[meta.Row],
            ref storage3[meta.Row], ref storage4[meta.Row], ref storage5[meta.Row]);
    }

    public Enumerator<C1, C2, C3, C4, C5> GetEnumerator()
    {
        return new Enumerator<C1, C2, C3, C4, C5>(Archetypes, Tables);
    }
}

public class Query<C1, C2, C3, C4, C5, C6> : Query
    where C1 : struct
    where C2 : struct
    where C3 : struct
    where C4 : struct
    where C5 : struct
    where C6 : struct
{
    public Query(Archetypes archetypes, Mask mask, List<Table> tables) : base(archetypes, mask, tables)
    {
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    protected override Array[] GetStorages(Table table)
    {
        return new Array[]
        {
            table.GetStorage<C1>(Identity.None),
            table.GetStorage<C2>(Identity.None),
            table.GetStorage<C3>(Identity.None),
            table.GetStorage<C4>(Identity.None),
            table.GetStorage<C5>(Identity.None),
            table.GetStorage<C6>(Identity.None),
        };
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RefValueTuple<C1, C2, C3, C4, C5, C6> Get(Entity entity)
    {
        var meta = Archetypes.GetEntityMeta(entity.Identity);
        var storages = Storages[meta.TableId];
        var storage1 = (C1[])storages[0];
        var storage2 = (C2[])storages[1];
        var storage3 = (C3[])storages[2];
        var storage4 = (C4[])storages[3];
        var storage5 = (C5[])storages[4];
        var storage6 = (C6[])storages[5];
        return new RefValueTuple<C1, C2, C3, C4, C5, C6>(ref storage1[meta.Row], ref storage2[meta.Row],
            ref storage3[meta.Row], ref storage4[meta.Row], ref storage5[meta.Row],
            ref storage6[meta.Row]);
    }

    public Enumerator<C1, C2, C3, C4, C5, C6> GetEnumerator()
    {
        return new Enumerator<C1, C2, C3, C4, C5, C6>(Archetypes, Tables);
    }
}

public class Query<C1, C2, C3, C4, C5, C6, C7> : Query
    where C1 : struct
    where C2 : struct
    where C3 : struct
    where C4 : struct
    where C5 : struct
    where C6 : struct
    where C7 : struct
{
    public Query(Archetypes archetypes, Mask mask, List<Table> tables) : base(archetypes, mask, tables)
    {
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    protected override Array[] GetStorages(Table table)
    {
        return new Array[]
        {
            table.GetStorage<C1>(Identity.None),
            table.GetStorage<C2>(Identity.None),
            table.GetStorage<C3>(Identity.None),
            table.GetStorage<C4>(Identity.None),
            table.GetStorage<C5>(Identity.None),
            table.GetStorage<C6>(Identity.None),
            table.GetStorage<C7>(Identity.None),
        };
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RefValueTuple<C1, C2, C3, C4, C5, C6, C7> Get(Entity entity)
    {
        var meta = Archetypes.GetEntityMeta(entity.Identity);
        var storages = Storages[meta.TableId];
        var storage1 = (C1[])storages[0];
        var storage2 = (C2[])storages[1];
        var storage3 = (C3[])storages[2];
        var storage4 = (C4[])storages[3];
        var storage5 = (C5[])storages[4];
        var storage6 = (C6[])storages[5];
        var storage7 = (C7[])storages[6];
        return new RefValueTuple<C1, C2, C3, C4, C5, C6, C7>(ref storage1[meta.Row], ref storage2[meta.Row],
            ref storage3[meta.Row], ref storage4[meta.Row], ref storage5[meta.Row],
            ref storage6[meta.Row], ref storage7[meta.Row]);
    }

    public Enumerator<C1, C2, C3, C4, C5, C6, C7> GetEnumerator()
    {
        return new Enumerator<C1, C2, C3, C4, C5, C6, C7>(Archetypes, Tables);
    }
}

public class Query<C1, C2, C3, C4, C5, C6, C7, C8> : Query
    where C1 : struct
    where C2 : struct
    where C3 : struct
    where C4 : struct
    where C5 : struct
    where C6 : struct
    where C7 : struct
    where C8 : struct
{
    public Query(Archetypes archetypes, Mask mask, List<Table> tables) : base(archetypes, mask, tables)
    {
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    protected override Array[] GetStorages(Table table)
    {
        return new Array[]
        {
            table.GetStorage<C1>(Identity.None),
            table.GetStorage<C2>(Identity.None),
            table.GetStorage<C3>(Identity.None),
            table.GetStorage<C4>(Identity.None),
            table.GetStorage<C5>(Identity.None),
            table.GetStorage<C6>(Identity.None),
            table.GetStorage<C7>(Identity.None),
            table.GetStorage<C8>(Identity.None),
        };
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RefValueTuple<C1, C2, C3, C4, C5, C6, C7, C8> Get(Entity entity)
    {
        var meta = Archetypes.GetEntityMeta(entity.Identity);
        var storages = Storages[meta.TableId];
        var storage1 = (C1[])storages[0];
        var storage2 = (C2[])storages[1];
        var storage3 = (C3[])storages[2];
        var storage4 = (C4[])storages[3];
        var storage5 = (C5[])storages[4];
        var storage6 = (C6[])storages[5];
        var storage7 = (C7[])storages[6];
        var storage8 = (C8[])storages[7];
        return new RefValueTuple<C1, C2, C3, C4, C5, C6, C7, C8>(ref storage1[meta.Row], ref storage2[meta.Row],
            ref storage3[meta.Row], ref storage4[meta.Row], ref storage5[meta.Row],
            ref storage6[meta.Row], ref storage7[meta.Row], ref storage8[meta.Row]);
    }

    public Enumerator<C1, C2, C3, C4, C5, C6, C7, C8> GetEnumerator()
    {
        return new Enumerator<C1, C2, C3, C4, C5, C6, C7, C8>(Archetypes, Tables);
    }
}

public class Query<C1, C2, C3, C4, C5, C6, C7, C8, C9> : Query
    where C1 : struct
    where C2 : struct
    where C3 : struct
    where C4 : struct
    where C5 : struct
    where C6 : struct
    where C7 : struct
    where C8 : struct
    where C9 : struct
{
    public Query(Archetypes archetypes, Mask mask, List<Table> tables) : base(archetypes, mask, tables)
    {
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    protected override Array[] GetStorages(Table table)
    {
        return new Array[]
        {
            table.GetStorage<C1>(Identity.None),
            table.GetStorage<C2>(Identity.None),
            table.GetStorage<C3>(Identity.None),
            table.GetStorage<C4>(Identity.None),
            table.GetStorage<C5>(Identity.None),
            table.GetStorage<C6>(Identity.None),
            table.GetStorage<C7>(Identity.None),
            table.GetStorage<C8>(Identity.None),
            table.GetStorage<C9>(Identity.None),
        };
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RefValueTuple<C1, C2, C3, C4, C5, C6, C7, C8, C9> Get(Entity entity)
    {
        var meta = Archetypes.GetEntityMeta(entity.Identity);
        var storages = Storages[meta.TableId];
        var storage1 = (C1[])storages[0];
        var storage2 = (C2[])storages[1];
        var storage3 = (C3[])storages[2];
        var storage4 = (C4[])storages[3];
        var storage5 = (C5[])storages[4];
        var storage6 = (C6[])storages[5];
        var storage7 = (C7[])storages[6];
        var storage8 = (C8[])storages[7];
        var storage9 = (C9[])storages[8];
        return new RefValueTuple<C1, C2, C3, C4, C5, C6, C7, C8, C9>(ref storage1[meta.Row], ref storage2[meta.Row],
            ref storage3[meta.Row], ref storage4[meta.Row], ref storage5[meta.Row],
            ref storage6[meta.Row], ref storage7[meta.Row], ref storage8[meta.Row], ref storage9[meta.Row]);
    }

    public Enumerator<C1, C2, C3, C4, C5, C6, C7, C8, C9> GetEnumerator()
    {
        return new Enumerator<C1, C2, C3, C4, C5, C6, C7, C8, C9>(Archetypes, Tables);
    }
}

public class Enumerator : IEnumerator, IDisposable
{
    protected readonly List<Table> Tables;

    protected int TableIndex;
    protected int EntityIndex;

    readonly Archetypes _archetypes;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    protected Enumerator(Archetypes archetypes, List<Table> tables)
    {
        _archetypes = archetypes;
        Tables = tables;

        _archetypes.Lock();

        Reset();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool MoveNext()
    {
        if (TableIndex == Tables.Count) return false;

        if (++EntityIndex < Tables[TableIndex].Count) return true;

        EntityIndex = 0;
        TableIndex++;

        while (TableIndex < Tables.Count && Tables[TableIndex].IsEmpty)
        {
            TableIndex++;
        }

        UpdateStorage();

        return TableIndex < Tables.Count && EntityIndex < Tables[TableIndex].Count;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Reset()
    {
        TableIndex = 0;
        EntityIndex = -1;

        UpdateStorage();
    }

    object IEnumerator.Current
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get => throw new Exception("Invalid Enumerator");
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Dispose()
    {
        _archetypes.Unlock();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    protected virtual void UpdateStorage()
    {
        throw new Exception("Invalid Enumerator");
    }
}

public class TriggerEnumerator : IEnumerator, IDisposable
{
    protected readonly List<Table> Tables;

    protected int TableIndex;
    protected int EntityIndex;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    protected TriggerEnumerator(List<Table> tables)
    {
        Tables = tables;
        Reset();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool MoveNext()
    {
        if (TableIndex == Tables.Count) return false;

        if (++EntityIndex < Tables[TableIndex].Count) return true;

        EntityIndex = 0;
        TableIndex++;

        while (TableIndex < Tables.Count && Tables[TableIndex].IsEmpty)
        {
            TableIndex++;
        }

        UpdateStorage();

        return TableIndex < Tables.Count && EntityIndex < Tables[TableIndex].Count;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Reset()
    {
        TableIndex = 0;
        EntityIndex = -1;

        UpdateStorage();
    }

    object IEnumerator.Current
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get => throw new Exception("Invalid Enumerator");
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Dispose()
    {
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    protected virtual void UpdateStorage()
    {
        throw new Exception("Invalid Enumerator");
    }
}

public class TriggerEnumerator<C> : TriggerEnumerator
{
    Trigger<C>[] _storage = default!;
    SystemList[] _systemLists = default!;
    readonly Type _systemType;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public TriggerEnumerator(List<Table> tables, Type systemType) : base(tables)
    {
        _systemType = systemType;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public new bool MoveNext()
    {
        if (TableIndex == Tables.Count) return false;

        EntityIndex++;

        while (Tables[TableIndex].Count > EntityIndex && _systemLists[EntityIndex].List.Contains(_systemType))
        {
            EntityIndex++;
        }

        if (EntityIndex < Tables[TableIndex].Count) return true;

        EntityIndex = 0;
        TableIndex++;

        while (TableIndex < Tables.Count && Tables[TableIndex].IsEmpty)
        {
            TableIndex++;
        }

        UpdateStorage();

        return TableIndex < Tables.Count && EntityIndex < Tables[TableIndex].Count;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    protected override void UpdateStorage()
    {
        if (TableIndex == Tables.Count) return;
        _storage = Tables[TableIndex].GetStorage<Trigger<C>>(Identity.None);
        _systemLists = Tables[TableIndex].GetStorage<SystemList>(Identity.None);
    }

    public C Current
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get
        {
            _systemLists[EntityIndex].List.Add(_systemType);
            return _storage[EntityIndex].Value;
        }
    }
}

public class Enumerator<C> : Enumerator
{
    C[] _storage = default!;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Enumerator(Archetypes archetypes, List<Table> tables) : base(archetypes, tables)
    {
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    protected override void UpdateStorage()
    {
        if (TableIndex == Tables.Count) return;
        _storage = Tables[TableIndex].GetStorage<C>(Identity.None);
    }

    public Ref<C> Current
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get => new(ref _storage[EntityIndex]);
    }
}

public class Enumerator<C1, C2> : Enumerator
{
    C1[] _storage1 = default!;
    C2[] _storage2 = default!;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Enumerator(Archetypes archetypes, List<Table> tables) : base(archetypes, tables)
    {
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    protected override void UpdateStorage()
    {
        if (TableIndex == Tables.Count) return;
        _storage1 = Tables[TableIndex].GetStorage<C1>(Identity.None);
        _storage2 = Tables[TableIndex].GetStorage<C2>(Identity.None);
    }

    public RefValueTuple<C1, C2> Current
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get => new(ref _storage1[EntityIndex], ref _storage2[EntityIndex]);
    }
}

public class Enumerator<C1, C2, C3> : Enumerator
{
    C1[] _storage1 = default!;
    C2[] _storage2 = default!;
    C3[] _storage3 = default!;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Enumerator(Archetypes archetypes, List<Table> tables) : base(archetypes, tables)
    {
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    protected override void UpdateStorage()
    {
        if (TableIndex == Tables.Count) return;
        _storage1 = Tables[TableIndex].GetStorage<C1>(Identity.None);
        _storage2 = Tables[TableIndex].GetStorage<C2>(Identity.None);
        _storage3 = Tables[TableIndex].GetStorage<C3>(Identity.None);
    }

    public RefValueTuple<C1, C2, C3> Current
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get => new(ref _storage1[EntityIndex], ref _storage2[EntityIndex], ref _storage3[EntityIndex]);
    }
}

public class Enumerator<C1, C2, C3, C4> : Enumerator
{
    C1[] _storage1 = default!;
    C2[] _storage2 = default!;
    C3[] _storage3 = default!;
    C4[] _storage4 = default!;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Enumerator(Archetypes archetypes, List<Table> tables) : base(archetypes, tables)
    {
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    protected override void UpdateStorage()
    {
        if (TableIndex == Tables.Count) return;
        _storage1 = Tables[TableIndex].GetStorage<C1>(Identity.None);
        _storage2 = Tables[TableIndex].GetStorage<C2>(Identity.None);
        _storage3 = Tables[TableIndex].GetStorage<C3>(Identity.None);
        _storage4 = Tables[TableIndex].GetStorage<C4>(Identity.None);
    }

    public RefValueTuple<C1, C2, C3, C4> Current
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get => new(ref _storage1[EntityIndex], ref _storage2[EntityIndex], ref _storage3[EntityIndex],
            ref _storage4[EntityIndex]);
    }
}

public class Enumerator<C1, C2, C3, C4, C5> : Enumerator
{
    C1[] _storage1 = default!;
    C2[] _storage2 = default!;
    C3[] _storage3 = default!;
    C4[] _storage4 = default!;
    C5[] _storage5 = default!;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Enumerator(Archetypes archetypes, List<Table> tables) : base(archetypes, tables)
    {
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    protected override void UpdateStorage()
    {
        if (TableIndex == Tables.Count) return;
        _storage1 = Tables[TableIndex].GetStorage<C1>(Identity.None);
        _storage2 = Tables[TableIndex].GetStorage<C2>(Identity.None);
        _storage3 = Tables[TableIndex].GetStorage<C3>(Identity.None);
        _storage4 = Tables[TableIndex].GetStorage<C4>(Identity.None);
        _storage5 = Tables[TableIndex].GetStorage<C5>(Identity.None);
    }

    public RefValueTuple<C1, C2, C3, C4, C5> Current
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get => new(ref _storage1[EntityIndex], ref _storage2[EntityIndex], ref _storage3[EntityIndex],
            ref _storage4[EntityIndex], ref _storage5[EntityIndex]);
    }
}

public class Enumerator<C1, C2, C3, C4, C5, C6> : Enumerator
{
    C1[] _storage1 = default!;
    C2[] _storage2 = default!;
    C3[] _storage3 = default!;
    C4[] _storage4 = default!;
    C5[] _storage5 = default!;
    C6[] _storage6 = default!;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Enumerator(Archetypes archetypes, List<Table> tables) : base(archetypes, tables)
    {
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    protected override void UpdateStorage()
    {
        if (TableIndex == Tables.Count) return;
        _storage1 = Tables[TableIndex].GetStorage<C1>(Identity.None);
        _storage2 = Tables[TableIndex].GetStorage<C2>(Identity.None);
        _storage3 = Tables[TableIndex].GetStorage<C3>(Identity.None);
        _storage4 = Tables[TableIndex].GetStorage<C4>(Identity.None);
        _storage5 = Tables[TableIndex].GetStorage<C5>(Identity.None);
        _storage6 = Tables[TableIndex].GetStorage<C6>(Identity.None);
    }

    public RefValueTuple<C1, C2, C3, C4, C5, C6> Current
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get => new(ref _storage1[EntityIndex], ref _storage2[EntityIndex], ref _storage3[EntityIndex],
            ref _storage4[EntityIndex], ref _storage5[EntityIndex], ref _storage6[EntityIndex]);
    }
}

public class Enumerator<C1, C2, C3, C4, C5, C6, C7> : Enumerator
{
    C1[] _storage1 = default!;
    C2[] _storage2 = default!;
    C3[] _storage3 = default!;
    C4[] _storage4 = default!;
    C5[] _storage5 = default!;
    C6[] _storage6 = default!;
    C7[] _storage7 = default!;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Enumerator(Archetypes archetypes, List<Table> tables) : base(archetypes, tables)
    {
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    protected override void UpdateStorage()
    {
        if (TableIndex == Tables.Count) return;
        _storage1 = Tables[TableIndex].GetStorage<C1>(Identity.None);
        _storage2 = Tables[TableIndex].GetStorage<C2>(Identity.None);
        _storage3 = Tables[TableIndex].GetStorage<C3>(Identity.None);
        _storage4 = Tables[TableIndex].GetStorage<C4>(Identity.None);
        _storage5 = Tables[TableIndex].GetStorage<C5>(Identity.None);
        _storage6 = Tables[TableIndex].GetStorage<C6>(Identity.None);
        _storage7 = Tables[TableIndex].GetStorage<C7>(Identity.None);
    }

    public RefValueTuple<C1, C2, C3, C4, C5, C6, C7> Current
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get => new(ref _storage1[EntityIndex], ref _storage2[EntityIndex], ref _storage3[EntityIndex],
            ref _storage4[EntityIndex], ref _storage5[EntityIndex], ref _storage6[EntityIndex],
            ref _storage7[EntityIndex]);
    }
}

public class Enumerator<C1, C2, C3, C4, C5, C6, C7, C8> : Enumerator
{
    C1[] _storage1 = default!;
    C2[] _storage2 = default!;
    C3[] _storage3 = default!;
    C4[] _storage4 = default!;
    C5[] _storage5 = default!;
    C6[] _storage6 = default!;
    C7[] _storage7 = default!;
    C8[] _storage8 = default!;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Enumerator(Archetypes archetypes, List<Table> tables) : base(archetypes, tables)
    {
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    protected override void UpdateStorage()
    {
        if (TableIndex == Tables.Count) return;
        _storage1 = Tables[TableIndex].GetStorage<C1>(Identity.None);
        _storage2 = Tables[TableIndex].GetStorage<C2>(Identity.None);
        _storage3 = Tables[TableIndex].GetStorage<C3>(Identity.None);
        _storage4 = Tables[TableIndex].GetStorage<C4>(Identity.None);
        _storage5 = Tables[TableIndex].GetStorage<C5>(Identity.None);
        _storage6 = Tables[TableIndex].GetStorage<C6>(Identity.None);
        _storage7 = Tables[TableIndex].GetStorage<C7>(Identity.None);
        _storage8 = Tables[TableIndex].GetStorage<C8>(Identity.None);
    }

    public RefValueTuple<C1, C2, C3, C4, C5, C6, C7, C8> Current
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get => new(ref _storage1[EntityIndex], ref _storage2[EntityIndex], ref _storage3[EntityIndex],
            ref _storage4[EntityIndex], ref _storage5[EntityIndex], ref _storage6[EntityIndex],
            ref _storage7[EntityIndex], ref _storage8[EntityIndex]);
    }
}

public class Enumerator<C1, C2, C3, C4, C5, C6, C7, C8, C9> : Enumerator
{
    C1[] _storage1 = default!;
    C2[] _storage2 = default!;
    C3[] _storage3 = default!;
    C4[] _storage4 = default!;
    C5[] _storage5 = default!;
    C6[] _storage6 = default!;
    C7[] _storage7 = default!;
    C8[] _storage8 = default!;
    C9[] _storage9 = default!;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Enumerator(Archetypes archetypes, List<Table> tables) : base(archetypes, tables)
    {
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    protected override void UpdateStorage()
    {
        if (TableIndex == Tables.Count) return;
        _storage1 = Tables[TableIndex].GetStorage<C1>(Identity.None);
        _storage2 = Tables[TableIndex].GetStorage<C2>(Identity.None);
        _storage3 = Tables[TableIndex].GetStorage<C3>(Identity.None);
        _storage4 = Tables[TableIndex].GetStorage<C4>(Identity.None);
        _storage5 = Tables[TableIndex].GetStorage<C5>(Identity.None);
        _storage6 = Tables[TableIndex].GetStorage<C6>(Identity.None);
        _storage7 = Tables[TableIndex].GetStorage<C7>(Identity.None);
        _storage8 = Tables[TableIndex].GetStorage<C8>(Identity.None);
        _storage9 = Tables[TableIndex].GetStorage<C9>(Identity.None);
    }

    public RefValueTuple<C1, C2, C3, C4, C5, C6, C7, C8, C9> Current
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get => new(ref _storage1[EntityIndex], ref _storage2[EntityIndex], ref _storage3[EntityIndex],
            ref _storage4[EntityIndex], ref _storage5[EntityIndex], ref _storage6[EntityIndex],
            ref _storage7[EntityIndex], ref _storage8[EntityIndex], ref _storage9[EntityIndex]);
    }
}