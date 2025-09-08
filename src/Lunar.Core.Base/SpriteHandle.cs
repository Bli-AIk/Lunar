using System;

namespace Lunar.Core.Base;

public class SpriteHandle
{
    public SpriteHandle(object nativeSprite)
    {
        NativeSprite = nativeSprite ?? throw new ArgumentNullException(nameof(nativeSprite));
    }
    
    public object NativeSprite { get; private set; }
}