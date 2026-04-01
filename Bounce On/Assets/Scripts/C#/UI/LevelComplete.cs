using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelComplete : MonoBehaviour
{
    [Header("¹ã²¥")]
    public VoidEventSO levelCompleteEvent;

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            levelCompleteEvent.RaiseEvent();
        }
    }
}
