using UnityEngine;

public class GridGenerator : MonoBehaviour
{
    [Header("Base Shape")]
    public Shape BaseShape;
    public Material BaseShapeMaterial;

    [Header("Marker")]
    public GameObject MarkerPrefab;
    public float Density = 1;


    public void GenerateGrid(Vector3 offset)
    {
        var gridShape = (CubeShape)ShapeExtension.Get(BaseShape);
        var baseShapeMr = gridShape.BaseShapePrefab.GetComponent<MeshRenderer>();
        baseShapeMr.material = BaseShapeMaterial;

        var parent = new GameObject();
        foreach (Vector3 point in gridShape.GetPoints(Density, true, false, true, true, true, true))
        {
            Instantiate(MarkerPrefab, point, Quaternion.identity, parent.transform);
        }
        parent.transform.Translate(offset);
    }
}
