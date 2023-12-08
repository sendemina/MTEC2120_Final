using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using System.Drawing;
using UnityEngine.InputSystem;


public class Aim : MonoBehaviour
{
    [SerializeField] private GameObject bow;
    [SerializeField] private CinemachineVirtualCamera aimVirtualCamera;
    [SerializeField] private float normalSensitivity = 5f;
    [SerializeField] private float aimSensitivity = 1f;

    private PlayerController playerController;
    public InputActionReference shootAction;


    private void OnEnable()
    {
        shootAction.action.Enable();

    }


    private void OnDisable()
    {
        shootAction.action.Disable();
    }



    private void Awake()
    {
       playerController = GetComponent<PlayerController>();
    }


    void Start()
    {
        shootAction.action.performed += context =>
        {
            aimVirtualCamera.gameObject.SetActive(true);
            Debug.Log("Shoot Action is called. ");
        };

        shootAction.action.canceled += context =>
        {
            aimVirtualCamera.gameObject.SetActive(false);
        };

    }

    void Update()
    {
        Vector3 bowDirection = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.farClipPlane));
        bow.transform.LookAt(bowDirection);


        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit raycastHit, 999f))
        {
            bow.transform.LookAt(raycastHit.point);
        }
    }
}


