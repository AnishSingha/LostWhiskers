using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaycastCheck : MonoBehaviour
{
    public float coneAngle = 30f;
    public float coneDistance = 10f;
    public float sphereRadius = 0.1f;
    public LayerMask obstacleLayerMask;

    void Update()
    {
        // Calculate the forward direction of the cone
        Vector3 coneDirection = transform.forward;

        // Perform the spherecast
        RaycastHit[] hits = Physics.SphereCastAll(transform.position, sphereRadius, coneDirection, coneDistance, obstacleLayerMask);

        // Check if the hit points are within the desired cone angle
        foreach (RaycastHit hit in hits)
        {
            Vector3 directionToHit = hit.point - transform.position;
            float angle = Vector3.Angle(coneDirection, directionToHit);

            if (angle <= coneAngle / 2f)
            {
                // The spherecast hit an obstacle within the cone
                Debug.Log("Obstacle detected!");

                // Access the hit information
                Vector3 hitPoint = hit.point;
                Vector3 hitNormal = hit.normal;
                GameObject hitObject = hit.collider.gameObject;

                // Perform further actions based on the hit information
                // ...
            }
        }
    }
}