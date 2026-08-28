using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWallJumpState : State
{
    public PlayerWallJumpState(PlayerController player, StateMachine stateMachine, string animBoolName) : base(player, stateMachine, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();

        player.SetVelocity(player.wallJumpForce.x * -player.facingDir, player.wallJumpForce.y);
    }

    public override void Update()
    {
        base.Update();

        if (rb.velocity.y < 0 && stateMachine.currentState != player.jumpAttackState)
            stateMachine.ChangeState(player.fallState);

        if (player.wallDetected)
            stateMachine.ChangeState(player.wallSlideState);

        if (inputControl.Player.Attack.WasPressedThisFrame())
            stateMachine.ChangeState(player.jumpAttackState);
    }

    public override void Exit()
    {
        base.Exit();
    }
}
