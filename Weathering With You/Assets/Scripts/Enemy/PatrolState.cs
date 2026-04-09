using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class PatrolState : State<EnemyController>
{
    public bool isReverse;

    [Header("基本属性")]
    public float runSpeed;

    [Header("计时")]
    public Vector2 timeInterval;

    private float reverse;
    private PatrolStates currentState;
    private float timer;
    private float faceDir;
    private float moveDir;

    public override void Enter(EnemyController owner)
    {
        this.owner = owner;

        reverse = isReverse ? -1f : 1f;

        timer = 0f;
    }

    public override void Execute()
    {
        if (owner.target != null)
        {
            owner.ChangeState(EnemyStates.Attack);
        }
        else
        {
            if (timer <= 0f)
            {
                currentState = Random.Range(0, 2) == 0 ? PatrolStates.Idle : PatrolStates.Run;
                timer = Random.Range(timeInterval.x, timeInterval.y);
            }

            if (currentState == PatrolStates.Idle)
            {
                owner.rb.velocity = new Vector2(owner.rb.velocity.x, owner.rb.velocity.y);
            }
            else
            {
                faceDir = transform.localScale.x;
                faceDir = Random.Range(0, 500) > 0 ? faceDir : -faceDir;
                transform.localScale = new Vector3(faceDir, 1, 1);

                moveDir = reverse * faceDir;
                owner.rb.velocity = new Vector2(moveDir * runSpeed, owner.rb.velocity.y);

                if ((faceDir == reverse * 1f && owner.physicsCheck.isRightWall) || (faceDir == reverse * -1f && owner.physicsCheck.isLeftWall))
                {
                    faceDir = -faceDir;
                    transform.localScale = new Vector3(faceDir, 1, 1);
                }
            }

            if (timer > 0f)
                timer -= Time.deltaTime;
        }
    }

    public override void Exit()
    {
        
    }
}
