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
            HealthSystem healthSystem = collision.GetComponent<HealthSystem>();
            
        }

    }
}

