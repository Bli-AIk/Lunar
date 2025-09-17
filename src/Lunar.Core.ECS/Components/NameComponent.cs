namespace Lunar.Core.ECS.Components;

public struct NameComponent
{
    public NameComponent(string name)
    {
        Name = name;
    }

    public string Name;
}