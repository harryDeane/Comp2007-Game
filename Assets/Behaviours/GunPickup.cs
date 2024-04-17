using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunPickup : MonoBehaviour
{
    public GameObject gunModel; // Reference to the gun model
    public Transform playerHand; // Reference to the player's hand transform
    public GameObject bulletPrefab; // Reference to the bullet prefab
    public Transform firePoint; // Reference to the point where bullets will be spawned
    public float bulletForce = 33f; // Force applied to the bullet
    public KeyCode pickupKey = KeyCode.E; // Key to pick up the gun
    public GameObject Instruction;


    private CharacterMovement characterMovement;
    private bool isInRange = false; // Flag to track if the player is in range of the gun
    private bool isGunPickedUp = false; // Flag to track if the gun has been picked up

    void Start()
    {
        characterMovement = FindObjectOfType<CharacterMovement>(); // Find the PlayerMovement script

        Instruction.SetActive(false);
    }

    void Update()
    {
        if (isInRange && Input.GetKeyDown(pickupKey))
        {
            PickUpGun();
        }

        // Check if the gun has been picked up before allowing shooting
        if (isGunPickedUp && Input.GetButtonDown("Fire1"))
        {
            Shoot();
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isInRange = true;
            // Display prompt to inform the player they can pick up the gun
            
            Instruction.SetActive(true);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isInRange = false;
            // Hide the prompt when the player moves away from the gun
            Instruction.SetActive(false);
        }
    }

    void Shoot()
    {
        if (!PauseMenu.isPaused)
        {
            // Instantiate a bullet at the fire point
            GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);

            // Add force to the bullet in the forward direction
            Rigidbody bulletRigidbody = bullet.GetComponent<Rigidbody>();
            if (bulletRigidbody != null)
            {
                bulletRigidbody.AddForce(firePoint.forward * bulletForce, ForceMode.Impulse);
            }
        }
    }

    void PickUpGun()
    {
        // Teleport the gun to the player's hand position
        gunModel.transform.position = playerHand.position;
        gunModel.transform.rotation = playerHand.rotation;

        // Make the gun a child of the player's hand
        gunModel.transform.parent = playerHand;

        // Disable the collider of the gun so it doesn't interfere with the player's movement
        gunModel.GetComponent<Collider>().enabled = false;

        // Disable the GunPickup script to prevent further interaction
        // enabled = false; 

        // Set the flag to indicate that the gun has been picked up
        isGunPickedUp = true;

        // Set the gun as picked up
        characterMovement.SetGunPickedUp(true);
        Instruction.SetActive(false);
    }
}
