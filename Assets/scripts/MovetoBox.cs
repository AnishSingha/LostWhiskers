using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MovetoBox : MonoBehaviour
{
    PlayerMovement playerMovement;
    public GameObject textPrompt;
    public GameObject player;
    public Transform teleportpos;

    public bool isinsidebox = false;

    public bool iscollided = false;


    private void Start()
    {
        playerMovement = FindObjectOfType<PlayerMovement>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            textPrompt.SetActive(true);
            iscollided=true;
            
        }
    }

    private void Update()
    {
        if (iscollided && isinsidebox == false)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                StartCoroutine("Teleport");
                textPrompt.SetActive(false);
                //Debug.Log("BUttonPressed");
                isinsidebox=true;

               
            }
            
        }
        
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            textPrompt.SetActive(false);
            iscollided=false;
        }
    }
    
    IEnumerator Teleport()
    {
        
        playerMovement.disabled = true; 
        yield return new WaitForSeconds(1f);
        player.transform.position = teleportpos.position;
        yield return new WaitForSeconds(0.1f);

        playerMovement.disabled = false;
        
    }
   

}
