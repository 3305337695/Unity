using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slime : MonoBehaviour
{
    [Header("基本参数")]
    public float moveSpeed;
    public float waitDuration;

    private float faceDir;
    private float moveDir;
    private bool isWait;

    private Rigidbody2D rb;
    private Animator anim;
    private PhysicsCheck physicsCheck;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        physicsCheck = GetComponent<PhysicsCheck>();
    }

    private void Update()
    {
        if(!isWait)
            Move();

        ChangeFace();
    }

    private void Move()
    {
        faceDir = transform.localScale.x;
        moveDir = faceDir;

        rb.velocity = new Vector2(moveDir * moveSpeed, rb.velocity.y);

        anim.SetFloat("velocityX", Mathf.Abs(rb.velocity.x));
    }

    private void ChangeFace()
    {
        if (((physicsCheck.isLeftWall || !physicsCheck.isLeftGround) && faceDir == -1f) 
            || ((physicsCheck.isRightWall || !physicsCheck.isRightGround) && faceDir == 1f))
        {
            if (!isWait)
            {
                StartCoroutine(OnChangeFace());
            }
        }
    }

    IEnumerator OnChangeFace()
    {
        isWait = true;

        rb.velocity = new Vector2(0,rb.velocity.y);

        yield return new WaitForSeconds(waitDuration);

        transform.localScale = new Vector3(-faceDir, 1, 1);

        isWait = false;
    }
}
