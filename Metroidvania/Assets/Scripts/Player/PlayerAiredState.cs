using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAiredState : State
{
    public PlayerAiredState(PlayerController player, StateMachine stateMachine, string animBoolName) : base(player, stateMachine, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Update()
    {
        base.Update();

        if (player.moveInput.x != 0)
            player.SetVelocity(player.moveInput.x * (player.moveSpeed * player.inAirMoveMultiplier), rb.velocity.y);
    }

    public override void Exit()
    {
        base.Exit();
    }
}
