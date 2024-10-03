using HealthNamespace;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DamageNamespace
{
    public class DamageDealer : MonoBehaviour
    {
        private Damageable target;
        private IDamageSource damageSource;
        private float attackCooldown;
        
        private void Start()
        {
            damageSource = GetComponentInParent<IDamageSource>();
            attackCooldown = damageSource.GetAttackCooldown();
            Debug.Log(damageSource.GetAttackCooldown() + ", dmg: " + damageSource.DealDamage());
        }
        private void OnTriggerEnter2D(Collider2D collision)
        {
            /*Debug.Log(collision.name);*/
            if (!collision.CompareTag("Player") && !collision.CompareTag(damageSource.GetTag()))
            {
                target = collision.GetComponent<Damageable>();
                DoDamage(target);
            }
        }
        public void DoDamage(Damageable target)
        {
            if(target != null && damageSource != null)
            {
                target.TakeDamage(damageSource.DealDamage());
                Debug.Log("target " + target.name + " take: " + damageSource.DealDamage() + " damage.");
            }
        }
    }
}

