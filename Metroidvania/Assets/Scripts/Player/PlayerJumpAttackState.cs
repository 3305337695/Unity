using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJumpAttackState : State
{
    private bool touchedGround;

    public PlayerJumpAttackState(PlayerController player, StateMachine stateMachine, string animBoolName) : base(player, stateMachine, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();

        touchedGround = false;

        player.SetVelocity(player.jumpAttackVelocity.x * player.facingDir, player.jumpAttackVelocity.y);
    }

    public override void Update()
    {
        base.Update();

        if (player.groundDetected && touchedGround == false)
        {
            touchedGround = true;
            anim.SetTrigger("jumpAttackTrigger");
            player.SetVelocity(0, rb.velocity.y);
        }

        if (triggerCalled && player.groundDetected)
            stateMachine.ChangeState(player.idleState);
    }

    public override void Exit()
    {
        base.Exit();
    }
}
