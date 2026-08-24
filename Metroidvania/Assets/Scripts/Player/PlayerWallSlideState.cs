using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWallSlideState : State
{
    public PlayerWallSlideState(PlayerController player, StateMachine stateMachine, string animBoolName) : base(player, stateMachine, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Update()
    {
        base.Update();

        HandleWallSlide();

        if (inputControl.Player.Jump.WasPressedThisFrame())
        {
            stateMachine.ChangeState(player.wallJumpState);
        }

        if (!player.wallDetected)
        {
            stateMachine.ChangeState(player.fallState);
        }

        if (player.groundDetected)
        {
            stateMachine.ChangeState(player.idleState);
            player.Flip();
        }
    }

    public override void Exit()
    {
        base.Exit();
    }

    private void HandleWallSlide()
    {
        if (player.moveInput.y < 0)
        {
            player.SetVelocity(player.moveInput.x, rb.velocity.y);
        }
        else
        {
            player.SetVelocity(player.moveInput.x, rb.velocity.y * player.wallSlideSlowMultiplier);
        }
    }
}
