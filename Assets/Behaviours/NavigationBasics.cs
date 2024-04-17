using UnityEngine;
using UnityEngine.AI;
using System.Collections.Generic;

public class NavigationBasics : MonoBehaviour
{
    public List<Transform> targets; // List of targets for the zombie to walk towards
    public float detectionRange = 10f; // Range within which the zombie can detect the player
    public float moveSpeed = 3f; // Movement speed of the zombie
    private Transform player; // Reference to the player's transform
    private NavMeshAgent agent;
    private int currentTargetIndex = 0; // Index of the current target

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();

        if (targets.Count == 0)
        {
            Debug.LogError("No targets assigned to the zombie!");
            return;
        }

        SetNextTarget(); // Set the first target
        player = GameObject.FindGameObjectWithTag("Player").transform; // Find the player GameObject and get its transform
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>(); // Get the NavMeshAgent component attached to the zombie
    }

    void Update()
    {
        // Check if the player is within detection range
        if (Vector3.Distance(transform.position, player.position) <= detectionRange)
        {
            // Set the destination for the NavMeshAgent to the player's position
            agent.SetDestination(player.position);
        }
        else
        {
            // Check if the zombie has reached the current target
            if (Vector3.Distance(transform.position, targets[currentTargetIndex].position) <= agent.stoppingDistance)
            {
                // Set the next target in the list
                SetNextTarget();
            }

            // Set the destination for the NavMeshAgent to the current target's position
            agent.SetDestination(targets[currentTargetIndex].position);
        }
    }

    void SetNextTarget()
    {
        // Increment the current target index and loop back to the start if necessary
        currentTargetIndex = (currentTargetIndex + 1) % targets.Count;
    }
}

