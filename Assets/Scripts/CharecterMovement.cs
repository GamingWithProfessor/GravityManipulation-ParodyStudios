using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class CharacterMovement : MonoBehaviour
{
    public float walkSpeed = 5f;
    public float sprintSpeed = 10f;
    public float jumpHeight = 2f;
    public float gravity = -9.81f;

    private CharacterController controller;
    private Vector3 velocity;
    private bool isGrounded;
    private float currentSpeed;
    private float fallTime;
    private float fallThreshold = 6f; // Time in seconds before falling results in game over
    private Animator animator;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
        currentSpeed = walkSpeed;
    }

    void Update()
    {
        // Check if the character is on the ground
        isGrounded = controller.isGrounded;
        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;  // Ensure the character sticks to the ground
        }

        // Get input for movement
        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");

        Vector3 move = transform.right * moveX + transform.forward * moveZ;

        if (Input.GetKey(KeyCode.LeftShift))
        {
            currentSpeed = sprintSpeed;
        }
        else
        {
            currentSpeed = walkSpeed;
        }

        controller.Move(move * currentSpeed * Time.deltaTime);

        // Set the Speed parameter in the Animator
        animator.SetFloat("Speed", move.magnitude);

        // Handle jumping
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }
        // Apply gravity
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
        CheckFalling();
    }
    public bool IsGrounded()
    {
        return isGrounded;
    }
    private void CheckFalling()
    {
        if (!isGrounded)
        {
            fallTime += Time.deltaTime; // Increment fall time if falling
            if (fallTime > fallThreshold)
            {
                // Notify GameManager that the player has been falling too long
                FindObjectOfType<GameManager>().HandleFalling();
            }
        }
        else
        {
            fallTime = 0f; 
        }
    }
}
