using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    private StateMachine stateMachine;

    private NavMeshAgent agent;
    public NavMeshAgent Agent { get => agent; }
    //for debugging
    [SerializeField]
    private string currentState;

    public Path path;

    private GameObject player;

    public GameObject Player { get => player; }

     private Vector3 lastKnowPost;

    public Vector3 LastKnowPos { get => lastKnowPost; set => lastKnowPost = value; }

    [Header("Sight values")]
    public float sightDistance = 20f;
    public float eyeHeight;
    public float fieldOfView = 85f;

    [Header("Weapon Values")]
    public Transform gunBarrel;//add an empty object to the barrel of the gun
    [Range(0.1f, 10f)]
    public float fireRate;

    void Start()
    {
        stateMachine = GetComponent<StateMachine>();
        agent = GetComponent<NavMeshAgent>();
        stateMachine.Initialise();
        player = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        CanSeePlayer();
        currentState = stateMachine.activeState.ToString();
    }

    public bool CanSeePlayer()
    {
        if (player != null)
        {
            // is the player close enough to seen
            if (Vector3.Distance(transform.position, player.transform.position) < sightDistance)
            {
                Vector3 targetDirection = player.transform.position - transform.position - (Vector3.up * eyeHeight);
                float angleToPlayer = Vector3.Angle(targetDirection, transform.forward);
                if (angleToPlayer >= -fieldOfView && angleToPlayer <= fieldOfView)
                {
                    Ray ray = new Ray(transform.position + (Vector3.up * eyeHeight), targetDirection);
                    RaycastHit hit;
                    if (Physics.Raycast(ray, out hit, sightDistance))
                    {
                        if (hit.transform.gameObject == player)
                        {
                            return true;
                        }
                    }
                    Debug.DrawRay(ray.origin, ray.direction * sightDistance);
                }
            }
        }
        return false;
    }
}
}
