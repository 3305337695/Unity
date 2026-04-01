using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AllComplete : MonoBehaviour
{
    [Header("¹ã²¥")]
    public VoidEventSO allCompleteEvent;

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            allCompleteEvent.RaiseEvent();
        }
    }
}
