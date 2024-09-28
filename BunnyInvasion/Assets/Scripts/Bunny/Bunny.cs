using HealthNamespace;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Tilemaps;
using DamageNamespace;

namespace BunnyNamespace
{
    public class Bunny : MonoBehaviour, IDamageSource
    {
        //Bunny stats
        [SerializeField] private float moveSpeed = .5f;
        [SerializeField] private float maxHealth = 20f;
        [SerializeField] private float attackDamage = 10f;
        
        //Component attach fields
        [SerializeField] private HealthBar healthBar;
        
        
        private HealthSystem healthSystem;
        Vector3 moveDir;

        private void Awake()
        {
            //Setup health
            if (healthSystem == null)
            {
                healthSystem = GetComponent<HealthSystem>();
            }

            healthSystem.Setup(maxHealth);
            healthBar.Setup(healthSystem);

            healthSystem.OnDeath += HandleOnDeath;
        }

        private void HandleOnDeath(object sender, System.EventArgs e)
        {
            Debug.Log("Destroy: " + gameObject.name);
            
            //Destroy this object
            Destroy(gameObject);
        }
        private void OnDestroy()
        {
            if (healthSystem != null)
            {
                healthSystem.OnDeath -= HandleOnDeath;
            }
        }

        private void Update()
        {
            HandleMove();
            HandleAttack();

            
        }

        private void HandleMove()
        {
            
            moveDir = new();




            /*transform.position += moveDir * moveSpeed * Time.deltaTime;*/

        }
        private void HandleAttack()
        {
            //Touch player then do damage
        }

        
        float IDamageSource.DealDamage() => attackDamage;
    }
}

