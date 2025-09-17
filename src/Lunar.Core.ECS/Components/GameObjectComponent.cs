using Lunar.Core.Base;

namespace Lunar.Core.ECS.Components;

public struct GameObjectComponent
{
    public GameObjectComponent(GameObjectHandle gameObjectHandle)
    {
        GameObjectHandle = gameObjectHandle;
    }

    public GameObjectHandle GameObjectHandle;
}