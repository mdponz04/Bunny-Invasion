using HealthNamespace;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static PlayerNamespace.PlayerVisual;

namespace PlayerNamespace
{
    public class Player : MonoBehaviour, IDamageSource
    {
        //Singleton pattern
        public static Player Instance { get; private set; }
        //Event when attack trigger
        public event EventHandler<OnAttackEventArgs> OnAttack;
        public class OnAttackEventArgs : EventArgs
        {
            public Vector3 attackEndPointPosition;
            public Vector3 attackPosition;
        }


        //Character stats
        [SerializeField] private float moveSpeed = 7f;
        [SerializeField] private float attackCooldown = 1f;
        [SerializeField] private float maxHealth = 100f;
        [SerializeField] private float attackDamage = 5f;
        //Component attach fields
        [SerializeField] private GameInput gameInput;
        [SerializeField] private HealthBar healthBar;
        [SerializeField] private Transform attackEndPointPositionTransform;
        [SerializeField] private BoxCollider2D attackZone;

        private HealthSystem healthSystem;
        private float nextAttackTime = 0f;
        private bool isWalking;
        private Rigidbody2D playerRigidbody2D;
        private Vector3 moveDir;
        private Transform playerTransform;

        private void Awake()
        {
            if (Instance != null)
            {
                Debug.LogError("There is more than 1 player!!!");
            }

            Instance = this;

            //Setup rigidbody for physics interaction
            playerRigidbody2D = GetComponent<Rigidbody2D>();
            //Setup health
            if (healthSystem == null)
            {
                healthSystem = GetComponent<HealthSystem>();
            }

            healthSystem.Setup(maxHealth);
            healthBar.Setup(healthSystem);
        }

        private void Update()
        {
            HandleMovement();
            HandleAttacking();
        }

        private void HandleMovement()
        {
            Vector2 inputVector = gameInput.GetMovementVectorNormalized();

            moveDir = new Vector3(inputVector.x, inputVector.y, 0f);
            /*transform.position += moveDir * moveSpeed * Time.deltaTime;*/

            isWalking = moveDir != Vector3.zero;
        }

        private void HandleAttacking()
        {
            
            if (Input.GetMouseButtonDown(0) && Time.time >= nextAttackTime)
            {
                //Enable attack zone
                EnableAttackZone();
                //perform attack
                Vector3 mousePosition = GetMouseWorldPosition2D();

                //Trigger attack event
                OnAttack?.Invoke(this, new OnAttackEventArgs
                {
                    attackEndPointPosition = attackEndPointPositionTransform.position,
                    attackPosition = mousePosition,
                });

                //reset attack cooldown
                nextAttackTime = Time.time + attackCooldown;

                //Disable attack zone after short delay, the time delay is the time animation action
                float attackAnimationDuration = 0.3f;
                StartCoroutine(DisableAttackZoneAfterDelay(attackAnimationDuration));
            }
        }

        private void FixedUpdate()
        {
            //Handle physics(push enemy away and cannot walk through wall)
            playerRigidbody2D.MovePosition(transform.position + moveDir * moveSpeed * Time.fixedDeltaTime);
        }

        public bool IsWalking()
        {
            return isWalking;
        }

        public Vector2 GetMoveDir()
        {
            Vector2 inputVector = gameInput.GetMovementVectorNormalized();

            moveDir = new Vector3(inputVector.x, inputVector.y, 0f);
            return moveDir;
        }

        private Vector3 GetMouseWorldPosition2D()
        {
            Vector3 vec = GetMouseWorldPositionWithZ(Input.mousePosition, Camera.main);
            vec.z = 0f;
            return vec;
        }

        private Vector3 GetMouseWorldPositionWithZ(Vector3 screenPosition, Camera worldCamera)
        {
            Vector3 worldPosition = worldCamera.ScreenToWorldPoint(screenPosition);
            return worldPosition;
        }

        private void EnableAttackZone()
        {
            attackZone.enabled = true;
        }

        private void DisableAttackZone()
        {
            attackZone.enabled = false;
        }

        private IEnumerator DisableAttackZoneAfterDelay(float delay)
        {
            yield return new WaitForSeconds(delay); // Wait for the delay
            DisableAttackZone();                    // Disable the attack zone after the delay
        }

        public float GetAttackDamage()
        {
            return attackDamage;
        }
        float IDamageSource.dealDamage() => GetAttackDamage();
    }
}


