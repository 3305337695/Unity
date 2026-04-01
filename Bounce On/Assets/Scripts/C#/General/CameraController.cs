using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [Header("基本参数")]
    public Transform playerTrans;
    public Vector3 offset;

    private void Update()
    {
        if (playerTrans.gameObject.activeInHierarchy)
        {
            transform.position = playerTrans.position + offset;
        }
        else
        {
            transform.position = offset;
        }
    }
}
