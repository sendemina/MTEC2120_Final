using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableChair : Interactable
{
    Transform playerTransform;
    Animator playerAnimator;

    [SerializeField] Transform marker;

    void Start()
    {

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
        playerAnimator.SetTrigger("sitDown");
    }
}
