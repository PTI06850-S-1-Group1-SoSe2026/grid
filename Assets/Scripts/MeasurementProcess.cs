using TMPro;
using UnityEngine;

public class MeasurementProcess : MonoBehaviour
{
    [Header("UI Components")]
    public TMP_Text StatusLabel;

    [Header("Grid Generator")]
    public Generator GridGenerator;

    public bool IsProcessActive { get; set; } = false;

    private int _measuredPoints = 0;

    public void startProcess()
    {
        IsProcessActive = true;
        updateLabel();
    }

    public void endProcess()
    {
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
        // TODO: reset colors of all measurement points
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
}
