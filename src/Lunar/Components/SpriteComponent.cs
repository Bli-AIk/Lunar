namespace Lunar.Components;

public struct SpriteComponent
{
    public SpriteComponent(Sprite sprite, string path)
    {
        Sprite = sprite;
        Path = path;
    }

    public Sprite Sprite;
    public string Path;
}