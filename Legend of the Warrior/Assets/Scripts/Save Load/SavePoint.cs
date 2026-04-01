using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SavePoint : MonoBehaviour, IInteractable
{
    [Header("基本参数")]
    public SpriteRenderer spriteRenderer;
    public Sprite darkSprite;
    public Sprite lightSprite;
    public GameObject lightObj;

    [Header("广播")]
    public VoidEventSO saveGameDataEvent;

    private bool isDone;

    private void Update()
    {
        spriteRenderer.sprite = isDone ? lightSprite : darkSprite;
        lightObj.SetActive(isDone);
    }

    public void TriggerAction()
    {
        if (!isDone)
        {
            isDone = true;

            spriteRenderer.sprite = lightSprite;
            lightObj.SetActive(true);
            GetComponent<AudioDefination>().PlayAudioClip();

            saveGameDataEvent.RaiseEvent();

            gameObject.tag = "Interacted";
        }
    }
}
