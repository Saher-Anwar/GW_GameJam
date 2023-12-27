using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This script handles player movement including WASD, mouse look, jumping and sprinting
public class PlayerMovement : MonoBehaviour
{

    public CharacterController Controller;
    public float moveSpeed = 5f;
    public float jumpHeight = 4f;
    public float gravity = -30f;
    public float sprintFactor = 2f;

    private Vector3 velocity;
    private bool isGrounded;

    // Start is called before the first frame update
    void Start()
    {
        Controller = GetComponent<CharacterController>();
        // Lock the cursor to the center of the screen
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        // Check if the player is on the ground
        isGrounded = Controller.isGrounded;
        if (isGrounded && velocity.y<0)
        {
            velocity.y = -2f;
        }

        // Get input from the player
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        // Calculate camera direction
        Vector3 cameraForward = Camera.main.transform.forward;
        Vector3 cameraRight = Camera.main.transform.right;
        cameraForward.y = 0f;
        cameraRight.y = 0f;
        Vector3 moveDirection = (cameraForward * vertical) + (cameraRight * horizontal);
        moveDirection.Normalize();

        // Move the player
        Controller.Move(moveDirection * moveSpeed * Time.deltaTime);

        // Handle player rotation
        if (moveDirection != Vector3.zero)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(moveDirection), 0.15F);
        }

        // Handle jumping
        if (Input.GetButtonDown("Jump") && isGrounded) velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);

        // Handle sprinting
        if (Input.GetKey(KeyCode.LeftShift))
        {
            // Only sprint if the player is on the ground
            if (isGrounded) moveSpeed = 5f * sprintFactor;
        }
        else moveSpeed = 5f;

        // Apply gravity
        velocity.y += gravity * Time.deltaTime;
        Controller.Move(velocity * Time.deltaTime);
        
    }
}
