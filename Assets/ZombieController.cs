using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieController : MonoBehaviour
{
    // Zombie Health
    public int maxHealth = 100;
    private int currentHealth;

   

    private void Start()
    {
        currentHealth = maxHealth;
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Bullet")
        {
            // Decrease the zombie's health
            ZTakeDamage(10); // Decrease health by 10 when hit by a bullet

            // Check if the zombie has died
            if (currentHealth <= 0)
            {
                Die();
            }
        }
    }

    void ZTakeDamage(int damage)
    {
        currentHealth -= damage;
        Debug.Log("Zombie Health: " + currentHealth);
    }

    void Die()
    {
        // Play death animation, disable collider, etc.
        Debug.Log("Zombie died!");
        gameObject.SetActive(false); // Example: Deactivate the zombie GameObject
    }
    
}
