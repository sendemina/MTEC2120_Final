using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GoatController : MonoBehaviour
{
    [SerializeField] float maxInteractingDistance = 10f;
    LayerMask layerMask;
    public Pinecone pineconeTarget;
    Vector3 direction;

    bool isMoving;
    bool isFollowing;

    Rigidbody rb;
    NavMeshAgent agent;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        //rb = GetComponent<Rigidbody>();
        layerMask = LayerMask.GetMask("Edible");
    }

    
    void FixedUpdate()
    {
        if (pineconeTarget)
        {
            agent.destination = pineconeTarget.transform.position;
        }

        direction = transform.TransformDirection(Vector3.forward);
        RaycastHit hit;

        if (Physics.SphereCast(transform.position, maxInteractingDistance, direction, out hit, layerMask))
        {
            if (hit.transform.TryGetComponent<Pinecone>(out pineconeTarget))
            {
                Debug.Log("set target to pinecone");
            }
        }
        
    }
}
