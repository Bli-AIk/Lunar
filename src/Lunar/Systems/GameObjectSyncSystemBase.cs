using Arch.Core;
using Arch.Core.Extensions;
using Arch.System;
using Lunar.Components;

namespace Lunar.Systems
{
    public abstract class GameObjectSyncSystemBase : BaseSystem<World, float>
    {
        protected GameObjectSyncSystemBase(World world) : base(world) { }

        protected abstract void SyncTransform(GameObjectComponent gameObject, PositionComponent position);
        protected abstract void SyncName(GameObjectComponent gameObject, NameComponent name);

        public override void Update(in float deltaTime)
        {
            var query = new QueryDescription().WithAll<GameObjectComponent, PositionComponent>();
            World.Query(in query,
                (Entity entity, ref GameObjectComponent gameObject, ref PositionComponent position) =>
                {
                    SyncTransform(gameObject, position);
                    
                    if (entity.Has<NameComponent>())
                    {
                        SyncName(gameObject, entity.Get<NameComponent>());
                    }
                });
        }
    }
}