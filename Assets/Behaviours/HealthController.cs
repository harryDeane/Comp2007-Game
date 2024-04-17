using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthController : MonoBehaviour
{
    public Slider healthSlider; // Reference to the health slider UI component
    public float startingHealth = 100f;
    public float maxHealth = 100f;
    public float decreaseAmount = 10f;
    public float increaseAmount = 10f;

    private float currentHealth;

    void Start()
    {
        currentHealth = startingHealth;
        UpdateHealthBar();
    }

    void Update()
    {
        
    }

    public void TakeDamage(float damageAmount) // Change the method to public
    {
        currentHealth -= damageAmount;
        currentHealth = Mathf.Clamp(currentHealth, 0f, maxHealth);
        
        UpdateHealthBar();
        if (currentHealth <= 0)
        {
            HandlePlayerDeath();
        }
    }

    void AddHealth(float healAmount) // Change the method to public
    {
        currentHealth += healAmount;
        currentHealth = Mathf.Clamp(currentHealth, 0f, maxHealth);
      
        UpdateHealthBar();
    }

    void UpdateHealthBar()
    {
        
        float normalizedHealth = currentHealth / maxHealth;
       
        healthSlider.value = normalizedHealth; // Update slider value based on current health
    }

    void HandlePlayerDeath()
    {
        GameManager.instance.EndGame(); // Call the method in GameManager to end the game
    }
}

