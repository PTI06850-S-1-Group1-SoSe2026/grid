using UnityEngine;

public class LookAtCamera : MonoBehaviour
{
    public Camera CameraToLookAt;

    void Update()
    {
        transform.LookAt(transform.position + CameraToLookAt.transform.rotation * Vector3.forward, CameraToLookAt.transform.rotation * Vector3.up);
    }
}
