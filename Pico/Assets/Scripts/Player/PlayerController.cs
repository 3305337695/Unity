using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public InputControl inputControl;

    [Header("Property")]
    public float moveSpeed = 10f;
    public float jumpForce = 10f;

    private float moveDir;
    private float faceDir;
    private bool canDoubleJump = true;
    private bool doubleJump = false;

    private Rigidbody2D rb;
    private PhysicsCheck physicsCheck;

    private void Awake()
    {
        inputControl = new InputControl();

        rb = GetComponent<Rigidbody2D>();
        physicsCheck = GetComponent<PhysicsCheck>();
    }

    private void OnEnable()
    {
        inputControl.Enable();

        inputControl.Gameplay.Jump.started += Jump;
    }

    private void OnDisable()
    {
        inputControl.Disable();
    }

    private void Update()
    {
        Move();
        ChangeFace();
    }

    private void Move()
    {
        moveDir = inputControl.Gameplay.Move.ReadValue<Vector2>().x;
        rb.velocity = new Vector2(moveDir * moveSpeed, rb.velocity.y);
    }

    private void ChangeFace()
    {
        if (moveDir != 0)
        {
            faceDir = transform.localScale.x;
            if (faceDir * moveDir < 0)
            {
                faceDir = -faceDir;
                transform.localScale = new Vector3(faceDir,transform.localScale.y,transform.localScale.z);
            }
        }
    }

    private void Jump(InputAction.CallbackContext context)
    {
        if (physicsCheck.isGround)
        {
            rb.AddForce(transform.up * jumpForce, ForceMode2D.Impulse);
            doubleJump = true;
        }
        else if (canDoubleJump && doubleJump)
        {
            rb.velocity = new Vector2(rb.velocity.x, 0);
            rb.AddForce(transform.up * jumpForce, ForceMode2D.Impulse);
            doubleJump = false;
        }
    }
}
