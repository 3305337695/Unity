using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoarPatrolState : BaseState
{
    public override void OnEnter(Enemy enemy)
    {
        currentEnemy = enemy;

        currentEnemy.currentSpeed = currentEnemy.normalSpeed;
        currentEnemy.waitTime = currentEnemy.normalWaitTime;

        currentEnemy.anim.SetBool("walk", true);
    }

    public override void Executing()
    {
        if(!currentEnemy.wait)
            currentEnemy.anim.SetBool("walk", true);
        else
            currentEnemy.anim.SetBool("walk", false);


        if (currentEnemy.FindPlayer())
            currentEnemy.SwitchState(NPCState.Chase);
    }

    public override void OnExit()
    {
        
    }
}
   