using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace RelEcs;

public readonly ref struct Ref<T>
{
    readonly Span<T> _span;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ref(ref T r)
    {
        _span = MemoryMarshal.CreateSpan(ref r, 1);
    }

    public ref T Value
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get => ref MemoryMarshal.GetReference(_span);
    }
}

// TODO: Change Ref<T> to this when NET7 hits the fan for great perf boost
// public readonly ref struct Ref<T>
// {
//     public readonly ref T Value;
//
//     [MethodImpl(MethodImplOptions.AggressiveInlining)]
//     public Ref(ref T r)
//     {
//         Value = ref r;
//     }
// }

public ref struct RefValueTuple<T1, T2>
{
    public Ref<T1> Item1;
    public Ref<T2> Item2;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RefValueTuple(ref T1 item1, ref T2 item2)
    {
        Item1 = new Ref<T1>(ref item1);
        Item2 = new Ref<T2>(ref item2);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Deconstruct(out Ref<T1> item1, out Ref<T2> item2)
    {
        item1 = Item1;
        item2 = Item2;
    }
}

public ref struct RefValueTuple<T1, T2, T3>
{
    public Ref<T1> Item1;
    public Ref<T2> Item2;
    public Ref<T3> Item3;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RefValueTuple(ref T1 item1, ref T2 item2, ref T3 item3)
    {
        Item1 = new Ref<T1>(ref item1);
        Item2 = new Ref<T2>(ref item2);
        Item3 = new Ref<T3>(ref item3);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Deconstruct(out Ref<T1> item1, out Ref<T2> item2, out Ref<T3> item3)
    {
        item1 = Item1;
        item2 = Item2;
        item3 = Item3;
    }
}

public ref struct RefValueTuple<T1, T2, T3, T4>
{
    public Ref<T1> Item1;
    public Ref<T2> Item2;
    public Ref<T3> Item3;
    public Ref<T4> Item4;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RefValueTuple(ref T1 item1, ref T2 item2, ref T3 item3, ref T4 item4)
    {
        Item1 = new Ref<T1>(ref item1);
        Item2 = new Ref<T2>(ref item2);
        Item3 = new Ref<T3>(ref item3);
        Item4 = new Ref<T4>(ref item4);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Deconstruct(out Ref<T1> item1, out Ref<T2> item2, out Ref<T3> item3, out Ref<T4> item4)
    {
        item1 = Item1;
        item2 = Item2;
        item3 = Item3;
        item4 = Item4;
    }
}

public ref struct RefValueTuple<T1, T2, T3, T4, T5>
{
    public Ref<T1> Item1;
    public Ref<T2> Item2;
    public Ref<T3> Item3;
    public Ref<T4> Item4;
    public Ref<T5> Item5;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RefValueTuple(ref T1 item1, ref T2 item2, ref T3 item3, ref T4 item4, ref T5 item5)
    {
        Item1 = new Ref<T1>(ref item1);
        Item2 = new Ref<T2>(ref item2);
        Item3 = new Ref<T3>(ref item3);
        Item4 = new Ref<T4>(ref item4);
        Item5 = new Ref<T5>(ref item5);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Deconstruct(out Ref<T1> item1, out Ref<T2> item2, out Ref<T3> item3, out Ref<T4> item4,
        out Ref<T5> item5)
    {
        item1 = Item1;
        item2 = Item2;
        item3 = Item3;
        item4 = Item4;
        item5 = Item5;
    }
}

public ref struct RefValueTuple<T1, T2, T3, T4, T5, T6>
{
    public Ref<T1> Item1;
    public Ref<T2> Item2;
    public Ref<T3> Item3;
    public Ref<T4> Item4;
    public Ref<T5> Item5;
    public Ref<T6> Item6;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RefValueTuple(ref T1 item1, ref T2 item2, ref T3 item3, ref T4 item4, ref T5 item5, ref T6 item6)
    {
        Item1 = new Ref<T1>(ref item1);
        Item2 = new Ref<T2>(ref item2);
        Item3 = new Ref<T3>(ref item3);
        Item4 = new Ref<T4>(ref item4);
        Item5 = new Ref<T5>(ref item5);
        Item6 = new Ref<T6>(ref item6);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Deconstruct(out Ref<T1> item1, out Ref<T2> item2, out Ref<T3> item3, out Ref<T4> item4,
        out Ref<T5> item5, out Ref<T6> item6)
    {
        item1 = Item1;
        item2 = Item2;
        item3 = Item3;
        item4 = Item4;
        item5 = Item5;
        item6 = Item6;
    }
}

public ref struct RefValueTuple<T1, T2, T3, T4, T5, T6, T7>
{
    public Ref<T1> Item1;
    public Ref<T2> Item2;
    public Ref<T3> Item3;
    public Ref<T4> Item4;
    public Ref<T5> Item5;
    public Ref<T6> Item6;
    public Ref<T7> Item7;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RefValueTuple(ref T1 item1, ref T2 item2, ref T3 item3, ref T4 item4, ref T5 item5, ref T6 item6,
        ref T7 item7)
    {
        Item1 = new Ref<T1>(ref item1);
        Item2 = new Ref<T2>(ref item2);
        Item3 = new Ref<T3>(ref item3);
        Item4 = new Ref<T4>(ref item4);
        Item5 = new Ref<T5>(ref item5);
        Item6 = new Ref<T6>(ref item6);
        Item7 = new Ref<T7>(ref item7);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Deconstruct(out Ref<T1> item1, out Ref<T2> item2, out Ref<T3> item3, out Ref<T4> item4,
        out Ref<T5> item5, out Ref<T6> item6, out Ref<T7> item7)
    {
        item1 = Item1;
        item2 = Item2;
        item3 = Item3;
        item4 = Item4;
        item5 = Item5;
        item6 = Item6;
        item7 = Item7;
    }
}

public ref struct RefValueTuple<T1, T2, T3, T4, T5, T6, T7, T8>
{
    public Ref<T1> Item1;
    public Ref<T2> Item2;
    public Ref<T3> Item3;
    public Ref<T4> Item4;
    public Ref<T5> Item5;
    public Ref<T6> Item6;
    public Ref<T7> Item7;
    public Ref<T8> Item8;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RefValueTuple(ref T1 item1, ref T2 item2, ref T3 item3, ref T4 item4, ref T5 item5, ref T6 item6,
        ref T7 item7, ref T8 item8)
    {
        Item1 = new Ref<T1>(ref item1);
        Item2 = new Ref<T2>(ref item2);
        Item3 = new Ref<T3>(ref item3);
        Item4 = new Ref<T4>(ref item4);
        Item5 = new Ref<T5>(ref item5);
        Item6 = new Ref<T6>(ref item6);
        Item7 = new Ref<T7>(ref item7);
        Item8 = new Ref<T8>(ref item8);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Deconstruct(out Ref<T1> item1, out Ref<T2> item2, out Ref<T3> item3, out Ref<T4> item4,
        out Ref<T5> item5, out Ref<T6> item6, out Ref<T7> item7, out Ref<T8> item8)
    {
        item1 = Item1;
        item2 = Item2;
        item3 = Item3;
        item4 = Item4;
        item5 = Item5;
        item6 = Item6;
        item7 = Item7;
        item8 = Item8;
    }
}

public ref struct RefValueTuple<T1, T2, T3, T4, T5, T6, T7, T8, T9>
{
    public Ref<T1> Item1;
    public Ref<T2> Item2;
    public Ref<T3> Item3;
    public Ref<T4> Item4;
    public Ref<T5> Item5;
    public Ref<T6> Item6;
    public Ref<T7> Item7;
    public Ref<T8> Item8;
    public Ref<T9> Item9;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RefValueTuple(ref T1 item1, ref T2 item2, ref T3 item3, ref T4 item4, ref T5 item5, ref T6 item6,
        ref T7 item7, ref T8 item8, ref T9 item9)
    {
        Item1 = new Ref<T1>(ref item1);
        Item2 = new Ref<T2>(ref item2);
        Item3 = new Ref<T3>(ref item3);
        Item4 = new Ref<T4>(ref item4);
        Item5 = new Ref<T5>(ref item5);
        Item6 = new Ref<T6>(ref item6);
        Item7 = new Ref<T7>(ref item7);
        Item8 = new Ref<T8>(ref item8);
        Item9 = new Ref<T9>(ref item9);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Deconstruct(out Ref<T1> item1, out Ref<T2> item2, out Ref<T3> item3, out Ref<T4> item4,
        out Ref<T5> item5, out Ref<T6> item6, out Ref<T7> item7, out Ref<T8> item8, out Ref<T9> item9)
    {
        item1 = Item1;
        item2 = Item2;
        item3 = Item3;
        item4 = Item4;
        item5 = Item5;
        item6 = Item6;
        item7 = Item7;
        item8 = Item8;
        item9 = Item9;
    }
}