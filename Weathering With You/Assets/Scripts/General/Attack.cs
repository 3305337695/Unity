using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    [Header("»ù±¾ÊôÐÔ")]
    public float attack;

    protected void OnTriggerStay2D(Collider2D collision)
    {
        LaunchAttack(collision);
    }

    protected virtual void LaunchAttack(Collider2D collision)
    {
        collision.GetComponent<Character>()?.TakeDamage(this);
    }
}
