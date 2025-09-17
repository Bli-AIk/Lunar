using System;

namespace Lunar;

public class GameObject
{
    public GameObject(object baseGameObject)
    {
        BaseGameObject = baseGameObject ?? throw new ArgumentNullException(nameof(baseGameObject));
    }

    public object BaseGameObject { get; private set; }
}