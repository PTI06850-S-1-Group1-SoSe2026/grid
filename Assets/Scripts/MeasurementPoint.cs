using UnityEngine;

public class MeasurementPoint : MonoBehaviour
{
    [SerializeField]
    private Material inactiveMaterial;

    [SerializeField]
    private Material activeMaterial;

    private static readonly string MICROPHONE_NAME = "DummyMicrophone";

    void OnTriggerEnter(Collider other)
    {
        Debug.LogError("A collider has made contact with a MeasurementPoint");
        if (other.name.Equals(MICROPHONE_NAME))
        {
            Debug.LogError(gameObject.GetComponent<Renderer>());
            gameObject.GetComponent<Renderer>().material = activeMaterial;
        }
    }

    void OnTriggerExit(Collider other)
    {
        Debug.LogError("A collider has ceased contact with a MeasurementPoint");
        if (other.name.Equals(MICROPHONE_NAME))
        {
            gameObject.GetComponent<Renderer>().material = inactiveMaterial;
        }
    }
}
