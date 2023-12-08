using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{

    void Start()
    {

    }

    private void OnCollisionEnter(Collision other)
    {
        Debug.Log("The arrow hit" + other.gameObject.name);
        if (other.gameObject.name != "goha")
        {
            Destroy(gameObject, 2f);
        }
    }
}

