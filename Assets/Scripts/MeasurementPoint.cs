using UnityEngine;

public class MeasurementPoint : MonoBehaviour
{
    [SerializeField]
    private Material inactiveMaterial;

    [SerializeField]
    private Material activeMaterial;

    [SerializeField]
    private MeasurementProcess measurementProcess;

    void OnTriggerEnter(Collider other)
    {
        Debug.LogError("A collider has made contact with a MeasurementPoint");
        if (other.CompareTag("Microphone"))
        {
            gameObject.GetComponent<Renderer>().material = activeMaterial;
            if (measurementProcess.IsProcessActive)
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
}
