using UnityEngine;

public class ControllerScript : MonoBehaviour
{
    public Camera sceneCamera;
    private Vector3 targetPosition;
    private Quaternion targetRotation;
    private float step;

    void Start()
    {
        transform.position = sceneCamera.transform.position + sceneCamera.transform.forward * 3.0f;
    }

    void centerCube()
    {
        targetPosition = sceneCamera.transform.position + sceneCamera.transform.forward * 3.0f;
        targetRotation = Quaternion.LookRotation(transform.position - sceneCamera.transform.position);

        transform.position = Vector3.Lerp(transform.position, targetPosition, step);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, step);
    }

    void Update()
    {
        step = 5.0f * Time.deltaTime;
        if (OVRInput.Get(OVRInput.RawButton.RIndexTrigger)) centerCube();
    }
}
