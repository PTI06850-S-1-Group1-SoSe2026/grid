using UnityEngine;

public class Generator : MonoBehaviour
{
    public Shape BaseShape { get; set; }

    [Header("Base Shape")]
    public Material BaseShapeMaterial;

    [Header("Marker")]
    public GameObject MarkerPrefab;
    public float Density = 1;

    public GameObject GenerateGrid()
    {
        return GenerateGrid(new Vector3());
    }

    public GameObject GenerateGrid(Vector3 offset)
    {
        return GenerateGrid(offset, new Vector3());
    }

    public GameObject GenerateGrid(Vector3 offset, Vector3 rotation)
    {
        var gridShape = ShapeExtension.Get(BaseShape);
        var baseShapeMr = gridShape.BaseShapePrefab.GetComponent<MeshRenderer>();
        baseShapeMr.material = BaseShapeMaterial;

        var parent = new GameObject();
        gridShape.BaseShapePrefab.transform.SetParent(parent.transform);
        foreach (Vector3 point in gridShape.GetPoints(Density))
        {
            Instantiate(MarkerPrefab, point, Quaternion.identity, parent.transform);
        }

        // apply modifiers
        parent.transform.Translate(offset);
        parent.transform.Rotate(rotation);

        return parent;
    }
}
