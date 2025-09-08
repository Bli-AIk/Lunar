namespace Lunar.Core.Components;

public struct SpriteComponent
{
    public SpriteComponent(string path)
    {
        Path = path;
        Sprite = null;
    
        LastLoadPath = null;
    }

    public string Path;
    public Sprite? Sprite;
    public string? LastLoadPath { get; private set; }

    public void SetLastLoadPath(string? lastLoadPath)
    {
        LastLoadPath = lastLoadPath;
    }
}