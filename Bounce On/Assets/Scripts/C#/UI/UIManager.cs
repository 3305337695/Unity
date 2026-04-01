using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [Header("组件")]
    public GameObject levelCompletePanel;
    public GameObject allCompletePanel;
    public GameObject pausePanel;
    public GameObject pauseButton;
    public Slider volumeSlider; 

    [Header("基本参数")]
    public Sprite pauseSprite;
    public Sprite playSprite;

    [Header("广播")]
    public VoidEventSO pauseEvent;
    public VoidEventSO playEvent;

    [Header("事件监听")]
    public VoidEventSO levelCompleteEvent;
    public VoidEventSO allCompleteEvent;
    public IntEventSO menuLoadEvent;
    public IntEventSO levelLoadEvent;
    public FloatEventSO displayEvent;

    private void OnEnable()
    {
        levelCompleteEvent.OnEventRaised += OnLevelCompleteEvent;
        allCompleteEvent.OnEventRaised += OnAllCompleteEvent;
        menuLoadEvent.OnEventRaised += OnMenuLoadEvent;
        levelLoadEvent.OnEventRaised += OnLevelLoadEvent;
        displayEvent.OnEventRaised += OnDisplayEvent;

        pauseButton.GetComponent<Button>().onClick.AddListener(OnPause);
    }

    private void OnDisable()
    {
        levelCompleteEvent.OnEventRaised -= OnLevelCompleteEvent;
        allCompleteEvent.OnEventRaised -= OnAllCompleteEvent;
        menuLoadEvent.OnEventRaised -= OnMenuLoadEvent;
        levelLoadEvent.OnEventRaised -= OnLevelLoadEvent;
        displayEvent.OnEventRaised -= OnDisplayEvent;
    }

    private void OnLevelCompleteEvent()
    {
        Time.timeScale = 0;

        levelCompletePanel.SetActive(true);
    }

    private void OnAllCompleteEvent()
    {
        Time.timeScale = 0;

        allCompletePanel.SetActive(true);
    }

    private void OnLevelLoadEvent(int arg0)
    {
        Time.timeScale = 1;

        levelCompletePanel.SetActive(false);
        allCompletePanel.SetActive(false);

        pausePanel.SetActive(false);
        pauseButton.SetActive(true);

        pauseButton.GetComponent<Image>().sprite = pauseSprite;
    }

    private void OnMenuLoadEvent(int arg0)
    {
        Time.timeScale = 1;

        levelCompletePanel.SetActive(false);
        allCompletePanel.SetActive(false);

        pausePanel.SetActive(false);
        pauseButton.SetActive(false);
    }

    private void OnPause()
    {
        if (pausePanel.activeInHierarchy)
        {
            Time.timeScale = 1;

            pausePanel.SetActive(false);

            pauseButton.GetComponent<Image>().sprite = pauseSprite;

            playEvent.RaiseEvent();
        }
        else 
        {
            Time.timeScale = 0;

            pausePanel.SetActive(true);

            pauseButton.GetComponent<Image>().sprite = playSprite;

            pauseEvent.RaiseEvent();
        }
    }

    private void OnDisplayEvent(float volume)
    {
        volumeSlider.value = (volume + 80f) / 100f;
    }
}
