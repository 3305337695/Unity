using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    [Header("基本参数")]
    public float attackForce;

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            var attackDir = new Vector3(collision.transform.position.x - transform.position.x, 0, 0).normalized;

            collision.GetComponent<PlayerController>().PlayerHurt(attackForce,attackDir);
        }
    }
}
