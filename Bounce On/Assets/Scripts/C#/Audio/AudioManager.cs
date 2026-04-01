using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    [Header("组件")]
    public AudioSource BGMAudioSource;
    public AudioSource FXAudioSource;
    public AudioMixer mainMixer;

    [Header("广播")]
    public FloatEventSO displayEvent;

    [Header("事件监听")]
    public AudioClipEventSO BGMEvent;
    public AudioClipEventSO FXEvent;
    public VoidEventSO levelCompleteEvent;
    public VoidEventSO allCompleteEvent;
    public VoidEventSO pauseEvent;
    public VoidEventSO playEvent;
    public FloatEventSO volumeEventSO;

    private void OnEnable()
    {
        BGMEvent.OnEventRaised += PlayBGM;
        FXEvent.OnEventRaised += PlayFX;
        levelCompleteEvent.OnEventRaised += OnLevelCompleteEvent;
        allCompleteEvent.OnEventRaised += OnAllCompleteEvent;
        pauseEvent.OnEventRaised += OnPauseEvent;
        playEvent.OnEventRaised += OnPlayEvent;
        volumeEventSO.OnEventRaised += OnVolumeEvent;
    }

    private void OnDisable()
    {
        BGMEvent.OnEventRaised -= PlayBGM;
        FXEvent.OnEventRaised -= PlayFX;
        levelCompleteEvent.OnEventRaised -= OnLevelCompleteEvent;
        allCompleteEvent.OnEventRaised -= OnAllCompleteEvent;
        pauseEvent.OnEventRaised -= OnPauseEvent;
        playEvent.OnEventRaised -= OnPlayEvent;
        volumeEventSO.OnEventRaised -= OnVolumeEvent;
    }

    private void PlayBGM(AudioClip audioClip)
    {
        BGMAudioSource.clip = audioClip;

        BGMAudioSource.Play();
    }

    private void PlayFX(AudioClip audioClip)
    {
        FXAudioSource.clip = audioClip;

        FXAudioSource.Play();
    }

    private void OnLevelCompleteEvent()
    {
        BGMAudioSource.clip = null;
    }

    private void OnAllCompleteEvent()
    {
        BGMAudioSource.clip = null;
    }

    private void OnPauseEvent()
    {
        BGMAudioSource.Pause();

        float volume;
        mainMixer.GetFloat("masterVolume", out volume);

        displayEvent.RaiseEvent(volume);
    }

    private void OnPlayEvent()
    {
        BGMAudioSource.Play();
    }

    private void OnVolumeEvent(float value)
    {
        mainMixer.SetFloat("masterVolume", value * 100f - 80f);
    }
}
