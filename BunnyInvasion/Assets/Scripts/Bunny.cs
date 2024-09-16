using HealthNamespace;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace BunnyNamespace
{
    public class Bunny : MonoBehaviour
    {
        //Bunny stats
        /*[SerializeField] private float moveSpeed = .5f;*/
        [SerializeField] private float bunnyMaxHealth = 200f;
        [SerializeField] private float bunnyAttackDamage = 10f;
        
        //Component attach fields
        [SerializeField] private HealthBar healthBar;
        private HealthSystem healthSystem;
        

        private void Awake()
        {
            //Setup health
            if (healthSystem == null)
            {
                healthSystem = GetComponent<HealthSystem>();
            }

            healthSystem.Setup(bunnyMaxHealth);
            healthBar.Setup(healthSystem);
        }
        private void Update()
        {
            HandleRoaming();
            HandleInteraction();
        }

        private void HandleRoaming()
        {
            //path-finding

        }
        private void HandleInteraction()
        {
            //Touch player then do damage
        }

        private void SpawnBunny()
        {
            //Spawn random in map in a specific timing
        }

        public float GetAttackDamage()
        {
            return bunnyAttackDamage;
        }
        public HealthSystem GetHealthSystem()
        {
            return healthSystem;
        }
    }
}

