using Oculus.Interaction.Samples;
using UnityEngine;

public class UILogic : MonoBehaviour
{
    [Header("UI Drop Down")]
    public DropDownGroup dropDownGroup;

    [Header("Grid Generator")]
    public Generator gridGenerator;

    public void Start()
    {
        dropDownGroup.WhenSelectionChanged.AddListener(OnDropDownChanged);
    }

    private void OnDropDownChanged(int index)
    {
        // INFO: This method uses hard coded integers to determine which Shape should be set.

        if (index == 0)
        {
            gridGenerator.BaseShape = Shape.Cube;
        }
        else if (index == 1)
        {
            gridGenerator.BaseShape = Shape.Sphere;
        }
    }
}
