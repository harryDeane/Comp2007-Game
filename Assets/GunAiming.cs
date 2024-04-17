using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem; // Import the Input System namespace
using UnityEngine.Animations.Rigging;

public class GunAiming : MonoBehaviour
{
    public Transform aimTarget; // Target for animation rigging
    public Animator animator; // Reference to the animator controlling the character's animations
    public RigBuilder armRigBuilder; // Reference to the RigBuilder component controlling the arm rig
    private bool isAiming = false; // Flag to track if the character is aiming

    void Update()
    {
        // Check if the character is in the aiming animation
        isAiming = animator.GetCurrentAnimatorStateInfo(0).IsName("Pistol Idle");

        if (isAiming)
        {
            // Calculate the direction the player is looking
            Vector3 lookDirection = Camera.main.transform.forward;
            // Set the aim target position to a point in front of the player
            aimTarget.position = transform.position + lookDirection * 10f;

            // Enable the arm rig
            armRigBuilder.enabled = true;
        }
        else
        {
            // Disable the arm rig when not aiming
            armRigBuilder.enabled = false;
        }
    }
}

