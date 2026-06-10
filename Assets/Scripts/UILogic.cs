using Oculus.Interaction.Samples;
using UnityEngine;
using UnityEngine.UI;

public class UILogic : MonoBehaviour
{
    [Header("UI Drop Down")]
    public DropDownGroup dropDownGroup;

    [Header("Measurement Process")]
    public MeasurementProcess measurementProcess;
    public Button startMeasuringButton;

    [Header("Grid Generator")]
    public Generator gridGenerator;

    public void Start()
    {
        dropDownGroup.WhenSelectionChanged.AddListener(OnDropDownChanged);
        startMeasuringButton.onClick.AddListener(OnStartMeasuringButtonClick);
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

    private void OnStartMeasuringButtonClick()
    {
        Debug.LogError("Starting measurement process...");
        measurementProcess.startProcess();
    }
}
