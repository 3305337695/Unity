using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using static UnityEngine.Rendering.DebugUI;

public class Character : MonoBehaviour
{
    [Header("Property")]
    public float maxHealth = 100f;
    public float currentHealth = 100f;

    [Header("Invincibility")]
    public bool invulnerable;
    public float invulnerableDuration = 1f;

    [Header("Broadcast")]
    public UnityEvent OnHurt;
    public UnityEvent OnDie;

    private void Start()
    {
        SetProperty();
    }

    public void SetProperty()
    {
        currentHealth = maxHealth;
    }

    public void OnTakeDamage(float value)
    {
        if (!invulnerable)
        {
            StartCoroutine(TakeDamage(value));
        }
    }

    IEnumerator TakeDamage(float value)
    {
        invulnerable = true;

        if (currentHealth - value > 0)
        {
            currentHealth -= value;
            OnHurt?.Invoke();
            yield return new WaitForSeconds(invulnerableDuration);
        }
        else
        {
            currentHealth = 0;
            OnDie?.Invoke();
            yield return null;
        }

        invulnerable = false;
    }
}
