using System;
using System.Collections.Generic;

namespace HypEcs;

internal class Trigger<T>
{
    internal T Value;
}

internal struct SystemList
{
    public readonly List<Type> List;
    public SystemList() => List = ListPool<Type>.Get();
}

internal struct LifeTime
{
    public int Value;
}

internal class TriggerLifeTimeSystem : ISystem
{
    public World World { get; set; }

    public void Run()
    {
        var query = World.Query<Entity, SystemList, LifeTime>().Build();
        foreach (var (entity, systemList, lifeTime) in query)
        {
            lifeTime.Value.Value++;
                
            if (lifeTime.Value.Value < 2) return;
                
            ListPool<Type>.Add(systemList.Value.List);
            World.Despawn(entity.Value);
        }
    }
}