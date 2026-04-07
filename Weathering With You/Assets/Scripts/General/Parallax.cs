using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{
    [Header("基本参数")]
    public float speed;

    private Transform mainCamera;
    private Vector3 lastPos;

    private void Start()
    {
        mainCamera = Camera.main.transform;
        lastPos = mainCamera.position;
    }

    private void Update()
    {
        ParallaxMove();
    }

    private void ParallaxMove()
    {
        float deltaX = mainCamera.position.x - lastPos.x;
        transform.position += new Vector3(deltaX * speed, 0, 0);
        lastPos = mainCamera.position;
    }
}
