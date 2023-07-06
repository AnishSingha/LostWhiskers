using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class aipart1 : MonoBehaviour
{
    public Transform[] waypoints;
    public float waypointWaitTime = 2f;

    private NavMeshAgent agent;
    private int currentWaypointIndex;
    private bool isWaiting;
    private float waitTimer;

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        currentWaypointIndex = 0;
        isWaiting = false;
        waitTimer = 0f;

        // Start AI navigation
        SetDestinationToNextWaypoint();
    }

    private void Update()
    {
        // Check if the AI is waiting
        if (isWaiting)
        {
            waitTimer -= Time.deltaTime;
            if (waitTimer <= 0f)
            {
                isWaiting = false;
                SetDestinationToNextWaypoint();
            }
            return;
        }

        // Check if the AI has reached the current waypoint
        if (agent.remainingDistance <= agent.stoppingDistance)
        {
            // Start waiting at the waypoint
            isWaiting = true;
            waitTimer = waypointWaitTime;
        }
    }

    private void SetDestinationToNextWaypoint()
    {
        // Set the AI's destination to the next waypoint
        agent.SetDestination(waypoints[currentWaypointIndex].position);

        // Increment the waypoint index
        currentWaypointIndex++;
        if (currentWaypointIndex >= waypoints.Length)
        {
            currentWaypointIndex = 0;
        }
    }
}
