using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class DataManager : MonoBehaviour
{
    public static DataManager instance;

    [Header("ÊÂ¼þ¼àÌý")]
    public VoidEventSO saveGameDataEvent;
    public VoidEventSO loadGameDataEvent;

    private Data saveData;
    private List<ISaveable> saveableList = new List<ISaveable>();

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);

        saveData = new Data();
    }

    private void OnEnable()
    {
        saveGameDataEvent.OnEventRaised += SaveData;
        loadGameDataEvent.OnEventRaised += LoadData;
    }

    private void OnDisable()
    {
        saveGameDataEvent.OnEventRaised -= SaveData;
        loadGameDataEvent.OnEventRaised -= LoadData;
    }

    public void RegisterSaveData(ISaveable saveable)
    {
        if(!saveableList.Contains(saveable))
            saveableList.Add(saveable);
    }

    public void UnRegisterSaveData(ISaveable saveable)
    {
        if(saveableList.Contains(saveable))
            saveableList.Remove(saveable);
    }

    public void SaveData()
    {
        foreach (var saveable in saveableList)
        {
            saveable.GetSaveData(saveData);
        }
    }

    public void LoadData()
    {
        foreach (var saveable in saveableList)
        {
            saveable.LoadSaveData(saveData);
        }
    }
}
