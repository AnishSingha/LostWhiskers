using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class OnwallActivator : MonoBehaviour
{
    public PlayerMovement playerMovement;
    // Start is called before the first frame update
    void Start()
    {
        playerMovement  = FindObjectOfType<PlayerMovement>();
    }

   
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            playerMovement.isOnWall = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            playerMovement.isOnWall = false;
        }
    }
}

