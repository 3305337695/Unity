using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicsCheck : MonoBehaviour
{
    public bool manual;

    [Header("¼ì²â²ÎÊý")]
    public Vector2 bottomOffset;
    public Vector2 bottomLeftOffset;
    public Vector2 bottomRightOffset;
    public Vector2 leftOffset;
    public Vector2 rightOffset;
    public float checkRadius;
    public LayerMask groundLayer;

    [Header("×´Ì¬")]
    public bool isGround;
    public bool isLeftGround;
    public bool isRightGround;
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
            bottomLeftOffset = new Vector2(coll.offset.x - coll.bounds.size.x / 2, coll.offset.y - coll.bounds.size.y / 2);
            bottomRightOffset = new Vector2(coll.offset.x + coll.bounds.size.x / 2, coll.offset.y - coll.bounds.size.y / 2);
            leftOffset = new Vector2(coll.offset.x - coll.bounds.size.x / 2, coll.offset.y);
            rightOffset = new Vector2(coll.offset.x + coll.bounds.size.x / 2, coll.offset.y);
        }

        Check();
    }

    private void Check()
    {
        isGround = Physics2D.OverlapCircle((Vector2)transform.position + bottomOffset, checkRadius, groundLayer);
        isLeftGround = Physics2D.OverlapCircle((Vector2)transform.position + bottomLeftOffset, checkRadius, groundLayer);
        isRightGround = Physics2D.OverlapCircle((Vector2)transform.position + bottomRightOffset, checkRadius, groundLayer);
        isLeftWall = Physics2D.OverlapCircle((Vector2)transform.position + leftOffset, checkRadius, groundLayer);
        isRightWall = Physics2D.OverlapCircle((Vector2)transform.position + rightOffset, checkRadius, groundLayer);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere((Vector2)transform.position + bottomOffset, checkRadius);
        Gizmos.DrawWireSphere((Vector2)transform.position + bottomLeftOffset, checkRadius);
        Gizmos.DrawWireSphere((Vector2)transform.position + bottomRightOffset, checkRadius);
        Gizmos.DrawWireSphere((Vector2)transform.position + leftOffset, checkRadius);
        Gizmos.DrawWireSphere((Vector2)transform.position + rightOffset, checkRadius);
    }
}
