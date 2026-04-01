using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class Character : MonoBehaviour,ISaveable
{
    [Header("基本属性")]
    public float maxHealth;
    public float currentHealth;
    public float maxPower;
    public float currentPower;

    [Header("无敌帧")]
    public float invulnerableDuration;
    public bool invulnerable;

    [Header("事件监听")]
    public VoidEventSO newGameEvent;
    public SceneLoadEventSO sceneLoadEvent;
    public VoidEventSO afterSceneLoadedEvent;

    [Header("广播")]
    public UnityEvent<Character> OnHealthChange;
    public UnityEvent<Transform> OnGetHurt;
    public UnityEvent OnDie;

    private bool inScene;

    private void OnEnable()
    {
        SetBaseValue();

        newGameEvent.OnEventRaised += SetBaseValue;
        sceneLoadEvent.LoadSceneEvent += OnLoadSceneEvent;
        afterSceneLoadedEvent.OnEventRaised += OnAfterSceneLoadedEvent;
    }

    private void OnDisable()
    {
        newGameEvent.OnEventRaised -= SetBaseValue;
        sceneLoadEvent.LoadSceneEvent -= OnLoadSceneEvent;
        afterSceneLoadedEvent.OnEventRaised -= OnAfterSceneLoadedEvent;

        ISaveable saveable = this;
        saveable.UnRegisterSaveData();
    }

    private void Start()
    {
        ISaveable saveable = this;
        saveable.RegisterSaveData();
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Water") || collision.CompareTag("Hole"))
        {
            if (currentHealth > 0 && inScene)
            {
                currentHealth = 0;
                OnDie?.Invoke();
                OnHealthChange?.Invoke(this);
            }
        }
    }

    public void TakeDamage(Attack attacker)
    {
        if (!invulnerable && inScene)
            StartCoroutine(OnTakeDamage(attacker));
    }

    IEnumerator OnTakeDamage(Attack attacker)
    {
        invulnerable = true;

        if (currentHealth - attacker.attack > 0)
        {
            currentHealth -= attacker.attack;
            OnGetHurt?.Invoke(attacker.transform);
            OnHealthChange?.Invoke(this);
            yield return new WaitForSeconds(invulnerableDuration);
        }
        else
        {
            currentHealth = 0;
            OnDie?.Invoke();
            OnHealthChange?.Invoke(this);
            yield return null;
        }

        invulnerable = false;
    }

    private void SetBaseValue()
    {
        currentHealth = maxHealth;
        currentPower = maxPower;
    }

    private void OnLoadSceneEvent(GameSceneSO arg0, Vector3 arg1, bool arg2)
    {
        inScene = false;
    }

    private void OnAfterSceneLoadedEvent()
    {
        inScene = true;

        OnHealthChange?.Invoke(this);
    }

    public DataDefination GetGuid()
    {
        return GetComponent<DataDefination>();
    }

    public void GetSaveData(Data data)
    {
        if (data.characterPosDict.ContainsKey(GetGuid().ID))
            data.characterPosDict[GetGuid().ID] = transform.position;
        else
            data.characterPosDict.Add(GetGuid().ID, transform.position);

        if(data.characterFloatDict.ContainsKey(GetGuid().ID + "Health"))
            data.characterFloatDict[GetGuid().ID + "Health"] = currentHealth;
        else
            data.characterFloatDict.Add(GetGuid().ID + "Health", currentHealth);

        if(data.characterFloatDict.ContainsKey(GetGuid().ID + "Power"))
            data.characterFloatDict[GetGuid().ID + "Power"] = currentPower;
        else
            data.characterFloatDict.Add(GetGuid().ID + "Power", currentPower);
    }

    public void LoadSaveData(Data data)
    {
        if (data.characterFloatDict.ContainsKey(GetGuid().ID + "Health"))
            currentHealth = data.characterFloatDict[GetGuid().ID + "Health"];

        if (data.characterFloatDict.ContainsKey(GetGuid().ID + "Power"))
            currentPower = data.characterFloatDict[GetGuid().ID + "Power"];

        OnHealthChange?.Invoke(this);
    }
}
