using Meta.XR.MRUtilityKit;
using UnityEngine;

public class MeasurementPoint : MonoBehaviour
{
    [Header("Materials")]
    public Material inactiveMaterial;
    public Material activeMaterial;

    private MeasurementProcess measurementProcess;
    private bool measured = false;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Microphone"))
        {
            Debug.LogError(
                "A collider of a Microphone-tagged object has made contact with a MeasurementPoint"
            );
            gameObject.GetComponent<Renderer>().material = activeMaterial;

            // for debugging
            Debug.LogError(
                "measurementProcess.IsProcessActive: " + measurementProcess.IsProcessActive
            );
            Debug.LogError("measured: " + measured);
            Debug.LogError("hand trigger: " + OVRInput.Get(OVRInput.RawButton.RHandTrigger));

            if (
                measurementProcess.IsProcessActive
                && !measured
                && OVRInput.Get(OVRInput.RawButton.RHandTrigger)
            )
            {
                Debug.LogError("********** point measured");
                measured = true;
                measurementProcess.increaseMeasuredPoints();
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Microphone"))
        {
            Debug.LogError(
                "A collider of a Microphone-tagged object has ceased contact with a MeasurementPoint"
            );
            if (!measured)
            {
                gameObject.GetComponent<Renderer>().material = inactiveMaterial;
            }
        }
    }

    private void Awake()
    {
        measurementProcess = FindAnyObjectByType<MeasurementProcess>();
    }

    public void setUnmeasured()
    {
        measured = false;
        gameObject.GetComponent<Renderer>().material = inactiveMaterial;
    }
}
