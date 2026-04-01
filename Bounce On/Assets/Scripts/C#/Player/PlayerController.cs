using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public InputControl inputControl;

    [Header("基本参数")]
    public float walkSpeed;
    public float dashSpeed;
    public float jumpForce;
    public float invulnerableDuration;

    [Header("事件监听")]
    public IntEventSO menuLoadEvent;
    public IntEventSO levelLoadEvent;

    private float moveSpeed;
    private float moveDir;
    private float faceDir;
    private bool invulnerable;

    private Rigidbody2D rb;
    private PhysicsCheck physicsCheck;
    private AudioDefination audioDefination;

    private void Awake()
    {
        inputControl = new InputControl();

        rb = GetComponent<Rigidbody2D>();
        physicsCheck = GetComponent<PhysicsCheck>();
        audioDefination = GetComponent<AudioDefination>();

        inputControl.Gameplay.Jump.started += Jump;
    }

    private void Start()
    {
        moveSpeed = walkSpeed;
    }

    private void OnEnable()
    {
        inputControl.Enable();

        menuLoadEvent.OnEventRaised += OnMenuLoadEvent;
        levelLoadEvent.OnEventRaised += OnLevelLoadEvent;
    }

    private void OnDisable()
    {
        inputControl.Disable();

        menuLoadEvent.OnEventRaised -= OnMenuLoadEvent;
        levelLoadEvent.OnEventRaised -= OnLevelLoadEvent;
    }

    private void Update()
    {
        Move();
        ChangeFace();
    }

    private void Move()
    {
        moveDir = inputControl.Gameplay.Move.ReadValue<Vector2>().normalized.x;

        if (moveDir != 0)
        {
            rb.velocity = new Vector2(moveDir * moveSpeed, rb.velocity.y);
        }
    }

    private void ChangeFace()
    {
        if (moveDir != 0)
        {
            faceDir = moveDir;

            transform.localScale = new Vector3(faceDir, 1, 1);
        }
    }

    private void Jump(InputAction.CallbackContext context)
    {
        if (physicsCheck.isGround)
        {
            rb.AddForce(transform.up * jumpForce, ForceMode2D.Impulse);

            audioDefination.PlayClip();
        }
    }

    public void PlayerHurt(float attackForce,Vector3 attackDir)
    {
        StartCoroutine(OnPlayerHurt(attackForce,attackDir));
    }

    IEnumerator OnPlayerHurt(float attackForce, Vector3 attackDir)
    {
        if (!invulnerable)
        {
            invulnerable = true;
            rb.AddForce(attackDir * attackForce, ForceMode2D.Impulse);

            yield return new WaitForSeconds(invulnerableDuration);

            invulnerable = false;
        }
    }

    private void OnMenuLoadEvent(int arg0)
    {
        inputControl.Disable();
    }

    private void OnLevelLoadEvent(int arg0)
    {
        inputControl.Enable();
    }
}
