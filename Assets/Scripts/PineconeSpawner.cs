using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PineconeSpawner : MonoBehaviour
{
    [SerializeField] GameObject pinecone;
    GameObject newPinecone;
    Transform treeTransform;

    void Start()
    {
        treeTransform = GetComponent<Transform>();
    }

    void Update()
    {
        if (Random.Range(0, 500) == 0)
        {
            Vector3 newPos = new Vector3(treeTransform.position.x + Random.Range(-5, 5), 10, treeTransform.position.z + Random.Range(-5, 5));
            Instantiate(pinecone, newPos, Quaternion.identity);
            //newPinecone.transform.position = treeTransform.position
            //forceDirection = new Vector3(Random.Range(-1, 1), 0, Random.Range(-1, 1));
        }
    }
}
