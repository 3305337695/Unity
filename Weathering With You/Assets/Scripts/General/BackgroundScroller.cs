using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundScroller : MonoBehaviour
{
    private Transform mainCamera;
    private float bgWidth;

    private void Start()
    {
        mainCamera = Camera.main.transform;
        bgWidth = GetComponent<SpriteRenderer>().bounds.size.x;
    }

    private void Update()
    {
        BgMove();
    }

    private void BgMove()
    {
        float dis = mainCamera.position.x - transform.position.x;

        if(Mathf.Abs(dis) > bgWidth)
        {
            transform.position += new Vector3(Mathf.Sign(dis) * bgWidth * 2, 0, 0);
        }
    }
}
