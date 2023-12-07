using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Interactor : MonoBehaviour
{
    [SerializeField] InputActionReference interactAction;
    [SerializeField] float maxInteractingDistance = 10f;
    PlayerStats stats;
    LayerMask layerMask;
    public Interactable interactableTarget;


    Vector3 direction;

    private void OnEnable()
    {
        interactAction.action.Enable();
    }

    private void OnDisable()
    {
        interactAction.action.Disable();
    }

    void Start()
    {
        stats = gameObject.GetComponent<PlayerStats>();

        layerMask = LayerMask.GetMask("Interactable");

        interactAction.action.performed += context =>
        {
            if(interactableTarget != null)
            {
                Debug.Log("interacting with " + interactableTarget.name);
                interactableTarget.Interact(this);
            }
            else
            {
                Debug.Log("interacting with nothing");
            }
        };
    }

    
    void Update()
    {
        direction = transform.TransformDirection(Vector3.forward);
        RaycastHit hit;

        if(interactableTarget)
        {
            //Debug.Log(interactableTarget.name);
        }

        if (Physics.Raycast(transform.position, direction, out hit, maxInteractingDistance, layerMask))
        {
            if(hit.transform.TryGetComponent<Interactable>(out interactableTarget))
            {
                Debug.Log("set target to " + interactableTarget.name);
            }
        }
        else
        {
            if (interactableTarget)
            {
                //interactableTarget = null; //MAKE IT STICK!!!!
            }
        }    
    }

    private void OnTriggerStay(Collider other)
    {
        int LayerDanger = LayerMask.NameToLayer("Danger");
        if (other.gameObject.layer == LayerDanger)
        {
            stats.ReceiveDamage(1);
        }
    }

    public void ToggleFire()
    {
        if (interactableTarget.name == "Fireplace")
        {
            InteractableFireplace fireplace = (InteractableFireplace)interactableTarget;
            fireplace.fireIsLit = !fireplace.fireIsLit;
        }
    }
}
