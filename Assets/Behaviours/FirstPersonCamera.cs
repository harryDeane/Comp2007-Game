using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem; // Import the Input System namespace

public class FirstPersonCamera : MonoBehaviour
{
    // Variables
    public Transform player;
    public Camera thirdPersonCamera; // Reference to the third-person camera
    public Transform targetForAnimationRigging; // Target for animation rigging
    public float mouseSensitivity = 2f;
    float cameraVerticalRotation = 0f;

    bool lockedCursor = true;

    void Start()
    {
        if (!PauseMenu.isPaused)
        {
            // Lock and Hide the Cursor
            Cursor.visible = false;


        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!PauseMenu.isPaused)
        {
            // Check for camera switch input
            if (Keyboard.current.fKey.wasPressedThisFrame)
            {
                // Toggle between first-person and third-person cameras
                ToggleCamera();
            }

            // Collect Mouse Input 
            Vector2 mouseInput = Mouse.current.delta.ReadValue() * mouseSensitivity;

            float inputX = mouseInput.x;
            float inputY = mouseInput.y;

            // Rotate the Camera around its local x axis
            cameraVerticalRotation -= inputY;
            cameraVerticalRotation = Mathf.Clamp(cameraVerticalRotation, -90f, 90f);
            transform.localEulerAngles = new Vector3(cameraVerticalRotation, 0f, 0f);

            // Rotate the Camera around its local y axis
            player.Rotate(Vector3.up * inputX);

            // Raycast from the mouse cursor to determine the position in world space
            Ray ray = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                // Move the target for animation rigging to the hit point
                targetForAnimationRigging.position = hit.point;
            }
        }
    }

    // Method to toggle between first-person and third-person cameras
    void ToggleCamera()
    {
        GetComponent<Camera>().enabled = !GetComponent<Camera>().enabled;
        thirdPersonCamera.enabled = !GetComponent<Camera>().enabled;
    }

}