using Arch.Core;
using Arch.Core.Extensions;
using Arch.System;
using Lunar.Core.ECS.Components;

namespace Lunar.Core.ECS.Systems;

public abstract class GameObjectSyncSystemBase : BaseSystem<World, float>
{
    protected GameObjectSyncSystemBase(World world) : base(world) { }

    protected abstract void SyncTransform(GameObjectComponent gameObject, TransformComponent transform);
    protected abstract void SyncName(GameObjectComponent gameObject, NameComponent name);

    public override void Update(in float deltaTime)
    {
        var query = new QueryDescription().WithAll<GameObjectComponent, TransformComponent>();
        World.Query(in query,
            (Entity entity, ref GameObjectComponent gameObject, ref TransformComponent transform) =>
            {
                SyncTransform(gameObject, transform);

                if (entity.Has<NameComponent>())
                {
                    SyncName(gameObject, entity.Get<NameComponent>());
                }
            });
    }
}