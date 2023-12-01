using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoatController : MonoBehaviour
{
    [SerializeField] float moveForce = 5f;
    [SerializeField] float jumpForce = 5f;
    [SerializeField] float maxSpeed = 5f;
    Vector3 forceDirection = Vector3.zero;
    bool isMoving;
    bool isFollowing;

    Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();    
    }

    
    void FixedUpdate()
    {
        NewDirection();
        rb.AddForce(forceDirection*moveForce, ForceMode.Impulse);
        LimitSpeed();
        FaceTowards();
        //Debug.Log(forceDirection.x + " " + forceDirection.z);
    }

    private void NewDirection()
    {
        if (Random.Range(0, 50) == 0)
        {
            forceDirection = new Vector3(Random.Range(-1, 1), 0, Random.Range(-1, 1));
        }
        else if(Random.Range(0, 50) == 0)
        {
            //find player, move towards
        }
    }

    private void LimitSpeed()
    {
        Vector3 horizontalVelocity = rb.velocity;
        horizontalVelocity.y = 0;
        if (horizontalVelocity.sqrMagnitude > maxSpeed * maxSpeed)
        {
            rb.velocity = horizontalVelocity.normalized * maxSpeed + Vector3.up * rb.velocity.y;
        }
    }

    private void FaceTowards()
    {
        Vector3 direction = rb.velocity;
        direction.y = 0f;

        if (isMoving)
        {
            rb.rotation = Quaternion.LookRotation(direction, Vector3.up);
        }
        else
        {
            rb.angularVelocity = Vector3.zero;
        }
    }
}
