using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Remoting.Messaging;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public InputControl inputControl;

    [Header("基本参数")]
    public float runSpeed;
    public float jumpForce;
    public float dashForce;
    public float hurtForce;

    [Header("状态")]
    public bool isDash;
    public bool isHurt;
    public bool isDead;

    [Header("广播")]
    public UnityEvent DashEvent;
    public UnityEvent AttackEvent;

    private float moveSpeed;
    private float moveDir;
    private float faceDir;

    private Rigidbody2D rb;
    private PhysicsCheck physicsCheck;
    private Character character;

    private void Awake()
    {
        inputControl = new InputControl();

        rb = GetComponent<Rigidbody2D>();
        physicsCheck = GetComponent<PhysicsCheck>();
        character = GetComponent<Character>();
    }

    private void Start()
    {
        moveSpeed = runSpeed;
        faceDir = 1;
    }

    private void OnEnable()
    {
        inputControl.Enable();

        inputControl.Gameplay.Jump.started += Jump;
        inputControl.Gameplay.Dash.started += Dash;
        inputControl.Gameplay.Attack.started += Attack;
    }

    private void OnDisable()
    {
        inputControl.Disable();
    }

    private void Update()
    {
        if(!isDash && !isHurt)
            Move();  
        
        ChangeFace();
    }

    private void Move()
    {
        moveDir = inputControl.Gameplay.Move.ReadValue<Vector2>().x;

        if(moveDir != 0)
            rb.velocity = new Vector2(moveDir * moveSpeed, rb.velocity.y);
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
        if(physicsCheck.isGround && !isDash && !isHurt)
            rb.AddForce(transform.up * jumpForce,ForceMode2D.Impulse);
    }

    private void Dash(InputAction.CallbackContext context)
    {
        if (!isDash && !isHurt)
        {
            isDash = true;

            character.enabled = false;
            rb.AddForce(new Vector2(faceDir, 0) * dashForce, ForceMode2D.Impulse);
            DashEvent?.Invoke();
        }
    }

    private void Attack(InputAction.CallbackContext context)
    {
        if (!isDash && !isHurt)
        {
            AttackEvent?.Invoke();
        }
    }

    public void Hurt(Vector2 attackDir)
    {
        if (!isHurt)
        {
            isHurt = true;

            faceDir = -attackDir.x;
            transform.localScale = new Vector3(faceDir, 1, 1);

            rb.AddForce(attackDir * hurtForce, ForceMode2D.Impulse);
        }
    }

    public void Die()
    {
        isDead = true;

        inputControl.Disable();
        character.enabled = false;
    }
}
