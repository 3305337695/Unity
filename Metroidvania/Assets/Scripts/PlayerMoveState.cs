using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoveState : State
{
    public PlayerMoveState(PlayerController player, StateMachine stateMachine, string stateName) : base(player, stateMachine, stateName)
    {
    }


}
