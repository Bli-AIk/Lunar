using System;

namespace Lunar.Core.Base;

public class GameObjectHandle
{
    public GameObjectHandle(object nativeGameObject)
    {
        NativeGameObject = nativeGameObject ?? throw new ArgumentNullException(nameof(nativeGameObject));
    }

    public object NativeGameObject { get; private set; }
}