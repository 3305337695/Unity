using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBasicAttackState : State
{
    private float attackVelocityTimer;
    private float lastAttackTime;

    private bool comboAttackQueued;
    private const int firstComboIndex = 1;
    private int comboIndex = 1;
    private int comboLimit = 3;

    private int attackDir;

    public PlayerBasicAttackState(PlayerController player, StateMachine stateMachine, string animBoolName) : base(player, stateMachine, animBoolName)
    {
        if (comboLimit != player.attackVelocity.Length)
        {
            Debug.LogWarning("Adjusted combo limit to match attack velocity array!");
            comboLimit = player.attackVelocity.Length;
        }
    }

    public override void Enter()
    {
        base.Enter();

        comboAttackQueued = false;
        ResetComboIndexIfNeeded();
        anim.SetInteger("basicAttackIndex", comboIndex);

        attackDir = player.moveInput.x != 0 ? ((int)player.moveInput.x) : player.facingDir;

        ApplyAttackVelocity();
    }

    public override void Update()
    {
        base.Update();

        HandleAttackVelocity();

        if (inputControl.Player.Attack.WasPressedThisFrame())
            QueueNextAttack();

        if (triggerCalled)
            HandleStateExit();

    }

    public override void Exit()
    {
        base.Exit();

        ++comboIndex;
        lastAttackTime = Time.time;
    }

    private void HandleStateExit()
    {
        if (comboAttackQueued)
        {
            anim.SetBool(animBoolName, false);
            player.EnterAttackStateWithDelay();
        }
        else
            stateMachine.ChangeState(player.idleState);
    }

    private void QueueNextAttack()
    {
        if (comboIndex < comboLimit)
            comboAttackQueued = true;
    }

    private void ApplyAttackVelocity()
    {
        attackVelocityTimer = player.attackVelocityDuration;

        Vector2 attackVelocity = player.attackVelocity[comboIndex - 1];
        player.SetVelocity(attackVelocity.x * attackDir, attackVelocity.y);
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
