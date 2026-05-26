using System.Collections.Generic;
using Meta.XR.MRUtilityKit;
using UnityEngine;

/**
 * Fixing the indicators works as follows:
 * If QR1 has been recognized already and the controller trigger gets triggered,
 * its indicator gets fixed within the world. After that, the same thing happens
 * to the indicator of QR2, if the button gets triggered again.
 */
public class TrackablesManager : MonoBehaviour
{
    public enum PlacementState
    {
        None,
        QR1Fixed,
        QR2Fixed,
    }

    [SerializeField]
    private GameObject trackedObjectPrefab;

    private Dictionary<string, GameObject> trackedObjects = new();

    public static string QR1_NAME = "QR_1";
    public static string QR2_NAME = "QR_2";

    private PlacementState currentState = PlacementState.None;

    void Update()
    {
        if (OVRInput.GetDown(OVRInput.RawButton.RIndexTrigger))
            HandlePlacement();
    }

    public void OnTrackableAdded(MRUKTrackable trackable)
    {
        Debug.LogError($"Trackable of type {trackable.TrackableType} added");

        if (
            trackable.TrackableType == OVRAnchor.TrackableType.QRCode
            && trackable.MarkerPayloadString != null
        )
        {
            Debug.LogError($"Detected QR code: {trackable.MarkerPayloadString}");
            GameObject markerIndicator = Instantiate(trackedObjectPrefab, trackable.transform);
            trackedObjects.Add(trackable.MarkerPayloadString, markerIndicator);
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
    }

    private bool FixIndicator(string qrName)
    {
        if (trackedObjects.TryGetValue(qrName, out GameObject obj))
        {
            // detach indicator from marker (qr code)
            obj.transform.SetParent(null);

            trackedObjects.Remove(qrName);
            return true;
        }

        Debug.LogError($"QR code {qrName} wasn't recognized yet");
        return false;
    }
}
