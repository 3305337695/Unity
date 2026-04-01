using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DieZone : MonoBehaviour
{
    [Header("基本参数")]
    public Vector3 pos;

    [Header("广播")]
    public Vector3EventSO dieEvent;

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            dieEvent.RaiseEvent(pos);
        }
    }
}
