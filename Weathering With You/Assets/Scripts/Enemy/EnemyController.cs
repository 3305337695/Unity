using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnemyController : MonoBehaviour
{
    [HideInInspector] public Character target;

    [HideInInspector] public Rigidbody2D rb;
    [HideInInspector] public PhysicsCheck physicsCheck;
    [HideInInspector] public Character character;

    [Header("基本属性")]
    public float hurtForce;

    [Header("状态")]
    public bool isDead;

    [Header("广播")]
    public VoidEventSO enemyHealthBarUndisplayEvent;

    private StateMachine<EnemyController> stateMachine;
    private Dictionary<EnemyStates, State<EnemyController>> enemyStateDict;

    private float faceDir;

    private void Awake()
    {
        stateMachine = new StateMachine<EnemyController>(this);
        enemyStateDict = new Dictionary<EnemyStates, State<EnemyController>>();

        enemyStateDict.Add(EnemyStates.Patrol, GetComponent<PatrolState>());
        enemyStateDict.Add(EnemyStates.Attack, GetComponent<AttackState>());

        rb = GetComponent<Rigidbody2D>();
        physicsCheck = GetComponent<PhysicsCheck>();    
        character = GetComponent<Character>();
    }

    private void Start()
    {
        ChangeState(EnemyStates.Patrol);
    }
    private void Update()
    {
        stateMachine.Execute();
    }

    public void ChangeState(EnemyStates enemyState)
    {
        stateMachine.ChangeState(enemyStateDict[enemyState]);
    }

    public void Hurt(Vector2 attackDir)
    {
        faceDir = -attackDir.x;
        transform.localScale = new Vector3(faceDir, 1, 1);

        rb.AddForce(attackDir * hurtForce, ForceMode2D.Impulse);
    }

    public void Die()
    {
        isDead = true;

        character.enabled = false;

        enemyHealthBarUndisplayEvent.RaiseEvent();
    }

    public void DestroySelf()
    {
        Destroy(gameObject);
    }
}
