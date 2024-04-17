using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestController : MonoBehaviour
{
    public Transform lid; // Reference to the chest lid
    public float openAngle = 90f; // Angle by which the lid should open
    public float openSpeed = 5f; // Speed at which the lid opens and closes

    public ChestLightController lightController; // Reference to the light controller script
    public Animation CameraAnim; // Reference to the camera animation
    public Animation CameraAnim2; // Reference to the camera animation
    private bool hasPlayedCameraAnimation1 = false; // Flag to track if the camera animation has been played
    private bool hasPlayedCameraAnimation2 = false; // Flag to track if the camera animation has been played


    private Quaternion closedRotation; // Rotation when lid is closed
    private Quaternion openRotation; // Rotation when lid is open
    private bool isOpen = false; // Flag to track if the chest is open

    void Start()
    {
        // Store the closed and open rotations
        closedRotation = lid.localRotation;
        openRotation = Quaternion.Euler(openAngle, 0, 0) * closedRotation; // Assuming rotation is around X-axis
    }

    void Update()
    {
        // Check if the player presses the 'E' key
        if (Input.GetKeyDown(KeyCode.E))
        {
            // Toggle the state of the chest
            ToggleChest();
        }

        // If the chest is open, smoothly rotate the lid towards the open position
        if (isOpen)
        {
            lid.localRotation = Quaternion.RotateTowards(lid.localRotation, openRotation, openSpeed * Time.deltaTime);
            // Turn on the light when the chest opens
            lightController.TurnOnLight();
        
            // Play the camera animation if it hasn't been played yet
            if (!hasPlayedCameraAnimation1 && CameraAnim != null)
            {
                CameraAnim.Play();
                hasPlayedCameraAnimation1 = true; // Set the flag to true to indicate the animation has been played
            }
        }
        // If the chest is closed, smoothly rotate the lid towards the closed position
        else
        {
            lid.localRotation = Quaternion.RotateTowards(lid.localRotation, closedRotation, openSpeed * Time.deltaTime);
            // Turn off the light when the chest closes
            lightController.TurnOffLight();
            // Play the camera animation if it hasn't been played yet
            if (!hasPlayedCameraAnimation2 && CameraAnim2 != null)
            {
                CameraAnim2.Play();
                hasPlayedCameraAnimation2 = true; // Set the flag to true to indicate the animation has been played
            }

        }
    }

    void ToggleChest()
    {
        // Toggle the isOpen flag
        isOpen = !isOpen;
    }
}

