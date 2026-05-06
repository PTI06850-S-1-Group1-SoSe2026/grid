using Meta.XR.MRUtilityKit;
using UnityEngine;

public class TrackablesManager : MonoBehaviour
{
    [SerializeField] private GameObject trackedObjectPrefab;

    public void OnTrackableAdded(MRUKTrackable trackable)
    {
        Debug.Log($"Trackable of type {trackable.TrackableType} added");
        Instantiate(trackedObjectPrefab, trackable.transform);
    }

    public void OnTrackableRemoved(MRUKTrackable trackable)
    {
        Debug.Log($"Trackable of type {trackable.TrackableType} removed");
        Destroy(transform.gameObject);
    }
}
