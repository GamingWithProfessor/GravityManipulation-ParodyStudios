using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform player; // The player's transform
    public float rotationSpeed = 5f; // Speed of camera rotation

    void Update()
    {
        // Determine the camera's target rotation based on gravity direction
        Vector3 gravityDirection = Physics.gravity.normalized;
        Debug.Log("Gravity Direction: " + gravityDirection);
        Quaternion targetRotation = Quaternion.LookRotation(gravityDirection, Vector3.up);
        Debug.Log("Target Rotation: " + targetRotation);

        // Smoothly rotate the camera to match the target rotation
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
    }
}
