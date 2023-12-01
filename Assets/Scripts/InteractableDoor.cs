using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableDoor : Interactable
{
    Transform playerTransform;
    Animator playerAnimator;

    [SerializeField] Transform marker;
    Transform hinge;

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

        playerTransform.position = marker.position;
        playerTransform.rotation = marker.rotation;

        playerAnimator = interactor.gameObject.GetComponent<Animator>();
        //playerAnimator.SetTrigger("openDoor");

        Debug.Log(interactor.name + " interacted with door");

        StartCoroutine(OpenDoor());
    }

    IEnumerator OpenDoor()
    {
        Debug.Log(hinge.rotation.eulerAngles.y);
        while(hinge.rotation.eulerAngles.y > 270 || hinge.rotation.eulerAngles.y == 0)
        {
            Debug.Log(hinge.rotation.eulerAngles.y);
            hinge.Rotate(Vector3.up, -1);
            yield return new WaitForSeconds(0.01f);
        }
        //Debug.Log(hinge.rotation.eulerAngles.y);

        StartCoroutine(CloseDoor());
    }

    IEnumerator CloseDoor()
    {
        while (hinge.rotation.eulerAngles.y < 359)
        {
            Debug.Log(hinge.rotation.eulerAngles.y);
            hinge.Rotate(Vector3.up, 1);
            yield return new WaitForSeconds(0.01f);
        }
        Debug.Log("door ended");
    }
}
