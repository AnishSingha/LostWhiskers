using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaticAIDetection : MonoBehaviour
{
    public Transform playerTransform;
    public float turnSpeed = 5f;
    public float detectionRange = 10f;
    public float fovAngle = 90f;
    public float fovAngleMultiplier = 0.5f;
    public Transform returnViewDirection;

    private void Update()
    {
        // Calculate the distance between AI and player
        float distanceToPlayer = Vector3.Distance(transform.position, playerTransform.position);

        Vector3 directionToPlayer = playerTransform.position - transform.position;
        float angleToPlayer = Vector3.Angle(directionToPlayer, transform.forward);
        // Check if the player is within the detection range
        if (distanceToPlayer <= detectionRange && angleToPlayer <= fovAngle *fovAngleMultiplier)
        {

            // Calculate the direction from AI to player
           
            directionToPlayer.y = 0f; // Optional: Set the y-component to 0 to ensure the AI turns only on the horizontal plane

            // Rotate the AI towards the player using Slerp for smooth rotation
            Quaternion targetRotation = Quaternion.LookRotation(directionToPlayer);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, turnSpeed * Time.deltaTime);
        }
        if (distanceToPlayer > detectionRange)
        {
            Vector3 returnDirection = returnViewDirection.position - transform.position;
            returnDirection.y = 0f;

            Quaternion targetRotation = Quaternion.LookRotation(returnDirection);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, turnSpeed * Time.deltaTime);
        }
    }
}
