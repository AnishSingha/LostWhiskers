using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dash : MonoBehaviour
{
    public float dashDistance = 5f;
    public float dashDuration = 0.5f;
    public float dashCooldown = 2f;
    public float maxDashSpeed = 20f;
    public LayerMask wallLayer;

    private bool isDashing = false;
    private bool canDash = true;

    private Rigidbody playerRigidbody;
    private Vector3 originalVelocity;

    private void Start()
    {
        playerRigidbody = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftControl) && canDash)
        {
            originalVelocity = playerRigidbody.velocity; // Store the original velocity before dashing
            StartCoroutine(Dash());
        }
    }

    private IEnumerator Dash()
    {
        isDashing = true;
        canDash = false;

        Vector3 dashDirection = transform.forward;
        Vector3 dashTarget = transform.position + dashDirection * dashDistance;

        float elapsedTime = 0f;
        float currentDashSpeed = maxDashSpeed;

        while (elapsedTime < dashDuration)
        {
            if (Physics.Raycast(transform.position, dashDirection, out RaycastHit hit, 1f, wallLayer))
            {
                dashTarget = hit.point;
                currentDashSpeed = Mathf.Clamp(hit.distance / dashDuration, 0f, maxDashSpeed);
                break;
            }

            elapsedTime += Time.deltaTime;
            playerRigidbody.velocity = dashDirection * currentDashSpeed + originalVelocity; // Add the original velocity to maintain movement

            yield return null;
        }

        playerRigidbody.velocity = originalVelocity; // Restore the original velocity after dashing
        isDashing = false;

        yield return new WaitForSeconds(dashCooldown);
        canDash = true;
    }
}
