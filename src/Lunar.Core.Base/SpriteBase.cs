using System;

namespace Lunar.Core.Base;

public class SpriteBase
{
    public SpriteBase(object baseSprite)
    {
        BaseSprite = baseSprite ?? throw new ArgumentNullException(nameof(baseSprite));
    }
    
    public object BaseSprite { get; private set; }
}