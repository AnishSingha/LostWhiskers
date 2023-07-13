using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;


public class aipart1 : MonoBehaviour
{

    [SerializeField] GameObject AlertUI; //Inidication of AI chase.

    public NavMeshAgent agent;
    public Transform[] patrolPoints;
    private int currentPatrolIndex;

    private bool isWaiting;
    private float waitTimer;
    public float waypointWaitTime = 2f;



    public float radius;
    [Range(0, 360)]
    public float angle;

    public GameObject playerRef;

    public LayerMask targetMask;
    public LayerMask obstructionMask;

    public bool canSeePlayer;

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        currentPatrolIndex = 0;

        playerRef = GameObject.FindGameObjectWithTag("Player");
        StartCoroutine(FOVRoutine());


    }

    private IEnumerator FOVRoutine()
    {
        WaitForSeconds wait = new WaitForSeconds(0.2f);

        while (true)
        {
            yield return wait;
            FieldOfViewCheck();
        }
    }

    private void FieldOfViewCheck()
    {
        Collider[] rangeChecks = Physics.OverlapSphere(transform.position, radius, targetMask);

        if (rangeChecks.Length != 0)
        {
            Transform target = rangeChecks[0].transform;
            Vector3 directionToTarget = (target.position - transform.position).normalized;

            if (Vector3.Angle(transform.forward, directionToTarget) < angle / 2)
            {
                float distanceToTarget = Vector3.Distance(transform.position, target.position);

                if (!Physics.Raycast(transform.position, directionToTarget, distanceToTarget, obstructionMask))
                    canSeePlayer = true;
                else
                    canSeePlayer = false;

                    
            }
            else
                canSeePlayer = false;
        }
        else if (canSeePlayer)
            canSeePlayer = false;
    }

    private void Update()
    {

        if (isWaiting)
        {
            waitTimer -= Time.deltaTime;
            if (waitTimer <= 0f)
            {
                isWaiting = false;
                SetDestinationToNextWaypoint();
            }
            return;
        }
    

        // Check if the AI has reached the current waypoint
        if (agent.remainingDistance <= agent.stoppingDistance)
        {
            // Start waiting at the waypoint
            isWaiting = true;
            waitTimer = waypointWaitTime;
        }

        if (canSeePlayer == true)
        {   
            agent.SetDestination(playerRef.transform.position);
            radius = 40f;

            if (canSeePlayer == false)
            {
                SetDestinationToNextWaypoint();
                
                isWaiting = true;
                

            }
        }
        else
        {
            radius = 11f;
        }
        
    }
    public void SetDestinationToNextWaypoint()
    {
        agent.SetDestination(patrolPoints[currentPatrolIndex].position);

        currentPatrolIndex++;
        if (currentPatrolIndex >= patrolPoints.Length)
        {
            currentPatrolIndex = 0;
        }
    }

    
}
