using TMPro;
using UnityEngine;

public class MeasurementProcess : MonoBehaviour
{
    private GameObject currentGrid;

    [Header("UI Components")]
    public TMP_Text StatusLabel;

    [Header("Grid Generator")]
    public Generator GridGenerator;

    public bool IsProcessActive { get; set; } = false;

    private int _measuredPoints = 0;

    public void startProcess()
    {
        Debug.LogError("Starting measurement process startProcess()...");
        IsProcessActive = true;
        updateLabel();
    }

    public void endProcess()
    {
        Debug.LogError("Ending measurement process endProcess()...");
        IsProcessActive = false;
        reset();
    }

    public void increaseMeasuredPoints()
    {
        _measuredPoints += 1;
        updateLabel();
    }

    // ----------- helper methods -----------

    private void reset()
    {
        _measuredPoints = 0;
        updateLabel();
        foreach (var elm in currentGrid.GetComponentsInChildren<MeasurementPoint>())
        {
            elm.setUnmeasured();
        }
    }

    private void updateLabel()
    {
        var density = 1f;
        StatusLabel.text =
            "Measured Points: "
            + _measuredPoints
            + "/"
            + GridGenerator.gridShape.GetPoints(density).Count;
    }

    public void setCurrentGrid(GameObject grid)
    {
        currentGrid = grid;
    }
}
