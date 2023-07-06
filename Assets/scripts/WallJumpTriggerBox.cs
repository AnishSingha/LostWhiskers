using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallJumpTriggerBox : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject text;


    
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            text.SetActive(true);
            Debug.Log("sdad");
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            text.SetActive(false);
        }
    }
}
