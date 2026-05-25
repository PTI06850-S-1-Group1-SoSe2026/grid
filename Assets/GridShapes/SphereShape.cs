using System.Collections.Generic;
using UnityEngine;

public class SphereShape : GridShape
{
    private GameObject _baseShape = GameObject.CreatePrimitive(PrimitiveType.Sphere);

    public override GameObject BaseShapePrefab { get { return _baseShape; } }

    public override List<Vector3> GetPoints(float density)
    {
        return new List<Vector3>();
    }
}
