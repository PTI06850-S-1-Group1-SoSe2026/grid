using Meta.XR.MRUtilityKit;
using UnityEngine;
using System.Collections.Generic;

public class TrackablesManager : MonoBehaviour
{
    [SerializeField] private GameObject trackedObjectPrefab;
    [SerializeField] private Dictionary<string, GameObject> trackedObjects = new();

    public void OnTrackableAdded(MRUKTrackable trackable)
    {
        Debug.Log($"Trackable of type {trackable.TrackableType} added");

        if (trackable.TrackableType == OVRAnchor.TrackableType.QRCode && trackable.MarkerPayloadString != null)
        {
            Debug.LogError($"Detected QR code: {trackable.MarkerPayloadString}");
            GameObject markerIndicator = Instantiate(trackedObjectPrefab, trackable.transform);
            trackedObjects.Add(trackable.MarkerPayloadString, markerIndicator);
        }
    }

    public void OnTrackableRemoved(MRUKTrackable trackable)
    {
        Debug.Log($"Trackable of type {trackable.TrackableType} removed");
        Destroy(transform.gameObject);
    }
}
