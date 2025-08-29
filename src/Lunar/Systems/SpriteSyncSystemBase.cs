using Arch.Core;
using Arch.System;
using Lunar.Components;

namespace Lunar.Systems;

public abstract class SpriteSyncSystemBase : BaseSystem<World, float>
{
    protected SpriteSyncSystemBase(World world) : base(world) { }
    
    protected abstract void SyncSprite(SpriteComponent sprite);

    public override void Update(in float deltaTime) 
    {
        var query = new QueryDescription().WithAll<GameObjectComponent, SpriteComponent>();
        World.Query(in query,
            (Entity entity, ref GameObjectComponent gameObject, ref SpriteComponent sprite) =>
            {
                SyncSprite(sprite);
            });
    }
    
}