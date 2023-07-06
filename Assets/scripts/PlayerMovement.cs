using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    //For disabling movement while going inside a box.
    public bool disabled = false;

    //Basic movement variables
    public float movementSpeed = 5f;
    public float jumpForce = 4f;
    public float sprintSpeedMultiplier = 2f;
    //ground check
    public bool isJumping = false;
    private Rigidbody rb;

    //to check if the player is on wall or not(to prevent player sticking on walls)
    public bool isOnWall = false;

    //to bring out skill tree 
    public GameObject skillTree;

    public bool isSprinting = false;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (!disabled)
        {
            UpdateMovement();
        }

        if (Input.GetKeyDown(KeyCode.I))
        {
            skillTree.SetActive(true);
            Time.timeScale = 0f;
        }
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            skillTree.SetActive(false);
            Time.timeScale = 1f;
        }

        if (rb.velocity.magnitude == 0)
        {
            isSprinting = false;
        }
    }


    void UpdateMovement()
    {

        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        
        Vector3 movement = new Vector3(moveHorizontal, 0f, moveVertical).normalized;

        float currentSpeed = isSprinting ? movementSpeed * sprintSpeedMultiplier : movementSpeed;

        // Apply the movement
        transform.Translate(movement * currentSpeed * Time.deltaTime, Space.World);



        if (Input.GetKeyDown(KeyCode.LeftShift) && rb.velocity.magnitude > 0)
        {
            isSprinting = true;
            
        }
        else if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            isSprinting = false;
        }

        // Optional: Rotate player towards movement direction
        if (movement != Vector3.zero)
        {
            Quaternion toRotation = Quaternion.LookRotation(movement, Vector3.up);
            transform.rotation = Quaternion.Lerp(transform.rotation, toRotation, 0.15f);
        }

        if (Input.GetButtonDown("Jump") && !isJumping)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isJumping = true;
        }
        if (!isJumping && !isOnWall)
        {


            rb.velocity = new Vector3(movement.x, rb.velocity.y, movement.z);
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground") )
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
