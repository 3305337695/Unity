using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : MonoBehaviour,IInteractable,ISaveable
{
    public Sprite closeSprite;
    public Sprite openSprite;

    private bool isDone;

    private SpriteRenderer spriteRenderer;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        ISaveable saveable = this;
        saveable.RegisterSaveData();
    }

    private void OnDisable()
    {
        ISaveable saveable = this;
        saveable.UnRegisterSaveData();
    }

    private void Update()
    {
        spriteRenderer.sprite = isDone ? openSprite : closeSprite;
        gameObject.tag = isDone ? "Interacted" : "Interactable";
    }

    public void TriggerAction()
    {
        if (!isDone)
        {
            isDone = true;

            OpenChest();
            GetComponent<AudioDefination>().PlayAudioClip();

            gameObject.tag = "Interacted";
        }    
    }

    public void OpenChest()
    {
        spriteRenderer.sprite = openSprite;
    }

    public DataDefination GetGuid()
    {
        return GetComponent<DataDefination>();
    }

    public void GetSaveData(Data data)
    {
        if (data.itemStateDict.ContainsKey(GetGuid().ID))
            data.itemStateDict[GetGuid().ID] = isDone;
        else
            data.itemStateDict.Add(GetGuid().ID, isDone);
    }

    public void LoadSaveData(Data data)
    {
        if (data.itemStateDict.ContainsKey(GetGuid().ID))
            isDone = data.itemStateDict[GetGuid().ID];
    }
}
