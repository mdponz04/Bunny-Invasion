using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace HealthNamespace
{
    public class HealthSystem : MonoBehaviour
    {
        public event EventHandler OnHealthChanged;
        public event EventHandler OnDeath;
        public float health { get; private set; }
        public float maxHealth { get; private set; }
        public void Setup(float maxHealth)
        {
            this.maxHealth = maxHealth;
            health = maxHealth;
        }

        public void Damage(float DamageAmount)
        {
            health -= DamageAmount;
            if (health < 0)
            {
                health = 0;
            }
            OnHealthChanged?.Invoke(this, EventArgs.Empty);

            //trigger death
            if(health <= 0)
            {

            }
        }

        public void Heal(float HealAmount)
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
            return health / maxHealth;
        }

        public void Die()
        {
            //trigger death event
            OnDeath?.Invoke(this, EventArgs.Empty);

            Debug.Log("Character has died!");
            
        }
    }
}
