using System.Linq;
using System.Reflection.Emit;
using TMPro;
using UnityEngine;

public class Generator : MonoBehaviour
{
    [Header("Base Shape")]
    public Shape BaseShape { get; set; }
    public Material BaseShapeMaterial;

    [Header("Marker")]
    public GameObject MarkerPrefab;
    public float Density = 1;

    [Header("Label")]
    public GameObject TextMeshPrefab;
    public Camera CameraToLookAt;

    public GridShape gridShape { get; private set; }

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
        gridShape = ShapeExtension.Get(BaseShape);
        var baseShapeMr = gridShape.BaseShapePrefab.GetComponent<MeshRenderer>();
        baseShapeMr.material = BaseShapeMaterial;

        var parent = new GameObject();
        gridShape.BaseShapePrefab.transform.SetParent(parent.transform);
        //foreach (Vector3 point in gridShape.GetPoints(Density))
        foreach (
            var element in gridShape
                .GetPoints(Density)
                .Select((x, i) => new { Point = x, Index = i })
        )
        {
            Instantiate(MarkerPrefab, element.Point, Quaternion.identity, parent.transform);
            var label = Instantiate(
                    TextMeshPrefab,
                    element.Point + new Vector3(0, .1f, 0),
                    Quaternion.identity,
                    parent.transform
                )
                .GetComponent<TextMeshPro>();
            label.text = element.Index.ToString();
            label.GetComponent<LookAtCamera>().CameraToLookAt = CameraToLookAt;
        }

        // apply modifiers
        parent.transform.Translate(offset);
        parent.transform.Rotate(rotation);

        return parent;
    }
}
