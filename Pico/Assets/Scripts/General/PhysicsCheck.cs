using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicsCheck : MonoBehaviour
{
    [Header("Parameter")]
    public float checkRadius = 0.2f;
    public LayerMask checkLayer;
    public Vector3 bottomOffset;

    [HideInInspector] public bool isGround;

    private void FixedUpdate()
    {
        Check();
    }

    private void Check() 
    {
        isGround = Physics2D.OverlapCircle(transform.position + bottomOffset, checkRadius, checkLayer);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position + bottomOffset, checkRadius);
    }
}
