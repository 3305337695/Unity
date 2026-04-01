using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    [Header("组件")]
    public AudioSource BGMSource;
    public AudioSource FXSource;
    public AudioMixer mainAudioMixer;

    [Header("广播")]
    public FloatEventSO syncVolumeEvent;

    [Header("事件监听")]
    public PlayerAudioEventSO BGMAudioEvent;
    public PlayerAudioEventSO FXAudioEvent;
    public FloatEventSO volumeChangeEventSO;
    public VoidEventSO pauseEvent;

    private void OnEnable()
    {
        BGMAudioEvent.OnEventRaised += BGMEvent;
        FXAudioEvent.OnEventRaised += FXEvent;
        volumeChangeEventSO.OnEventRaised += OnVolumeChangeEvent;
        pauseEvent.OnEventRaised += OnPauseEvent;
    }

    private void OnDisable()
    {
        BGMAudioEvent.OnEventRaised -= BGMEvent;
        FXAudioEvent.OnEventRaised -= FXEvent;
        volumeChangeEventSO.OnEventRaised -= OnVolumeChangeEvent;
        pauseEvent.OnEventRaised -= OnPauseEvent;
    }

    private void BGMEvent(AudioClip audioClip)
    {
        BGMSource.clip = audioClip;
        BGMSource.Play();
    }

    private void FXEvent(AudioClip audioClip)
    {
        FXSource.clip = audioClip;
        FXSource.Play();
    }

    private void OnVolumeChangeEvent(float value)
    {
        mainAudioMixer.SetFloat("MasterVolume", value * 100f - 80f);
    }

    private void OnPauseEvent()
    {
        float value;
        mainAudioMixer.GetFloat("MasterVolume",out value);

        syncVolumeEvent.RaiseEvent(value);
    }
}
