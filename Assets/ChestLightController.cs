using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestLightController : MonoBehaviour
{
    public Light chestLight; // Reference to the light component attached to the chest

    void Start()
    {
        // Ensure the light is turned off initially
        chestLight.enabled = false;
    }

    // Called when the chest opens
    public void TurnOnLight()
    {
        chestLight.enabled = true; // Turn on the light
    }

    // Called when the chest closes
    public void TurnOffLight()
    {
        chestLight.enabled = false; // Turn off the light
    }
}

