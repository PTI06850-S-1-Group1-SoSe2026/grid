using System;
using System.Collections.Generic;
using Meta.XR.MRUtilityKit;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

/**
 * Fixing the indicators works as follows:
 * If QR1 has been recognized already and the controller trigger gets triggered,
 * its indicator gets fixed within the world. After that, the same thing happens
 * to the indicator of QR2, if the button gets triggered again.
 */
public class TrackablesManager : MonoBehaviour
{
    private GameObject currentGrid;

    public enum PlacementState
    {
        None,
        QR1Fixed,
        QR2Fixed,
    }

    [SerializeField]
    private GameObject trackedObjectPrefab;

    [Header("UI Controls")]
    public Button ResetFixationButton;
    public TMP_Text FixationStatusLabel;

    private Dictionary<string, GameObject> trackedObjects = new();

    public static string QR1_NAME = "QR_1";
    public static string QR2_NAME = "QR_2";

    private Transform qr1IndicatorParent;
    private Transform qr2IndicatorParent;

    private PlacementState currentState = PlacementState.None;

    void Update()
    {
        if (OVRInput.GetDown(OVRInput.RawButton.B))
            HandlePlacement();

        if (OVRInput.GetDown(OVRInput.RawButton.A))
            CalculateAndPlaceGrid();
    }

    public void Start()
    {
        ResetFixationButton.onClick.AddListener(OnResetFixationButtonClick);
    }

    public void OnTrackableAdded(MRUKTrackable trackable)
    {
        Debug.LogError($"Trackable of type {trackable.TrackableType} added");

        if (
            trackable.TrackableType == OVRAnchor.TrackableType.QRCode
            && trackable.MarkerPayloadString != null
        )
        {
            if (!trackedObjects.ContainsKey(trackable.MarkerPayloadString))
            {
                Debug.LogError($"Detected QR code: {trackable.MarkerPayloadString}");
                GameObject markerIndicator = Instantiate(trackedObjectPrefab, trackable.transform);
                trackedObjects.Add(trackable.MarkerPayloadString, markerIndicator);
            }
        }
    }

    public void OnTrackableRemoved(MRUKTrackable trackable)
    {
        Debug.LogError($"Trackable of type {trackable.TrackableType} removed");

        if (trackable.TrackableType == OVRAnchor.TrackableType.QRCode)
        {
            if (trackedObjects.TryGetValue(trackable.MarkerPayloadString, out GameObject marker))
            {
                Destroy(marker);
                trackedObjects.Remove(trackable.MarkerPayloadString);
            }
        }
    }

    private void HandlePlacement()
    {
        switch (currentState)
        {
            case PlacementState.None:
                if (FixIndicator(QR1_NAME))
                {
                    currentState = PlacementState.QR1Fixed;
                    Debug.LogError($"QR Code '{QR1_NAME}' has been fixed");
                }
                break;

            case PlacementState.QR1Fixed:
                if (FixIndicator(QR2_NAME))
                {
                    currentState = PlacementState.QR2Fixed;
                    Debug.LogError($"QR code '{QR2_NAME}' has been fixed");
                }
                break;

            case PlacementState.QR2Fixed:
                Debug.LogError("All Indicators have already been fixed");
                break;
        }
        UpdateFixationStatusLabel();
    }

    private bool FixIndicator(string qrName)
    {
        if (trackedObjects.TryGetValue(qrName, out GameObject obj))
        {
            if (qrName == QR1_NAME)
            {
                qr1IndicatorParent = obj.transform.parent;
            }
            else if (qrName == QR2_NAME)
            {
                qr2IndicatorParent = obj.transform.parent;
            }
            // detach indicator from marker (qr code)
            obj.transform.SetParent(null);

            // trackedObjects.Remove(qrName);
            return true;
        }

        Debug.LogError($"QR code {qrName} wasn't recognized yet");
        return false;
    }

    private void CalculateAndPlaceGrid()
    {
        if (
            trackedObjects.TryGetValue(QR1_NAME, out GameObject qr1)
            && trackedObjects.TryGetValue(QR2_NAME, out GameObject qr2)
        )
        {
            Vector3 pos1 = qr1.transform.position;
            Vector3 pos2 = qr2.transform.position;

            Vector3 rotation = new Vector3(0, 0, 0);

            if (currentGrid != null)
            {
                Destroy(currentGrid);
                currentGrid = null;
            }

            Generator generator = FindAnyObjectByType<Generator>();

            if (generator != null)
            {
                currentGrid = generator.GenerateGrid(CalculateMidpointOfGrid(pos1, pos2), rotation);
                float edgeLength = (float)CalculateGridEdgeLength(pos1, pos2);
                currentGrid.transform.localScale = new Vector3(edgeLength, edgeLength, edgeLength);
                foreach (var elm in currentGrid.GetComponentsInChildren<MeasurementPoint>())
                    elm.transform.localScale *= 1 / currentGrid.transform.localScale.x;
                foreach (var elm in currentGrid.GetComponentsInChildren<TextMeshPro>())
                    elm.transform.localScale *= 1 / currentGrid.transform.localScale.x * 0.3f;
            }
            else
            {
                Debug.LogError("No GridGenerator found in scene");
            }
        }
    }

    private Vector3 CalculateMidpointOfGrid(Vector3 pos1, Vector3 pos2)
    {
        Vector3 midpointOfMarkers = (pos1 + pos2) / 2f;
        double distanceBetweenMarkers = Vector3.Distance(pos1, pos2);
        float halfEdgeLength = (float)(CalculateGridEdgeLength(pos1, pos2) / 2.0);
        return midpointOfMarkers + new Vector3(0, halfEdgeLength, 0);
    }

    /**
     * This method is implemented based on the calculation of pythagoras theorem.
     */
    private double CalculateGridEdgeLength(Vector3 pos1, Vector3 pos2)
    {
        double d = Vector3.Distance(pos1, pos2); // distance between the markers
        return d * Math.Sqrt(2) / 2; // cube edge length
    }

    private void OnResetFixationButtonClick()
    {
        Debug.LogError("Resetting fixation...");
        ResetFixation();
        UpdateFixationStatusLabel();
    }

    private void ResetFixation()
    {
        // ATTENTION: this method just resets the internal fixation, but not the labels inside the UI

        // INFO: the following function should better be implemented dynamically, but we didn't have enough time to do that. sorry xD
        if (currentState != PlacementState.None)
        {
            if (currentState == PlacementState.QR1Fixed || currentState == PlacementState.QR2Fixed)
            {
                if (trackedObjects.TryGetValue(QR1_NAME, out GameObject objQR1))
                {
                    objQR1.transform.SetParent(qr1IndicatorParent);
                }
            }
            if (currentState == PlacementState.QR2Fixed)
            {
                if (trackedObjects.TryGetValue(QR2_NAME, out GameObject objQR2))
                {
                    objQR2.transform.SetParent(qr2IndicatorParent);
                }
            }
            currentState = PlacementState.None;
        }
    }

    private void UpdateFixationStatusLabel()
    {
        String fixationStatus = "Fixation Status:\n";

        switch (currentState)
        {
            case PlacementState.None:
                fixationStatus += "nothing fixed";
                break;
            case PlacementState.QR1Fixed:
                fixationStatus += "QR_1 is fixed";
                break;
            case PlacementState.QR2Fixed:
                fixationStatus += "both fixed";
                break;
        }

        FixationStatusLabel.text = fixationStatus;
    }
}
