using System.Collections.Generic;
using UnityEngine;

public class SphereShape : GridShape
{
    private GameObject _baseShape = GameObject.CreatePrimitive(PrimitiveType.Sphere);

    public SphereShape()
    {
        _baseShape.transform.localScale = new(2, 2, 2);
    }

    public override GameObject BaseShapePrefab
    {
        get { return _baseShape; }
    }

    public override List<Vector3> GetPoints(float density)
    {
        return new List<Vector3>()
        {
            new(.16f, .22f, -.96f),
            new(.78f, .20f, -.60f),
            new(.78f, .31f, .55f),
            new(.16f, .41f, .90f),
            new(-.83f, .45f, .32f),
            new(-.83f, .38f, -.40f),
            new(-.26f, .71f, -.65f),
            new(.74f, .67f, -.07f),
            new(-.26f, .83f, .50f),
            new(.10f, .99f, -.10f),
            new(.91f, .22f, -.34f),
            new(.91f, .20f, .38f),
            new(-.09f, .31f, .95f),
            new(-.70f, .41f, .59f),
            new(-.69f, .45f, -.56f),
            new(-.07f, .38f, -.92f),
            new(.43f, .71f, -.55f),
            new(.43f, .67f, .61f),
            new(-.56f, .83f, .02f),
            new(.14f, .99f, .04f),
        };
    }
}
