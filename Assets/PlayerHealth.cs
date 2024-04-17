using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 100; // Maximum health of the player
    private int currentHealth; // Current health of the player

    private void Start()
    {
        currentHealth = maxHealth; // Set current health to maximum health at the start
    }

    // Method to take damage and reduce player's health
    public void TakeDamage(int damage)
    {
        currentHealth -= damage; // Reduce player's health by the amount of damage
        Debug.Log("Player took " + damage + " damage. Current health: " + currentHealth);

        // Check if the player has run out of health
        if (currentHealth <= 0)
        {
            Die(); // Call the Die method if player health is zero or less
        }
    }

    // Method to handle player death
    private void Die()
    {
        Debug.Log("Player died!");
        // Add any additional logic here, such as respawn or game over handling
    }
}