using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthSystem : MonoBehaviour
{
    public event EventHandler OnHealthChanged;
    public int health { get; private set; }
    public int maxHealth { get; private set; }
    public HealthSystem(int maxHealth)
    {
        this.maxHealth = maxHealth;
        this.health = maxHealth;
    }

    public void Damage(int DamageAmount)
    {
        health -= DamageAmount;
        if(health < 0)
        {
            health = 0;
        }
        OnHealthChanged?.Invoke(this, EventArgs.Empty);
    }
    
    public void Heal(int HealAmount)
    {
        health += HealAmount;
        if (health > maxHealth) 
        {
            health = maxHealth;
        }
        OnHealthChanged?.Invoke(this, EventArgs.Empty);
    }

    public float GetHealthPercent()
    {
        return (float)health / maxHealth;
    }
}
