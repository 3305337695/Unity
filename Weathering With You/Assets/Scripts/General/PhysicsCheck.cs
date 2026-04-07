using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class PhysicsCheck : MonoBehaviour
{
    public bool manual;

    [Header("¼ì²â²ÎÊý")]
    public Vector2 bottomOffset;
    public Vector2 leftOffset;
    public Vector2 rightOffset;
    public float checkRadius;
    public LayerMask groundLayer;

    [Header("×´Ì¬")]
    public bool isGround;
    public bool isLeftWall;
    public bool isRightWall;

    private Collider2D coll;

    private void Awake()
    {
        coll = GetComponent<CapsuleCollider2D>();
    }

    private void Update()
    {
        if (!manual)
        {
            bottomOffset = new Vector2(coll.offset.x, coll.offset.y - coll.bounds.size.y / 2);
            leftOffset = new Vector2(coll.offset.x - coll.bounds.size.x / 2, coll.offset.y - coll.bounds.size.y / 3);
            rightOffset = new Vector2(coll.offset.x + coll.bounds.size.x / 2, coll.offset.y - coll.bounds.size.y / 3);
        }

        Check();
    }

    private void Check()
    {
        isGround = Physics2D.OverlapCircle((Vector2)transform.position + bottomOffset,checkRadius,groundLayer);
        isLeftWall = Physics2D.OverlapCircle((Vector2)transform.position + leftOffset, checkRadius, groundLayer);
        isRightWall = Physics2D.OverlapCircle((Vector2)transform.position + rightOffset, checkRadius, groundLayer);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position + (Vector3)bottomOffset,checkRadius);
        Gizmos.DrawWireSphere(transform.position + (Vector3)leftOffset,checkRadius);
        Gizmos.DrawWireSphere(transform.position + (Vector3)rightOffset,checkRadius);
    }
}
