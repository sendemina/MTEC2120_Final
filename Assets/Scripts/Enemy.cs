using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{

    [SerializeField] GameObject player;
    [SerializeField] float damage = 1f;
    [SerializeField] float health = 10;
    NavMeshAgent agent;
    Transform enemy;



    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        enemy = GetComponent<Transform>();
    }

    void Update()
    {
        agent.destination = player.transform.position;
    }

    void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject == player)
        {
            collision.gameObject.GetComponent<PlayerStats>().ReceiveDamage(damage);
            StartCoroutine(Attack());
        }

        if(collision.gameObject.layer == LayerMask.NameToLayer("Damaging"))
        {
            health -= 5;
            if(health <= 0) { Destroy(gameObject); }
        }
    }

    IEnumerator Attack()
    {
        for (int i = 0; i < 45; i++)
        {
            enemy.Rotate(Vector3.left, -1);
            yield return new WaitForSeconds(0.01f);
        }

        for (int i = 0; i < 45; i++)
        {
            enemy.Rotate(Vector3.left, 1);
            yield return new WaitForSeconds(0.01f);
        }

        //Debug.Log("enemy has attacked");
    }
}
