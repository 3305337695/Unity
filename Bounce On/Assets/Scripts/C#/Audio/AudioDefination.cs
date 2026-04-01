using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioDefination : MonoBehaviour
{
    [Header("基本参数")]
    public AudioClip audioClip;
    public bool playOnEnable;

    [Header("广播")]
    public AudioClipEventSO audioClipEvent;

    private void OnEnable()
    {
        if (playOnEnable)
            PlayClip();
    }

    public void PlayClip()
    {
        audioClipEvent.RaiseEvent(audioClip);
    }
}
