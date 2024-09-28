using HealthNamespace;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DamageNamespace
{
    public class DamageDealer : MonoBehaviour
    {
        private void OnTriggerEnter2D(Collider2D collision)
        {
            Debug.Log(collision.name);
            Damageable target = collision.GetComponent<Damageable>();
            IDamageSource damageSource = GetComponentInParent<IDamageSource>();

            DoDamage(target, damageSource);
        }

        private void DoDamage(Damageable target, IDamageSource damageSource)
        {
            if(target != null && damageSource != null)
            {
                target.TakeDamage(damageSource.DealDamage());
                Debug.Log("target take: " + damageSource.DealDamage() + " damage.");
            }
        }
    }
}

