using UnityEngine;

public class MeasurementPoint : MonoBehaviour
{
    [SerializeField]
    private Material inactiveMaterial;

    [SerializeField]
    private Material activeMaterial;

    void OnTriggerEnter(Collider other)
    {
        Debug.LogError("A collider has made contact with a MeasurementPoint");
        if (other.CompareTag("Microphone"))
        {
            gameObject.GetComponent<Renderer>().material = activeMaterial;
        }
    }

    void OnTriggerExit(Collider other)
    {
        Debug.LogError("A collider has ceased contact with a MeasurementPoint");
        if (other.CompareTag("Microphone"))
        {
            gameObject.GetComponent<Renderer>().material = inactiveMaterial;
        }
    }
}
