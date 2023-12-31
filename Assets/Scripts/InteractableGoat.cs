using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableGoat : Interactable
{
    Animator goatAnimator;
    Transform playerTransform;
    [SerializeField] Animator playerAnimator;

    [SerializeField] Transform marker;

    AudioSource audio;

    private IEnumerator coroutine;

    bool isPetting;
    float petTime = 3f;
    float timeSinceLastAttack;

    void Start()
    {
        goatAnimator = GetComponent<Animator>();
        audio = GetComponent<AudioSource>();
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
        //playerAnimator.SetTrigger("petGoat");

        Debug.Log(interactor.name + " interacted with goat");
        goatAnimator.SetTrigger("isPet");
        playerAnimator.SetTrigger("pet");
        audio.Play();
    }
}
