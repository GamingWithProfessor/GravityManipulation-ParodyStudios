using UnityEngine;

public class GravityManipulator : MonoBehaviour
{
    public float gravityStrength = 9.81f;
    public float rotationSpeed = 5.0f;
    public GameObject hologram;

    private Rigidbody rb;
    private Animator animator;
    private Vector3 currentGravityDirection = Vector3.down;
    private Transform hologramTransform;
    private bool gravityChangeRequested = false;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();

        if (hologram != null)
        {
            hologramTransform = hologram.transform;
            hologram.SetActive(false); // Hide hologram initially
        }
    }

    void Update()
    {
        // Update hologram based on input keys
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            SetHologramDirection(Vector3.up);
            currentGravityDirection = Vector3.up;
            gravityChangeRequested = true;
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            SetHologramDirection(Vector3.down);
            currentGravityDirection = Vector3.down;
            gravityChangeRequested = true;
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            SetHologramDirection(Vector3.left);
            currentGravityDirection = Vector3.left;
            gravityChangeRequested = true;
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            SetHologramDirection(Vector3.right);
            currentGravityDirection = Vector3.right;
            gravityChangeRequested = true;
        }

        if (Input.GetKeyDown(KeyCode.Return) && gravityChangeRequested) // Enter key
        {
            ApplyGravity();
            gravityChangeRequested = false; // Reset flag
        }

        // Rotate the character to match the new gravity direction
        Quaternion targetRotation = Quaternion.FromToRotation(Vector3.up, -currentGravityDirection.normalized);
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        animator.SetBool("IsGrounded", FindObjectOfType<CharacterMovement>().IsGrounded());
    }

    private void SetHologramDirection(Vector3 direction)
    {
        if (hologram != null)
        {
            hologram.SetActive(true);
            hologramTransform.position = transform.position + Vector3.up * 2.0f;
            hologramTransform.forward = direction;
        }
    }

    private void ApplyGravity()
    {
        if (hologram != null)
        {
            hologram.SetActive(false);
        }
        rb.velocity = Vector3.zero;
        Physics.gravity = currentGravityDirection * gravityStrength;
    }
}
