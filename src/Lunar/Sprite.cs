using System;

namespace Lunar;

public class Sprite
{
    public Sprite(object baseSprite)
    {
        BaseSprite = baseSprite ?? throw new ArgumentNullException(nameof(baseSprite));
    }
    
    public object BaseSprite { get; private set; }
}