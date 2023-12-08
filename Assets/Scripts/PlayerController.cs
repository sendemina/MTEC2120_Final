using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private PlayerControls inputActions;
    private InputAction movement;
    private InputAction attack;
    private Animator playerAnimator;
    private PlayerStats playerStats;

    private Rigidbody rb;

    [SerializeField] float moveForce = 0.01f;
    [SerializeField] float jumpForce = 200f;
    [SerializeField] float maxSpeed = 5f;
    private Vector3 forceDirection = Vector3.zero;

    [SerializeField] private Camera playerCamera;

    bool isAttacking;
    float attackTime = 3f;
    float timeSinceLastAttack;


    private void Awake()
    {
        inputActions = new PlayerControls();
        rb = GetComponent<Rigidbody>();
        playerAnimator = GetComponent<Animator>();
        playerStats = GetComponent<PlayerStats>();
    }

    private void OnEnable()
    {
        movement = inputActions.Player.Move;
        movement.Enable();

        inputActions.Player.Jump.started += OnJump;
        inputActions.Player.Jump.Enable();

        attack = inputActions.Player.Shoot;
        attack.started += OnShoot;
        attack.Enable();
    }

    private void FixedUpdate()
    {
        forceDirection += movement.ReadValue<Vector2>().x * GetCameraRight(playerCamera) * moveForce;
        forceDirection += movement.ReadValue<Vector2>().y * GetCameraForward(playerCamera) * moveForce;
        
        rb.AddForce(forceDirection, ForceMode.Impulse);
        forceDirection = Vector3.zero;

        if (rb.velocity.y < 0f)
        {
            rb.velocity -= Vector3.down * Physics.gravity.y * Time.fixedDeltaTime;
        }

        LimitSpeed();
        FaceTowards();

        timeSinceLastAttack += Time.deltaTime;
        if(timeSinceLastAttack >= attackTime)
        {
            isAttacking = false;
            timeSinceLastAttack = 0f;
        }
        playerAnimator.SetBool("attackReady", isAttacking);

    }

    private void LimitSpeed()
    {
        Vector3 horizontalVelocity = rb.velocity;
        horizontalVelocity.y = 0;
        if (horizontalVelocity.sqrMagnitude > maxSpeed * maxSpeed)
        {
            rb.velocity = horizontalVelocity.normalized * maxSpeed + Vector3.up * rb.velocity.y;
        }
    }

    private void FaceTowards()
    {
        Vector3 direction = rb.velocity;
        direction.y = 0f;

        if (movement.ReadValue<Vector2>().sqrMagnitude > 0.1f && direction.sqrMagnitude > 0.1f)
        {
            rb.rotation = Quaternion.LookRotation(direction, Vector3.up);
            playerAnimator.SetBool("walking", true);
            Debug.Log("walking is true");
        }
        else
        {
            rb.angularVelocity = Vector3.zero;
            playerAnimator.SetBool("walking", false);
            Debug.Log("walking is false");
        }
    }

    private Vector3 GetCameraForward(Camera playerCamera)
    {
        Vector3 forward = playerCamera.transform.forward;
        forward.y = 0;
        return forward.normalized;
    }

    private Vector3 GetCameraRight(Camera playerCamera)
    {
        Vector3 right = playerCamera.transform.right;
        right.y = 0;
        return right.normalized;
    }

    private void OnJump(InputAction.CallbackContext context)
    {
        if (!IsGrounded())
        {
            forceDirection += Vector3.up * jumpForce;
            playerAnimator.SetTrigger("jump");
        }
    }

    private void OnShoot(InputAction.CallbackContext context)
    {
        isAttacking = true;
        timeSinceLastAttack = 0f;
        playerStats.UseStamina(10);
    }

    private bool IsGrounded()
    {
        Ray ray = new Ray(this.transform.position, Vector3.down);
        if (Physics.Raycast(ray, out RaycastHit hit, 5f)) { return true; }
        else { return false; }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Indoors"))
        {
            other.gameObject.transform.Find("walls").gameObject.SetActive(false);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Indoors"))
        {
            other.gameObject.transform.Find("walls").gameObject.SetActive(true);
        }
    }

    private void OnDisable()
    {
        movement.Disable();
        inputActions.Player.Jump.started -= OnJump;
        inputActions.Player.Disable();
    }


}
