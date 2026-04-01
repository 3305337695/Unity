using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(menuName = "Event/FadeEventSO")]
public class FadeEventSO : ScriptableObject
{
    public UnityAction<Color, float> OnEventRaised;

    public void RaiseEvent(Color targetColor, float fadeDuration)
    {
        OnEventRaised?.Invoke(targetColor, fadeDuration);
    }

    public void FadeIn(float fadeDuration)
    {
        RaiseEvent(Color.black, fadeDuration);
    }

    public void FadeOut(float fadeDuration)
    {
        RaiseEvent(Color.clear,fadeDuration);
    }
}
