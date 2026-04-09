using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Effect : MonoBehaviour
{
    [Header("ÊÂ¼þ¼àÌý")]
    public CharacterEventSO launchEffectEvent;

    protected void OnEnable()
    {
        launchEffectEvent.OnEventRaised += LaunchEffect;
    }

    protected void OnDisable()
    {
        launchEffectEvent.OnEventRaised -= LaunchEffect;
    }

    protected virtual void LaunchEffect(Character target) { }
}
