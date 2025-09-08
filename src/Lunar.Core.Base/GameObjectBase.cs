using System;

namespace Lunar.Core.Base;

public class GameObjectBase
{
    public GameObjectBase(object baseGameObject)
    {
        BaseGameObject = baseGameObject ?? throw new ArgumentNullException(nameof(baseGameObject));
    }

    public object BaseGameObject { get; private set; }
}