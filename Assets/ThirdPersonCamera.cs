using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem; // Import the Input System namespace

public class ThirdPersonCamera : MonoBehaviour
{
    // Variables
    public Transform player;
    public Transform targetForAnimationRigging; // Target for animation rigging
    public float mouseSensitivity = 2f;
    float cameraVerticalRotation = 0f;

    void Update()
    {
        if (!PauseMenu.isPaused)
        {
            // Collect Mouse Input 
            Vector2 mouseInput = Mouse.current.delta.ReadValue() * mouseSensitivity;

            float inputX = mouseInput.x;
            float inputY = mouseInput.y;

            // Rotate the Camera around its local x axis
            cameraVerticalRotation -= inputY;
            cameraVerticalRotation = Mathf.Clamp(cameraVerticalRotation, -90f, 90f);
            transform.localEulerAngles = new Vector3(cameraVerticalRotation, 0f, 0f);

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
}
