using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallJumpTest : MonoBehaviour
{
    public float movementSpeed = 5f;
    public float jumpForce = 5f;
    public float wallJumpForce = 5f;
    public float wallJumpHorizontalForce = 5f;
    private bool isJumping = false;
    private bool isOnWall = false;
    private Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        // Horizontal movement
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(moveHorizontal, 0f, moveVertical) * movementSpeed;
        rb.velocity = new Vector3(movement.x, rb.velocity.y, movement.z);

        // Jumping
        if (Input.GetButtonDown("Jump"))
        {
            if (isOnWall)
            {
                // Wall jump
                rb.AddForce(new Vector3(-moveHorizontal * wallJumpHorizontalForce, 1f, 0f) * wallJumpForce, ForceMode.Impulse);
                isJumping = true;
            }
            else if (!isJumping)
            {
                // Regular jump
                rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
                isJumping = true;
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isJumping = false;
            isOnWall = false;
        }
        else if (collision.gameObject.CompareTag("Wall"))
        {
            isOnWall = true;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Wall"))
        {
            isOnWall = false;
        }
    }
}
