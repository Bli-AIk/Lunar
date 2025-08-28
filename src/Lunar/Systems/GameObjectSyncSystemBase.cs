using Arch.Core;
using Arch.System;
using Lunar.Components;

namespace Lunar.Systems
{
    public abstract class GameObjectSyncSystemBase : BaseSystem<World, float>
    {
        protected GameObjectSyncSystemBase(World world) : base(world) { }

        protected abstract void ApplyTransform(GameObjectComponent gameObject, PositionComponent position);

        public override void Update(in float deltaTime)
        {
            var query = new QueryDescription().WithAll<GameObjectComponent, PositionComponent>();
            World.Query(in query,
                (Entity entity, ref GameObjectComponent gameObject, ref PositionComponent position) =>
                {
                    ApplyTransform(gameObject, position);
                });
        }
    }
}