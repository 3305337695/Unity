using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGroundState : State
{
    public PlayerGroundState(PlayerController player, StateMachine stateMachine, string animBoolName) : base(player, stateMachine, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Update()
    {
        base.Update();

        if (rb.velocity.y < 0 && !player.groundDetected)
        {
            stateMachine.ChangeState(player.fallState);
        }

        if (inputControl.Player.Jump.WasPressedThisFrame())
        {
            stateMachine.ChangeState(player.jumpState);
        }
    }

    public override void Exit()
    {
        base.Exit();
    }
}
