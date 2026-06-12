using Meta.XR.MRUtilityKit;
using UnityEngine;

public class MeasurementPoint : MonoBehaviour
{
    [Header("Materials")]
    public Material inactiveMaterial;
    public Material activeMaterial;

    private MeasurementProcess measurementProcess;
    private bool measured;

    void OnTriggerEnter(Collider other)
    {
        Debug.LogError("A collider has made contact with a MeasurementPoint");
        if (other.CompareTag("Microphone"))
        {
            gameObject.GetComponent<Renderer>().material = activeMaterial;
            if (
                measurementProcess.IsProcessActive
                && !measured
                && OVRInput.GetDown(OVRInput.RawButton.RHandTrigger)
            // TODO: test everything!!
            )
            {
                measurementProcess.increaseMeasuredPoints();
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        Debug.LogError("A collider has ceased contact with a MeasurementPoint");
        if (other.CompareTag("Microphone"))
        {
            if (!measurementProcess.IsProcessActive)
            {
                gameObject.GetComponent<Renderer>().material = inactiveMaterial;
            }
        }
    }

    private void Awake()
    {
        measurementProcess = FindAnyObjectByType<MeasurementProcess>();
    }
}
