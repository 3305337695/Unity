using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerIdleState : State
{
    public PlayerIdleState(PlayerController player, StateMachine stateMachine, string stateName) : base(player, stateMachine, stateName)
    {
    }


}
