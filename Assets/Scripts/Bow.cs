using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Bow : MonoBehaviour
{
    [SerializeField] GameObject arrowPrefab;
    [SerializeField] Transform arrowRest;
    [SerializeField] float arrowForce = 2000f;
    [SerializeField] GameObject player;
    [SerializeField] GameObject enemy;
    private GameObject newArrow;
    Animator playerAnimator;

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
        playerAnimator = player.GetComponent<Animator>();

        shootAction.action.performed += context =>
        {
            Vector3 bowDirection = Camera.main.ScreenToWorldPoint
                (new Vector3(Screen.width, Screen.height, Camera.main.farClipPlane));
            transform.LookAt(bowDirection);
            //arrowRest.LookAt(bowDirection
            newArrow = Instantiate(arrowPrefab, arrowRest.position, arrowRest.rotation, arrowRest.transform);
            newArrow.GetComponent<Rigidbody>().isKinematic = true;
            playerAnimator.SetBool("attack", false);
            playerAnimator.SetBool("drawBow", true);
        };

        shootAction.action.canceled += context =>
        {
            if (newArrow != null)
            {
                newArrow.GetComponent<Rigidbody>().isKinematic = false;
                newArrow.GetComponent<Rigidbody>().AddForce(arrowRest.forward * arrowForce);
                playerAnimator.SetBool("drawBow", false);
                playerAnimator.SetBool("attack", true);
                player.GetComponent<PlayerStats>().UseStamina(10);
            }
        };
    }

    private void Update()
    {
        transform.position = player.GetComponent<Transform>().position + new Vector3(0, 2, 0);
        if (enemy != null) { transform.LookAt(enemy.transform.position); }
    }
}
