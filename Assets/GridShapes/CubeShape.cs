using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Vector3 = UnityEngine.Vector3;

class Vector3Comparator : IEqualityComparer<Vector3>
{
    readonly float epsilon = .1f;

    public bool Equals(Vector3 x, Vector3 y)
    {
        return Mathf.Abs(x.x - y.x) <= epsilon && Mathf.Abs(x.y - y.y) <= epsilon && Mathf.Abs(x.z - y.z) <= epsilon;
    }

    public int GetHashCode(Vector3 obj)
    {
        int x = Mathf.RoundToInt(obj.x / epsilon);
        int y = Mathf.RoundToInt(obj.y / epsilon);
        int z = Mathf.RoundToInt(obj.z / epsilon);

        return HashCode.Combine(x, y, z);
    }
}

public class CubeShape : GridShape
{
    private readonly GameObject _baseShape = GameObject.CreatePrimitive(PrimitiveType.Cube);

    public override GameObject BaseShapePrefab { get { return _baseShape; } }

    public override List<Vector3> GetPoints(float density)
    {
        var points = new List<Vector3>();

        var renderer = _baseShape.GetComponent<Renderer>();
        var bounds = renderer.bounds.size;
        var pivot = renderer.bounds.center;

        for (float i = 0; i <= bounds.y; i += bounds.y / density)
        {
            for (float f = 0; f <= bounds.x; f += bounds.x / density)
            {
                foreach (float side in new float[2] { -bounds.z / 2, bounds.z / 2 })
                {
                    // add points on opposite sides
                    var pointSet = new List<Vector3>()
                    {
                        (i < bounds.y && f < bounds.x)
                          ? new Vector3(i - (bounds.x / 2) + (bounds.x / density / 2), f - (bounds.y / 2) + (bounds.x / density / 2), side)
                          : new Vector3(),
                        new(i - (bounds.x / 2), f - (bounds.y / 2), side)
                    };
                    points.AddRange(pointSet);

                    // add other sides
                    foreach (Vector3 point in pointSet)
                    {
                        points.AddRange(new Vector3[] { Quaternion.AngleAxis(90f, Vector3.up) * (point - pivot) + pivot, Quaternion.AngleAxis(90f, Vector3.right) * (point - pivot) + pivot });
                    }
                }
            }
        }

        points = points.Distinct(new Vector3Comparator()).ToList();

        return points;
    }

    public List<Vector3> GetPoints(float density, bool top, bool bottom, bool left, bool right, bool front, bool back)
    {
        var points = this.GetPoints(density);

        var renderer = _baseShape.GetComponent<Renderer>();
        var bounds = renderer.bounds.size;

        if (!top)
            points.RemoveAll(p => p.y >= (bounds.y / 2) - 0.001);

        if (!bottom)
            points.RemoveAll(p => p.y <= (-bounds.y / 2) + 0.001);

        if (!left)
            points.RemoveAll(p => p.x <= (-bounds.x / 2) + 0.001);

        if (!right)
            points.RemoveAll(p => p.x >= (bounds.x / 2) - 0.001);

        if (!front)
            points.RemoveAll(p => p.z >= (bounds.z / 2) - 0.001);

        if (!back)
            points.RemoveAll(p => p.z <= (-bounds.z / 2) + 0.001);

        return points;
    }
}
