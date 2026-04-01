using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("基本参数")]
    public float normalSpeed;
    public float chaseSpeed;
    public float currentSpeed;
    public float hurtForce;

    [Header("检测参数")]
    public Vector2 centerOffset;
    public Vector2 checkSize;
    public float checkDistance;
    public LayerMask playerLayer;

    [Header("状态")]
    public bool isHurt;
    public bool isDead;
    private BaseState currentState;
    protected BaseState patrolState;
    protected BaseState chaseState;

    [Header("计时器")]
    public float normalWaitTime;
    public float chaseWaitTime;
    public float waitTime;
    public bool wait;
    public float lostTime;
    public float lostTimer;

    private Vector3 faceDir;
    private float moveDir;

    Rigidbody2D rb;
    [HideInInspector]public Animator anim;
    PhysicsCheck physicsCheck;
    Attack attack;

    protected virtual void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        physicsCheck = GetComponent<PhysicsCheck>();
        attack = GetComponent<Attack>();
    }

    private void OnEnable()
    {
        currentState = patrolState;
        currentState.OnEnter(this);
    }

    private void Update()
    {
        if(!isHurt && !isDead && !wait)
            Move();
        ChangeFace();

        currentState.Executing();
    }

    private void OnDisable()
    {
        currentState.OnExit();
    }

    public virtual void Move()
    {
        faceDir = new Vector3(-transform.localScale.x, 0, 0);
        moveDir = faceDir.x;
        rb.velocity = new Vector2(moveDir * currentSpeed, rb.velocity.y);
    }

    public void ChangeFace()
    {
        if ((!physicsCheck.isLeftGround && faceDir.x < 0f) || (!physicsCheck.isRightGround && faceDir.x > 0f) || (physicsCheck.touchLeftWall && faceDir.x < 0f) || (physicsCheck.touchRightWall && faceDir.x > 0f))
        {
            if(!wait)
                StartCoroutine(OnChangeFace());
        }
    }

    IEnumerator OnChangeFace()
    {
        wait = true;
        rb.velocity = Vector2.zero;
        yield return new WaitForSeconds(waitTime);
        transform.localScale = new Vector3(faceDir.x, 1, 1);
        wait = false;
    }

    public bool FindPlayer()
    {
        return Physics2D.BoxCast(transform.position + (Vector3)centerOffset, checkSize, 0, (Vector2)faceDir, checkDistance,playerLayer);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position + (Vector3)centerOffset + new Vector3(faceDir.x * checkDistance,0,0), 0.2f);
    }


    public void SwitchState(NPCState state)
    {
        var newState = state switch
        {
            NPCState.Patrol => patrolState,
            NPCState.Chase => chaseState,
            _ => null
        };

        currentState.OnExit();
        currentState = newState;
        currentState.OnEnter(this);
    }


    public void EnemyHurt(Transform attacker)
    {
        StartCoroutine(OnEnemyHurt(attacker));
    }

    IEnumerator OnEnemyHurt(Transform attacker)
    {
        isHurt = true;

        attack.enabled = false;

        if (transform.position.x - attacker.position.x < 0)
            transform.localScale = new Vector3(-1, 1, 1);
        if (transform.position.x - attacker.position.x > 0)
            transform.localScale = new Vector3(1, 1, 1);

        anim.SetTrigger("hurt");

        rb.velocity = Vector2.zero;
        Vector2 dir = new Vector2(transform.position.x - attacker.position.x, 0).normalized;
        rb.AddForce(dir * hurtForce, ForceMode2D.Impulse);

        yield return new WaitForSeconds(0.5f);

        attack.enabled = true;

        isHurt = false;
    }

    public void EnemyDie()
    {
        isDead = true;

        gameObject.layer = 2;

        rb.velocity = Vector2.zero;

        anim.SetBool("dead", true);
    }

    public void DestroyAfterAnimation()
    {
        Destroy(gameObject);
    }
}
