using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimation : MonoBehaviour
{
    private Animator anim;
    private Rigidbody2D rb;
    private EnemyController enemy;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        enemy = GetComponent<EnemyController>();
    }

    private void Update()
    {
        PlayAnimations();
    }

    private void PlayAnimations()
    {
        anim.SetFloat("velocityX", Mathf.Abs(rb.velocity.x));
        anim.SetBool("isDead", enemy.isDead);
    }

    public void PlayAttack()
    {
        anim.SetTrigger("attack");
    }

    public void PlayHurt()
    {
        anim.SetTrigger("hurt");
    }

    public void PlaySkill()
    {
        anim.SetTrigger("skill");
    }
}
