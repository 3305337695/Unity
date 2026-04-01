using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.TextCore.Text;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [Header("组件")]
    public PlayerStat playerStat;
    public GameObject gameOverPanel;
    public GameObject restoreBunt;
    public Button settingButton;
    public GameObject settingPanel;
    public Slider volumeSlider;
    public GameObject backButton;

    [Header("广播")]
    public VoidEventSO pauseEvent;

    [Header("事件监听")]
    public CharacterEventSO characterEvent;
    public SceneLoadEventSO sceneLoadEvent;
    public VoidEventSO afterSceneLoadedEvent;
    public VoidEventSO gameOverEvent;
    public VoidEventSO loadGameDataEvent;
    public VoidEventSO backToMenuEvent;
    public FloatEventSO syncVolumeEvent;

    private void Awake()
    {
        settingButton.onClick.AddListener(OnPressSettingButton);
    }

    private void OnEnable()
    {
        characterEvent.OnEventRaised += HealthEvent;
        sceneLoadEvent.LoadSceneEvent += OnLoadSceneEvent;
        afterSceneLoadedEvent.OnEventRaised += OnAfterSceneLadedEvent;
        gameOverEvent.OnEventRaised += OnGameOverEvent;
        loadGameDataEvent.OnEventRaised += OnLoadGameDataEvent;
        backToMenuEvent.OnEventRaised += OnBackToMenuEvent;
        syncVolumeEvent.OnEventRaised += onSyncVolumeEvent;
    }

    private void OnDisable()
    {
        characterEvent.OnEventRaised -= HealthEvent;
        sceneLoadEvent.LoadSceneEvent -= OnLoadSceneEvent;
        afterSceneLoadedEvent.OnEventRaised -= OnAfterSceneLadedEvent;
        gameOverEvent.OnEventRaised -= OnGameOverEvent;
        loadGameDataEvent.OnEventRaised += OnLoadGameDataEvent;
        backToMenuEvent.OnEventRaised -= OnBackToMenuEvent;
        syncVolumeEvent.OnEventRaised -= onSyncVolumeEvent;
    }

    public void HealthEvent(Character character)
    {
        var persentage = character.currentHealth / character.maxHealth;
        playerStat.OnHealthChange(persentage);
    }

    private void OnLoadSceneEvent(GameSceneSO arg0, Vector3 arg1, bool arg2)
    {
        playerStat.gameObject.SetActive(false);
    }

    private void OnAfterSceneLadedEvent()
    {
        playerStat.gameObject.SetActive(true);
    }

    public void OnGameOverEvent()
    {
        gameOverPanel.SetActive(true);

        EventSystem.current.SetSelectedGameObject(restoreBunt);
    }

    private void OnLoadGameDataEvent()
    {
        gameOverPanel.SetActive(false);
    }

    public void OnBackToMenuEvent()
    {
        gameOverPanel.SetActive(false);

        settingPanel.SetActive(false);
        Time.timeScale = 1;
    }

    private void OnPressSettingButton()
    {
        if (settingPanel.activeInHierarchy)
        {
            settingPanel.SetActive(false);
            Time.timeScale = 1;
        }
        else 
        {
            pauseEvent.RaiseEvent();
            settingPanel.SetActive(true);
            EventSystem.current.SetSelectedGameObject(backButton);
            Time.timeScale = 0;
        }
    }

    private void onSyncVolumeEvent(float value)
    {
        volumeSlider.value = (value + 80f) / 100f;
    }
}
