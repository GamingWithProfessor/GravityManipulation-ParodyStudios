using UnityEngine;

public class GravityController : MonoBehaviour
{
    public Transform player;
    public float gravityStrength = 9.81f;
    public Transform hologram; // Assign hologram in inspector

    private Vector3 currentGravity = Vector3.down;
    private Quaternion targetRotation;

    void Start()
    {
        // Initialize the hologram's position
        hologram.gameObject.SetActive(false);
    }

    void Update()
    {
        Vector3 newGravityDirection = Vector3.down;
        Quaternion newRotation = Quaternion.identity;
        bool hologramVisible = false;

        if (Input.GetKey(KeyCode.Keypad8)) // Numpad 8 for Up
        {
            newGravityDirection = Vector3.up;
            newRotation = Quaternion.Euler(0, 180, 0);
            hologramVisible = true;
        }
        else if (Input.GetKey(KeyCode.Keypad2)) // Numpad 2 for Down
        {
            newGravityDirection = Vector3.down;
            newRotation = Quaternion.Euler(0, 0, 0);
            hologramVisible = true;
        }
        else if (Input.GetKey(KeyCode.Keypad4)) // Numpad 4 for Left
        {
            newGravityDirection = Vector3.left;
            newRotation = Quaternion.Euler(0, 90, 0);
            hologramVisible = true;
        }
        else if (Input.GetKey(KeyCode.Keypad6)) // Numpad 6 for Right
        {
            newGravityDirection = Vector3.right;
            newRotation = Quaternion.Euler(0, -90, 0);
            hologramVisible = true;
        }
        else if (Input.GetKey(KeyCode.Keypad1)) // Numpad 1 for Back
        {
            newGravityDirection = Vector3.back;
            newRotation = Quaternion.Euler(0, 0, 0);
            hologramVisible = true;
        }
        else if (Input.GetKey(KeyCode.Keypad3)) // Numpad 3 for Forward
        {
            newGravityDirection = Vector3.forward;
            newRotation = Quaternion.Euler(0, 0, 0);
            hologramVisible = true;
        }

        hologram.transform.rotation = newRotation;
        hologram.gameObject.SetActive(hologramVisible);

        if (Input.GetKeyDown(KeyCode.Return))
        {
            Physics.gravity = newGravityDirection * gravityStrength;
            currentGravity = newGravityDirection;
            // Rotate the camera to align with new gravity
            Camera.main.transform.rotation = newRotation;
        }
    }
}
