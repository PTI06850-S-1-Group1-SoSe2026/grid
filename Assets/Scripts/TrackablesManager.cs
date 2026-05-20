using System.Collections.Generic;
using Meta.XR.MRUtilityKit;
using UnityEngine;

public class TrackablesManager : MonoBehaviour
{
    [SerializeField]
    private GameObject trackedObjectPrefab;

    private Dictionary<string, GameObject> trackedObjects = new();

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
}
