using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Bow : MonoBehaviour
{
    [SerializeField] GameObject arrowPrefab;
    [SerializeField] Transform arrowRest;
    [SerializeField] float arrowForce = 2000f;
    private GameObject newArrow;

    public InputActionReference shootAction;

    private void OnEnable()
    {
        shootAction.action.Enable();
    }

    private void OnDisable()
    {
        shootAction.action.Disable();
    }

    void Start()
    {
        shootAction.action.performed += context =>
        {
            Vector3 bowDirection = Camera.main.ScreenToWorldPoint
                (new Vector3(Screen.width, Screen.height, Camera.main.farClipPlane));
            transform.LookAt(bowDirection);
            //arrowRest.LookAt(bowDirection
            newArrow = Instantiate(arrowPrefab, arrowRest.position, arrowRest.rotation, arrowRest.transform);
            newArrow.GetComponent<Rigidbody>().isKinematic = true;
        };

        shootAction.action.canceled += context =>
        {
            if (newArrow != null)
            {
                newArrow.GetComponent<Rigidbody>().isKinematic = false;
                newArrow.GetComponent<Rigidbody>().AddForce(arrowRest.forward * arrowForce);
            }
        };
    }
}
