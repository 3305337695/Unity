using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [Header("基本参数")]
    public float runSpeed = 10f;
    public float attackSpeed = 1f;
    public float currentSpeed = 0f;
    public float jumpForce = 10f;
    public float hurtForce;

    [Header("状态")]
    public bool isHurt;
    public bool isDead;
    public bool isAttack;

    [Header("物理材质")]
    public PhysicsMaterial2D normal;
    public PhysicsMaterial2D wall;

    [Header("事件监听")]
    public SceneLoadEventSO sceneLoadEvent;
    public VoidEventSO afterSceneLoadedEvent;
    public VoidEventSO loadGameDataEvent;
    public VoidEventSO backToMenuEvent;

    private int faceDir;
    private Vector2 moveDir;

    private PlayerInputControl inputControl;
    private Rigidbody2D rb;
    private PhysicsCheck physicsCheck;
    private PlayerAnimation playerAnimation;
    private CapsuleCollider2D coll;
    private AudioDefination audioDefination;

    private void Awake()
    {
        inputControl = new PlayerInputControl();
    }

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        physicsCheck = GetComponent<PhysicsCheck>();
        playerAnimation = GetComponent<PlayerAnimation>();
        coll = GetComponent<CapsuleCollider2D>();
        audioDefination = GetComponent<AudioDefination>();

        inputControl.Gameplay.Jump.started += Jump;
        inputControl.Gameplay.Attack.started += PlayerAttack;

        currentSpeed = runSpeed;
    }

    private void OnEnable()
    {
        inputControl.Enable();

        sceneLoadEvent.LoadSceneEvent += OnLoadSceneEvent;
        afterSceneLoadedEvent.OnEventRaised += OnAfterSceneLoadedEvent;
        loadGameDataEvent.OnEventRaised += OnLoadGameDataEvent;
        backToMenuEvent.OnEventRaised += OnBackToMenuEvent;
    }

    private void OnDisable()
    {
        inputControl.Disable();

        sceneLoadEvent.LoadSceneEvent -= OnLoadSceneEvent;
        afterSceneLoadedEvent.OnEventRaised -= OnAfterSceneLoadedEvent;
        loadGameDataEvent.OnEventRaised -= OnLoadGameDataEvent;
        backToMenuEvent.OnEventRaised += OnBackToMenuEvent;
    }

    private void Update()
    {
        if(!isHurt)
            Move();

        ChangeMaterial();
    }

    public void Move()
    {
        moveDir = inputControl.Gameplay.Move.ReadValue<Vector2>();

        faceDir = (int)moveDir.x;
        if (faceDir != 0)
        {
            transform.localScale = new Vector3(faceDir,1,1);
        }

        rb.velocity = new Vector2(moveDir.x * currentSpeed,rb.velocity.y);
    }

    private void Jump(InputAction.CallbackContext context)
    {
        if (physicsCheck.isGround && !isHurt && !isAttack)
        {
            rb.AddForce(transform.up * jumpForce, ForceMode2D.Impulse);
            audioDefination.PlayAudioClip();
        }
    }

    private void PlayerAttack(InputAction.CallbackContext context)
    {
        isAttack = true;
        if(physicsCheck.isGround)
            currentSpeed = attackSpeed;
        playerAnimation.PlayAttack();
    }

    public void PlayerHurt(Transform attacker)
    {
        isHurt = true;
        rb.velocity = new Vector2(0,rb.velocity.y);
        Vector2 dir = new Vector2(transform.position.x - attacker.position.x, 0).normalized;
        rb.AddForce(dir * hurtForce, ForceMode2D.Impulse);
    }

    public void PlayerDie()
    {
        isDead = true;
        inputControl.Gameplay.Disable();
    }

    public void ChangeMaterial()
    {
        coll.sharedMaterial = physicsCheck.isGround ? normal : wall;
    }

    private void OnLoadSceneEvent(GameSceneSO arg0, Vector3 arg1, bool arg2)
    {
        inputControl.Gameplay.Disable();
    }

    private void OnAfterSceneLoadedEvent()
    {
        inputControl.Gameplay.Enable();
    }

    public void OnLoadGameDataEvent()
    {
        isDead = false;
    }

    public void OnBackToMenuEvent()
    {
        isDead = false;
    }
}
