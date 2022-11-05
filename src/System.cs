using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace HypEcs;

public interface ISystem
{
    World World { get; set; }
    void Run();
}

public static class SystemExtensions
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void Run(this ISystem system, World world)
    {
        var stopWatch = new Stopwatch();
        stopWatch.Start();

        system.World = world;
        system.Run();

        stopWatch.Stop();
        world.SystemExecutionTimes.Add((system.GetType(), stopWatch.Elapsed));
    }
}

public sealed class SystemGroup
{
    readonly List<ISystem> _systems = new();

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public SystemGroup Add(ISystem aSystem)
    {
        _systems.Add(aSystem);
        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Run(World world)
    {
        foreach (var system in _systems)
        {
            system.Run(world);
        }
    }
}