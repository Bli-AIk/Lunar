using Arch.Core;
using Arch.Core.Extensions;
using Arch.System;
using Lunar.Core.ECS.Components;

namespace Lunar.Core.ECS.Systems;

public abstract class SpriteCleanSystemBase : BaseSystem<World, float>
{
    protected SpriteCleanSystemBase(World world) : base(world) { }
    protected abstract void CleanSprite(SpriteComponent sprite);


    public override void AfterUpdate(in float deltaTime)
    {
        var query = new QueryDescription().WithNone<GameObjectComponent>().WithAll<SpriteComponent>();
        World.Query(in query,
            (Entity entity, ref SpriteComponent sprite) =>
            {
                CleanSprite(sprite);
                entity.Remove<SpriteComponent>();
            });
    }
}