using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableDoor : Interactable
{
    Transform playerTransform;
    Animator playerAnimator;

    //[SerializeField] Transform marker;
    Transform hinge;
    bool doorOpen;

    private IEnumerator coroutine;

    void Start()
    {
        hinge = GetComponent<Transform>();
    }

    void Update()
    {

    }

    public override void Interact(Interactor interactor)
    {
        playerTransform = interactor.gameObject.GetComponent<Transform>();

        //playerTransform.position = marker.position;
        //playerTransform.rotation = marker.rotation;

        playerAnimator = interactor.gameObject.GetComponent<Animator>();
        //playerAnimator.SetTrigger("openDoor");

        Debug.Log(interactor.name + " interacted with door");

        if (doorOpen)
        {
            StartCoroutine(CloseDoor());
        }
        else
        {
            StartCoroutine(OpenDoor());
        }
    }

    IEnumerator OpenDoor()
    {
        //Debug.Log(hinge.rotation.eulerAngles.y);
        for(int i = 0; i < 90; i++)
        { 
            hinge.Rotate(Vector3.forward, -1);
            yield return new WaitForSeconds(0.01f);
        }
        //while(hinge.rotation.eulerAngles.y > 270 || hinge.rotation.eulerAngles.y == 0)
        //{
        //    Debug.Log(hinge.rotation.eulerAngles.y);
        //    hinge.Rotate(Vector3.up, -1);
        //}
        //Debug.Log(hinge.rotation.eulerAngles.y);

    }

    IEnumerator CloseDoor()
    {
        for (int i = 0; i < 90; i++)
        {
            hinge.Rotate(Vector3.forward, 1);
            yield return new WaitForSeconds(0.01f);
        }
        //while (hinge.rotation.eulerAngles.y < 359)
        //{
        //    Debug.Log(hinge.rotation.eulerAngles.y);
        //    hinge.Rotate(Vector3.up, 1);
        //    yield return new WaitForSeconds(0.01f);
        //}
        Debug.Log("door ended");
    }
}
