using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class FadeCanvas : MonoBehaviour
{
    public Image fadeImage;
    public FadeEventSO fadeEvent;

    private void OnEnable()
    {
        fadeEvent.OnEventRaised += FadeEvent;
    }

    private void OnDisable()
    {
        fadeEvent.OnEventRaised -= FadeEvent;
    }

    public void FadeEvent(Color targetColor,float fadeDuration)
    {
        fadeImage.DOBlendableColor(targetColor, fadeDuration);
    }
}
