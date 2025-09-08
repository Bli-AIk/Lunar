using Arch.Core;
using Arch.System;

namespace Lunar.Core.ECS;

public abstract class GameControllerBase
{
    protected World MainWorld = null!;
    protected Group<float> Systems = null!;

    public void Initialize()
    {
        MainWorld = World.Create();
        Systems = CreateSystems(MainWorld);

        SetEvents(MainWorld);
        Systems.Initialize();
    }

    public void Update(float deltaTime)
    {
        Systems.BeforeUpdate(in deltaTime);
        Systems.Update(in deltaTime);
    }

    public void LateUpdate(float deltaTime)
    {
        Systems.AfterUpdate(in deltaTime);
    }

    public void Dispose()
    {
        Systems.Dispose();
        MainWorld.Dispose();
    }

    /// <summary>
    ///     Define the system to register
    /// </summary>
    protected abstract Group<float> CreateSystems(World world);

    /// <summary>
    ///     Define event binding
    /// </summary>
    protected abstract void SetEvents(World world);
}