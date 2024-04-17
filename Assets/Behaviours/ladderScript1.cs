using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ladderScript1 : MonoBehaviour
{
    private CharacterController chController;
    private CharacterMovement characterMovement; // Reference to CharacterMovement script
    private bool inside = false;
    public float speedUpDown = 3.2f;

    void Start()
    {
        chController = GetComponent<CharacterController>();
        characterMovement = GetComponent<CharacterMovement>(); // Get the CharacterMovement component
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Ladder")
        {
            inside = true;
            // Disable the CharacterMovement script
            characterMovement.enabled = false;
        }
    }
    void OnTriggerExit(Collider col)
    {
        if (col.gameObject.tag == "Ladder")
        {
            inside = false;
            // Enable the CharacterMovement script
            characterMovement.enabled = true;
        }
    }

    void Update()
    {
        if (inside && (Input.GetKey("w") || Input.GetKey("s")))
        {
            float verticalInput = Input.GetAxis("Vertical");
            Vector3 moveDirection = Vector3.up * verticalInput * speedUpDown * Time.deltaTime;
            chController.Move(moveDirection);
        }
    }
}
