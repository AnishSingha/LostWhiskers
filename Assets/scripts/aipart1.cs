using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.AI;
using UnityEngine.Rendering;

public class aipart1 : MonoBehaviour
{
    public Transform[] waypoints;
    public float waypointWaitTime = 2f;

    private NavMeshAgent agent;
    private int currentWaypointIndex;
    private bool isWaiting;
    private float waitTimer;


    public Transform playerTransform;
    public float turnSpeed = 5f;
    public float detectionRange = 10f;
    public float fovAngle = 90f;
    public float fovAngleMultiplier = 0.5f;

    [SerializeField] bool isFollowing = false;

    [SerializeField] GameObject AlertUI;

    [SerializeField] float stoppingDistance = 1.5f;

    private void Start()
    {
        AlertUI.SetActive(true);

        

        agent = GetComponent<NavMeshAgent>();
        currentWaypointIndex = 0;
        isWaiting = false;
        waitTimer = 0f;

        // Start AI navigation
        SetDestinationToNextWaypoint();
    }

    private void Update()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, playerTransform.position);

        Vector3 directionToPlayer = playerTransform.position - transform.position;
        float angleToPlayer = Vector3.Angle(directionToPlayer, transform.forward);
        // Check if the player is within the detection range
        if (distanceToPlayer <= detectionRange && angleToPlayer <= fovAngle * fovAngleMultiplier)
        {
            agent.isStopped = false;
            isFollowing = true;
            AlertUI.SetActive(true);
            // Calculate the direction from AI to player

            directionToPlayer.y = 0f; // Optional: Set the y-component to 0 to ensure the AI turns only on the horizontal plane

            // Rotate the AI towards the player using Slerp for smooth rotation
            Quaternion targetRotation = Quaternion.LookRotation(directionToPlayer);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, turnSpeed * Time.deltaTime);
        }


        //Makes the AI follow the player if detected and initiates chase state.
        if (isFollowing == true)
        {
            agent.SetDestination(playerTransform.position);
            if (agent.remainingDistance <= stoppingDistance)
            {
                agent.isStopped = true;

                isWaiting = true;
                waitTimer = waypointWaitTime;
            }
            else
            {
                agent.isStopped = false;
            }
        }
        
        //Repositions AI when player is out of range
        if (distanceToPlayer > detectionRange)
        {
            isFollowing = false;
            AlertUI.SetActive(false);
        }

       

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
