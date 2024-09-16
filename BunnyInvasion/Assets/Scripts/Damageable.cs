using HealthNamespace;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DamageNamespace
{
    public class Damageable : MonoBehaviour
    {
        private HealthSystem healthSystem;
        private void Start()
        {
            healthSystem = GetComponent<HealthSystem>();
            if (healthSystem == null)
            {
                Debug.LogError("HealthSystem not found!");
            }
        }


    }
}


