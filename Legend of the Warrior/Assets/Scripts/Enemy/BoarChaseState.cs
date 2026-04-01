using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoarChaseState : BaseState
{
    public override void OnEnter(Enemy enemy)
    {
        currentEnemy = enemy;

        currentEnemy.currentSpeed = currentEnemy.chaseSpeed;
        currentEnemy.waitTime = currentEnemy.chaseWaitTime;

        currentEnemy.anim.SetBool("run", true);
    }

    public override void Executing()
    {
        if (!currentEnemy.wait)
            currentEnemy.anim.SetBool("run", true);
        else
            currentEnemy.anim.SetBool("run", false);

        if (!currentEnemy.FindPlayer())
            currentEnemy.lostTimer -= Time.deltaTime;
        else
            currentEnemy.lostTimer = currentEnemy.lostTime;

        if (currentEnemy.lostTimer <= 0)
            currentEnemy.SwitchState(NPCState.Patrol);
    }

    public override void OnExit()
    {
        currentEnemy.anim.SetBool("run", false);
    }
}
