using System.Numerics;

namespace Lunar.Core.Components;

public struct TransformComponent
{
    public Vector3 Position;
    public Quaternion Rotation;
    public Vector3 Scale = Vector3.One;

    public TransformComponent()
        : this(Vector3.Zero, Quaternion.Identity, Vector3.One) { }

    public TransformComponent(Vector3 position)
        : this(position, Quaternion.Identity, Vector3.One) { }

    public TransformComponent(Vector3 position, Quaternion rotation)
        : this(position, rotation, Vector3.One) { }

    public TransformComponent(Vector3 position, Vector3 scale)
        : this(position, Quaternion.Identity, scale) { }

    public TransformComponent(Vector3 position, Quaternion rotation, Vector3 scale)
    {
        Position = position;
        Rotation = rotation;
        Scale = scale;
    }
}