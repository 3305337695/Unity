using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [Header("组件")]
    public Image playerHealthBarFill;
    public GameObject enemyHealthBar;
    public Image enemyHealthBarFill;

    [Header("事件监听")]
    public FloatEventSO playerHealthChangeEvent;
    public VoidEventSO enemyHealthBarDisplayEvent;
    public VoidEventSO enemyHealthBarUndisplayEvent;
    public FloatEventSO enemyHealthChangeEvent;

    private void OnEnable()
    {
        playerHealthChangeEvent.OnEventRaised += OnPlayerHealthChangeEvent;
        enemyHealthBarDisplayEvent.OnEventRaised += EnemyHealthBarDisplayEvent;
        enemyHealthBarUndisplayEvent.OnEventRaised += EnemyHealthBarUndisplayEvent;
        enemyHealthChangeEvent.OnEventRaised += EnemyHealthChangeEvent;
    }

    private void OnDisable()
    {
        playerHealthChangeEvent.OnEventRaised -= OnPlayerHealthChangeEvent;
        enemyHealthBarDisplayEvent.OnEventRaised -= EnemyHealthBarDisplayEvent;
        enemyHealthBarUndisplayEvent.OnEventRaised -= EnemyHealthBarUndisplayEvent;
        enemyHealthChangeEvent.OnEventRaised -= EnemyHealthChangeEvent;
    }

    private void OnPlayerHealthChangeEvent(float value)
    {
        playerHealthBarFill.fillAmount = value;
    }

    private void EnemyHealthBarDisplayEvent()
    {
        enemyHealthBar.SetActive(true);
    }

    private void EnemyHealthBarUndisplayEvent()
    {
        enemyHealthBar.SetActive(false);
    }

    private void EnemyHealthChangeEvent(float value)
    {
        enemyHealthBarFill.fillAmount = value;
    }
}
