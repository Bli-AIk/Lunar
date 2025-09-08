using Lunar.Core.Base;

namespace Lunar.Core.ECS.Components;

public struct GameObjectComponent
{
    public GameObjectComponent(GameObject gameObject)
    {
        GameObject = gameObject;
    }

    public GameObject GameObject;
}