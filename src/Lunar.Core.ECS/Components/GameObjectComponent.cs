using Lunar.Core.Base;

namespace Lunar.Core.ECS.Components;

public struct GameObjectComponent
{
    public GameObjectComponent(GameObjectBase gameObjectBase)
    {
        GameObjectBase = gameObjectBase;
    }

    public GameObjectBase GameObjectBase;
}