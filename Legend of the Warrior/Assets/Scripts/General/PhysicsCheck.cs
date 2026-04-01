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
    public bool isGround = true;
    public bool isLeftGround = true;
    public bool isRightGround = true;
    public bool touchLeftWall = false;
    public bool touchRightWall = false;

    private CapsuleCollider2D coll;

    private void Start()
    {
        coll = GetComponent<CapsuleCollider2D>();
    }

    private void Update()
    {
        if (!manual)
        {
            bottomLeftOffset = new Vector2(coll.offset.x - coll.bounds.size.x / 2, 0);
            bottomRightOffset = new Vector2(coll.offset.x + coll.bounds.size.x / 2, 0);
            leftOffset = new Vector2(coll.offset.x - coll.bounds.size.x / 2, coll.bounds.size.y / 4);
            rightOffset = new Vector2(coll.offset.x + coll.bounds.size.x / 2, coll.bounds.size.y / 4);
        }

        Check();
    }

    public void Check()
    {
        isGround = Physics2D.OverlapCircle((Vector2)transform.position + bottomOffset, checkRadius, groundLayer);
        isLeftGround = Physics2D.OverlapCircle((Vector2)transform.position + bottomLeftOffset, checkRadius, groundLayer);
        isRightGround = Physics2D.OverlapCircle((Vector2)transform.position + bottomRightOffset, checkRadius, groundLayer);
        touchLeftWall = Physics2D.OverlapCircle((Vector2)transform.position + leftOffset, checkRadius, groundLayer);
        touchRightWall = Physics2D.OverlapCircle((Vector2)transform.position + rightOffset, checkRadius, groundLayer);
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
