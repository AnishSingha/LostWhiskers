using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CloseRangeAICheck : MonoBehaviour
{

    aipart1 aipart1;
    public float turnspeed;
    // Start is called before the first frame update
    void Start()
    {
        aipart1 = FindObjectOfType<aipart1>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            aipart1.agent.angularSpeed = turnspeed;
            aipart1.canSeePlayer = true;
        }
        
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            aipart1.agent.angularSpeed = 120f;
        }
    }



}
