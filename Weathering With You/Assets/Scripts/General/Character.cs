using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Character : MonoBehaviour
{
    [Header("基本属性")]
    public float maxHealth;
    public float currentHealth;

    [Header("无敌帧")]
    public float invulnerableDuration;
    public bool invulnerable;

    [Header("广播")]
    public UnityEvent<Vector2> HurtEvent;
    public UnityEvent DieEvent;
    public UnityEvent<float> HealthChangeEvent;

    private void Start()
    {
        InitialHealth();
    }

    public void InitialHealth()
    {
        currentHealth = maxHealth;
        Display();
    }

    public void TakeDamage(Attack attack)
    {
        if(!invulnerable)
            StartCoroutine(OnTakeDamage(attack));
    }

    IEnumerator OnTakeDamage(Attack attack)
    {
        invulnerable = true;

        if (currentHealth - attack.attack > 0)
        {
            currentHealth -= attack.attack;
            var attackDir = new Vector2(transform.position.x - attack.transform.position.x, 0).normalized;
            HurtEvent?.Invoke(attackDir);
            Display();
        }
        else
        {
            currentHealth = 0;
            DieEvent?.Invoke();
            Display();
        }

        yield return new WaitForSeconds(invulnerableDuration);

        invulnerable = false;
    }

    public void Display()
    {
        HealthChangeEvent?.Invoke(currentHealth / maxHealth);
    }
}
