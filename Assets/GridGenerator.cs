using UnityEngine;

public class Generator : MonoBehaviour
{
    [Header("Base Shape")]
    public Shape BaseShape;
    public Material BaseShapeMaterial;

    [Header("Marker")]
    public GameObject MarkerPrefab;
    public float Density = 1;

    public void GenerateGrid()
    {
        GenerateGrid(new Vector3());
    }

    public void GenerateGrid(Vector3 offset)
    {
        GenerateGrid(offset, new Vector3());
    }


    public void GenerateGrid(Vector3 offset, Vector3 rotation)
    {
        var gridShape = (CubeShape)ShapeExtension.Get(BaseShape);
        var baseShapeMr = gridShape.BaseShapePrefab.GetComponent<MeshRenderer>();
        baseShapeMr.material = BaseShapeMaterial;

        var parent = new GameObject();
        foreach (Vector3 point in gridShape.GetPoints(Density, true, false, true, true, true, true))
        {
            Instantiate(MarkerPrefab, point, Quaternion.identity, parent.transform);
        }

        // apply modifiers
        parent.transform.Translate(offset);
        gridShape.BaseShapePrefab.transform.Translate(offset);

        parent.transform.Rotate(rotation);
        gridShape.BaseShapePrefab.transform.Rotate(rotation);
    }
}
