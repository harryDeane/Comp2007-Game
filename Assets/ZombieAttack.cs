using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieAttack : MonoBehaviour
{
    public float damageAmount = 10f; // Damage amount inflicted by the zombie's attack
    public AudioSource attackAudio; // Reference to the AudioSource component for attack sound

    private Animator animator;
    private bool isAttacking = false;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            HealthController playerHealth = other.GetComponent<HealthController>();
            if (playerHealth != null)
            {
                // Apply damage to the player
                playerHealth.TakeDamage(damageAmount);

                // Trigger attack animation
                if (!isAttacking)
                {
                    isAttacking = true;
                    animator.SetBool("isAttacking", true);

                    // Play attack sound
                    if (attackAudio != null && !attackAudio.isPlaying)
                    {
                        attackAudio.Play();
                    }
                }
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Stop attack animation when the player exits the trigger
            isAttacking = false;
            animator.SetBool("isAttacking", false);
        }
    }
}

