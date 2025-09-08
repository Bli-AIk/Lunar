namespace Lunar.Core.Components;

public struct GameObjectComponent
{
    public GameObjectComponent(GameObject gameObject)
    {
        GameObject = gameObject;
    }

    public GameObject GameObject;
}