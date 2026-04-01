using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DieManager : MonoBehaviour
{
    [Header("基本参数")]
    public Transform playerTrans;

    [Header("事件监听")]
    public Vector3EventSO dieEvent;

    private void OnEnable()
    {
        dieEvent.OnEventRaised += OnDieEvent;
    }

    private void OnDisable()
    {
        dieEvent.OnEventRaised -= OnDieEvent;
    }

    private void OnDieEvent(Vector3 pos)
    {
        playerTrans.position = pos;
    }
}
