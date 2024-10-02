using HealthNamespace;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Tilemaps;
using DamageNamespace;
using mapNamespace;

namespace BunnyNamespace
{
    public class Bunny : MonoBehaviour, IDamageSource
    {
        //Bunny stats
        [SerializeField] private float moveSpeed = 5f;
        [SerializeField] private float maxHealth = 20f;
        [SerializeField] private float attackDamage = 10f;
        [SerializeField] private float attackCooldown = 5f;
        
        
        //Component attach fields
        [SerializeField] private HealthBar healthBar;
        private BunnyPathfinding pathfinding;
        
        
        private HealthSystem healthSystem;
        Vector3 moveDir;
        private float nextAttackTime = 0f;
        private DamageDealer damageDealer;
        private List<Vector3> pathVectorList;
        private int currentPathIndex;

        private void Awake()
        {
            //Setup health
            if (healthSystem == null)
            {
                healthSystem = GetComponent<HealthSystem>();
            }

            healthSystem.Setup(maxHealth);
            healthBar.Setup(healthSystem);

            damageDealer = GetComponent<DamageDealer>();
            Debug.Log(damageDealer);

            healthSystem.OnDeath += HandleOnDeath;

            /*pathfinding = GetComponentInParent<BunnyPathfinding>();*/
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
            /*HandleMove();*/
        }

        private void HandleMove()
        {        
            pathfinding.SetTargetPosition(transform.position, out int pathIndex, out List<Vector3> path);
            pathVectorList = path;
            currentPathIndex = pathIndex;
            HandleTrackPlayer();
        }

        public void HandleTrackPlayer()
        {
            if (pathVectorList != null)
            {
                pathfinding.UpdateWalkableNodes();
                Vector3 targetPosition = pathVectorList[currentPathIndex];

                if (Vector3.Distance(transform.position, targetPosition) > .5f)
                {
                    Vector3 moveDir = (targetPosition - transform.position).normalized;
                    /*Debug.Log("Move direction vector: " + moveDir);*/
                    moveDir.z = 0f;
                        
                    transform.position += moveDir * Time.deltaTime * moveSpeed;
                }
                else
                {
                    currentPathIndex++;
                    if (currentPathIndex >= pathVectorList.Count)
                    {
                        StopMoving();
                    }
                }
            }
        }
        private void StopMoving()
        {
            pathVectorList = null;
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("Player"))
            {
                Damageable target = collision.GetComponent<Damageable>();
                StartCoroutine(HandleAttacking(target)); // Start attacking coroutine
            }
        }

        private IEnumerator HandleAttacking(Damageable target)
        {
            while (target != null) // While target is valid
            {
                if (Time.time >= nextAttackTime)
                {
                    damageDealer.DoDamage(target); // Apply damage
                    nextAttackTime = Time.time + attackCooldown; // Reset cooldown
                }
                yield return null; // Wait until the next frame
            }
        }

        float IDamageSource.DealDamage() => attackDamage;
        float IDamageSource.GetAttackCooldown() => attackCooldown;
    }
}

