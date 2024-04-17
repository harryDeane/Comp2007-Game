using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    public float speed = 2.0f; // Movement speed
    public float crouchSpeed = 1.0f; // Movement speed while crouching
    public float runSpeed = 4.0f; // Running speed
    public float strafeSpeed = 1.0f; // Strafing speed
    public float jumpHeight = 2.0f; // Jump height
    public float gravity = 2.0f; // Gravity force
    public float cameraCrouchHeight = 0.5f; // Height of the camera when crouching

    //Health
    public int maxHealth = 100;
    private int currentHealth;

    private CharacterController controller; // Reference to the CharacterController component
    private Animator animator; // Reference to the Animator component
    private Vector3 moveDirection = Vector3.zero; // Movement direction

    private bool isCrouching = false; // Flag to track crouching state
    private bool isRunning = false; // Flag to track running state
    private bool isAiming = false;
    private bool isJumping = false;
    private bool hasGun = false;
    private Vector3 originalCameraPosition; // Original position of the camera

    private float yTeleportOffset = 200.0f; // Offset for teleportation on the y-axis

    // Static reference to the singleton instance
    public static CharacterMovement instance;

    void Start()
    {
        controller = GetComponent<CharacterController>(); // Get the CharacterController component
        animator = GetComponent<Animator>(); // Get the Animator component from the character GameObject

        // Store the original position of the camera
        originalCameraPosition = Camera.main.transform.localPosition;
    }

    void Update()
    {
        // Handle crouching input
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            isCrouching = !isCrouching; // Toggle crouching state
            animator.SetBool("IsCrouching", isCrouching); // Set the "IsCrouching" parameter in the Animator

            // Adjust camera position based on crouching state
            if (isCrouching)
            {
                Camera.main.transform.localPosition = new Vector3(originalCameraPosition.x, cameraCrouchHeight, originalCameraPosition.z);
            }
            else
            {
                Camera.main.transform.localPosition = originalCameraPosition;
            }
        }

        // Handle running input
        isRunning = Input.GetKey(KeyCode.LeftShift);

       
        // Handle aiming input
        if (Input.GetMouseButton(1) && hasGun) // Right mouse button and checks if the player has the gun
        {
            isAiming = true;
        }
        else
        {
            isAiming = false;
        }

        // Handle jumping input
        if (Input.GetButtonDown("Jump") && IsGrounded())
        {
            // Teleport player up on the y-axis
            transform.position += Vector3.up * yTeleportOffset;

            // Set jumping flag and apply jump force
            moveDirection.y = jumpHeight;
            isJumping = true;
        }
        else
        {
            isJumping = false;
        }

        // Calculate movement direction based on player input
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        Vector3 movement = transform.TransformDirection(new Vector3(horizontalInput, 0.0f, verticalInput)) * GetMovementSpeed();

        // Apply gravity
        movement.y -= gravity * Time.deltaTime;

        // Move the character controller
        controller.Move(movement * Time.deltaTime);

        // Update animations
        UpdateAnimations(horizontalInput, verticalInput);
    }

   

    float GetMovementSpeed()
    {
        return isCrouching ? crouchSpeed : (isRunning ? runSpeed : speed);
    }

    void UpdateAnimations(float horizontalInput, float verticalInput)
    {
        animator.SetBool("IsCrouchWalking", isCrouching && (verticalInput > 0.1f || Mathf.Abs(horizontalInput) > 0.1f));
        animator.SetBool("IsWalking", !isCrouching && (verticalInput > 0.1f || Mathf.Abs(horizontalInput) > 0.1f));
        animator.SetBool("IsRunning", isRunning && (verticalInput > 0.1f || Mathf.Abs(horizontalInput) > 0.1f));
        animator.SetBool("IsStrafingLeft", horizontalInput < 0);
        animator.SetBool("IsStrafingRight", horizontalInput > 0);
        animator.SetBool("IsWalkingBackward", !isCrouching && !isRunning && verticalInput < -0.1f);
        animator.SetBool("IsAiming", isAiming); // Set the "IsAiming" parameter in the Animator
        animator.SetBool("IsJumping", isJumping);

        // Reset jumping animation once the player lands
        if (!isJumping && IsGrounded())
        {
            animator.SetBool("IsJumping", false);
        }
    }

    public void SetGunPickedUp(bool pickedUp)
    {
        hasGun = pickedUp;
    }

    void Awake()
    {
        // Ensure only one instance of PlayerController exists
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    bool IsGrounded()
    {
        float rayDistance = 0.2f; // Adjust this based on your character's size
        RaycastHit hit;
        // Cast a ray downward from slightly above the character's feet
        if (Physics.Raycast(transform.position + Vector3.up * 0.1f, Vector3.down, out hit, rayDistance))
        {
            // Check if the object hit by the ray is considered ground
            if (hit.collider.CompareTag("Ground"))
            {
                return true;
            }
        }
        return false;
    }
}
