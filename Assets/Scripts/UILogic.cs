using Oculus.Interaction.Samples;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UILogic : MonoBehaviour
{
    [Header("UI Drop Down")]
    public DropDownGroup dropDownGroup;

    [Header("Measurement Process")]
    public MeasurementProcess measurementProcess;
    public Button startMeasuringButton;
    
    [Header("Measurement Button Icon")]
    [SerializeField] private Image buttonIcon;
    [SerializeField] private TMP_Text buttonLabel;
    [SerializeField] public Sprite startIcon;
	[SerializeField] public Sprite stopIcon;

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
        measurementProcess.endProcess();
        switchButtonVisualsToStart();

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
		Debug.LogError("Registered Button Click");
		if (measurementProcess.IsProcessActive){
			Debug.LogError("Ending measurement process...");
			measurementProcess.endProcess();
			switchButtonVisualsToStart();
		} else {
        	Debug.LogError("Starting measurement process...");
        	measurementProcess.startProcess();
        	buttonLabel.text = "Stop Measuring";
        	buttonIcon.sprite = stopIcon;
        }
    }
    
    private void switchButtonVisualsToStart() {
		buttonLabel.text = "Start Measuring";
        buttonIcon.sprite = startIcon;
	}
}
