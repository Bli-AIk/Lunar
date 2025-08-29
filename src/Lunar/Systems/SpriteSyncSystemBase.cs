using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Arch.Core;
using Arch.System;
using Lunar.Components;

namespace Lunar.Systems;

public abstract class SpriteSyncSystemBase : BaseSystem<World, float>
{
    private bool _isProcessing;

    protected SpriteSyncSystemBase(World world) : base(world) { }

    protected abstract Task<SpriteComponent> SyncSprite(SpriteComponent sprite);

    public override void Update(in float deltaTime)
    {
        var pending = new List<(Entity entity, SpriteComponent sprite)>();
        var query = new QueryDescription().WithAll<GameObjectComponent, SpriteComponent>();
        World.Query(in query,
            (Entity entity, ref GameObjectComponent gameObject, ref SpriteComponent sprite) =>
            {
                pending.Add((entity, sprite));
            });

        if (pending.Count == 0)
        {
            return;
        }

        if (_isProcessing)
        {
            return;
        }

        _isProcessing = true;
        _ = ProcessPendingAsync(pending);
    }

    private async Task ProcessPendingAsync(List<(Entity entity, SpriteComponent sprite)> pending)
    {
        try
        {
            var tasks = pending.Select(async item =>
            {
                var newSprite = await SyncSprite(item.sprite).ConfigureAwait(false);
                return item with { sprite = newSprite };
            }).ToArray();

            var results = await Task.WhenAll(tasks).ConfigureAwait(false);

            var map = results.ToDictionary(r => r.entity, r => r.sprite);

            var query = new QueryDescription().WithAll<GameObjectComponent, SpriteComponent>();
            World.Query(in query, (Entity entity, ref GameObjectComponent gameObject, ref SpriteComponent sprite) =>
            {
                if (map.TryGetValue(entity, out var newSprite))
                {
                    sprite = newSprite;
                }
            });
        }
        finally
        {
            _isProcessing = false;
        }
    }
}