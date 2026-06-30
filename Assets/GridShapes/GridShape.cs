using System.Collections.Generic;
using UnityEngine;

public abstract class GridShape : ScriptableObject
{
    public abstract GameObject BaseShapePrefab { get; }

    public abstract List<Vector3> GetPoints(float density);
}
