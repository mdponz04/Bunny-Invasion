using HealthNamespace;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace BunnyNamespace
{
    public class Bunny : MonoBehaviour, IDamageSource
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
            HandleFinding();
            HandleAttack();

            
        }

        private void HandleFinding()
        {
            //Path finding

        }
        private void HandleAttack()
        {
            //Touch player then do damage
        }

        
        float IDamageSource.dealDamage() => bunnyAttackDamage;
    }
}

