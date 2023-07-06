using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waypoint : MonoBehaviour
{
    public Transform[] waypoints;
    public float movementSpeed = 5f;
    public float rotationSpeed = 5f;

    private int currentWaypointIndex = 0;

    private void Update()
    {
        if (currentWaypointIndex < waypoints.Length)
        {
            // Move towards the current waypoint
            Transform currentWaypoint = waypoints[currentWaypointIndex];
            Vector3 targetPosition = new Vector3(currentWaypoint.position.x, currentWaypoint.position.y, currentWaypoint.position.z);
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, movementSpeed * Time.deltaTime);

            // Rotate towards the current waypoint
            Vector3 targetDirection = currentWaypoint.position - transform.position;
           
           

            // Check if the car has reached the current waypoint
            if (Vector3.Distance(transform.position, targetPosition) < 0.1f)
            {
                currentWaypointIndex++;
            }
        }
    }
}
