using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveOutsideBox : MonoBehaviour
{
    PlayerMovement playerMovement;
    public Transform teleportOutPos;
    public GameObject player;
    public bool iscollided = false;
    MovetoBox movetoBox;
    // Update is called once per frame
    private void Start()
    {
        playerMovement = FindObjectOfType<PlayerMovement>();
        movetoBox = FindObjectOfType<MovetoBox>();

    }
    IEnumerator TeleportOut()
    {
        playerMovement.disabled = true;
        yield return new WaitForSeconds(1f);
        player.transform.position = teleportOutPos.position;
        yield return new WaitForSeconds(0.1f);
        playerMovement.disabled = false;

    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            iscollided = true;

        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            iscollided = false;
        }
    }
    void Update()
    {
        if (iscollided )
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                StartCoroutine("TeleportOut");
                
                movetoBox.isinsidebox = false;
               
            }
            
        }
    }
}
