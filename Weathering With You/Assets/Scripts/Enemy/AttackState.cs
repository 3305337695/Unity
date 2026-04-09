using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AttackState : State<EnemyController>
{
    public bool isReverse;

    [Header("基本属性")]
    public float runSpeed;
    public float chaseDis;
    public float attackDis;

    [Header("计时")]
    public Vector2 timeInterval;

    [Header("连击")]
    public int comboCount;

    [Header("广播")]
    public UnityEvent AttackEvent;
    public UnityEvent<Character> SkillEvent;
    public VoidEventSO enemyHealthBarDisplayEvent;
    public VoidEventSO enemyHealthBarUndisplayEvent;

    private float reverse;
    private AttackStates currentState;
    private float timer;
    private float deltaX;
    private float faceDir;
    private float moveDir;

    public override void Enter(EnemyController owner)
    {
        this.owner = owner;

        reverse = isReverse ? -1f : 1f;

        timer = 0f;

        enemyHealthBarDisplayEvent.RaiseEvent();
        owner.character.Display();
    }

    public override void Execute()
    {
        if (owner.target == null)
        {
            owner.ChangeState(EnemyStates.Patrol);
        }
        else
        {
            deltaX = owner.target.transform.position.x - transform.position.x;
            faceDir = deltaX >= 0 ? reverse * 1 : reverse * -1;
            transform.localScale = new Vector3(faceDir, 1, 1);

            if (timer <= 0f && Mathf.Abs(deltaX) <= chaseDis)
            {
                currentState = Random.Range(0, 2) == 0 ? AttackStates.Idle : AttackStates.Attack;
            }
            if (Mathf.Abs(deltaX) > chaseDis)
            {
                currentState = AttackStates.Chase;
            }

            if (currentState == AttackStates.Idle)
            {
                if (timer <= 0f)
                    timer = Random.Range(timeInterval.x, timeInterval.y);

                owner.rb.velocity = new Vector2(owner.rb.velocity.x, owner.rb.velocity.y);
            }
            else if (currentState == AttackStates.Attack)
            {
                if (Mathf.Abs(deltaX) > attackDis)
                {
                    moveDir = reverse * faceDir;
                    owner.rb.velocity = new Vector2(moveDir * runSpeed, owner.rb.velocity.y);
                }

                if (timer <= 0f)
                {
                    timer = Random.Range(timeInterval.x, timeInterval.y);

                    if (Random.Range(0, 4) > 0)
                    {
                        int combo = Random.Range(1, comboCount);
                        for (int i = 0; i < combo; i++)
                        {
                            AttackEvent?.Invoke();
                        }
                    }
                    else
                    {
                        SkillEvent?.Invoke(owner.target);
                    }
                }
            }
            else
            {
                moveDir = reverse * faceDir;
                owner.rb.velocity = new Vector2(moveDir * runSpeed, owner.rb.velocity.y);
            }

            if (timer > 0f)
                timer -= Time.deltaTime;
        }
    }

    public override void Exit()
    {
        enemyHealthBarUndisplayEvent.RaiseEvent();
    }
}
