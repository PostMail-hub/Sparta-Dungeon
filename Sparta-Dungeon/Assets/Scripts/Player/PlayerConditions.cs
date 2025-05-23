using System;
using UnityEngine;

public interface IDamagable
{
    void TakePhysicalDamage(int damage);
}


public class PlayerConditions : MonoBehaviour, IDamagable
{
    public UICondition uiCondition;

    Condition health { get { return uiCondition.health; } }

    public event Action onTakeDamage;

    void Update()
    {
        if (health.curValue <= 0f)
        {
            Die();
        }
    }

    public void Heal(float amount)
    {
        health.Add(amount);
    }

    public void Die()
    {
        Time.timeScale = 0;
    }

    public void TakePhysicalDamage(int damage)
    {
        health.Subtract(damage);
        onTakeDamage?.Invoke();
    }
}