using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBasicAttackState : State
{
    private float attackVelocityTimer;

    private const int firstComboIndex = 1;
    private int comboIndex = 1;
    private int comboLimit = 3;

    private float lastAttackTime;

    public PlayerBasicAttackState(PlayerController player, StateMachine stateMachine, string animBoolName) : base(player, stateMachine, animBoolName)
    {
        if (comboLimit != player.attackVelocity.Length)
        {
            Debug.LogWarning("I've adjusted combo limit, according to attack velocity array!");
            comboLimit = player.attackVelocity.Length;
        }
    }

    public override void Enter()
    {
        base.Enter();

        ResetComboIndexIfNeeded();
        anim.SetInteger("basicAttackIndex", comboIndex);

        ApplyAttackVelocity();
    }

    public override void Update()
    {
        base.Update();

        HandleAttackVelocity();

        if (triggerCalled)
            stateMachine.ChangeState(player.idleState);

    }

    public override void Exit()
    {
        base.Exit();

        ++comboIndex;
        lastAttackTime = Time.time;
    }

    private void ApplyAttackVelocity()
    {
        attackVelocityTimer = player.attackVelocityDuration;

        Vector2 attackVelocity = player.attackVelocity[comboIndex - 1];
        player.SetVelocity(attackVelocity.x * player.facingDir, attackVelocity.y);
    }

    private void HandleAttackVelocity()
    {
        attackVelocityTimer -= Time.deltaTime;

        if(attackVelocityTimer < 0)
            player.SetVelocity(0, rb.velocity.y);
    }

    private void ResetComboIndexIfNeeded()
    {
        if (Time.time > lastAttackTime + player.comboResetTime)
            comboIndex = firstComboIndex;

        if (comboIndex > comboLimit)
            comboIndex = firstComboIndex;
    }
}
