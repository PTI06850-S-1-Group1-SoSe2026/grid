using System;

public enum Shape
{
    Cube,
    Sphere
}

static class ShapeExtension
{
    public static GridShape Get(Shape shape)
    {
        return shape switch
        {
            Shape.Cube => new CubeShape(),
            Shape.Sphere => new SphereShape(),
            _ => throw new NotImplementedException()
        };
    }
}