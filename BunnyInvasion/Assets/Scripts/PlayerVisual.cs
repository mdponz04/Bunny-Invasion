using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PlayerNamespace
{
    public class PlayerVisual : MonoBehaviour
    {
        [SerializeField] private Player player;
        [SerializeField] private Transform playerVisualTransform;

        private Animator animator;
        private bool isWalking;

        private void Awake()
        {
            animator = playerVisualTransform.GetComponent<Animator>();
            player.OnAttack += HandleAttacking;
        }

        private void HandleAttacking(object sender, Player.OnAttackEventArgs e)
        {
            animator.SetTrigger("Attack");
        }
        
        private void Update()
        {
            HandleRotating();
            HandleWalking();
        }

        private void HandleWalking()
        {
            isWalking = player.IsWalking();
            animator.SetBool("IsWalking", isWalking);
        }
        private void HandleRotating()
        {
            //Get the move direction 
            Vector2 moveDir = player.GetMoveDir();

            
            if(moveDir == Vector2.zero)
            {
                return;
            }

            float angle = Mathf.Atan2(moveDir.y, moveDir.x) * Mathf.Rad2Deg;
            playerVisualTransform.eulerAngles = new Vector3(0f, 0f, angle);

            Vector3 localScale = new Vector3(5f, 5f, 5f);

            if (angle > 90 || angle < -90)
            {
                localScale.y = -5f;
            }
            else
            {
                localScale.y = +5f;
            }
            playerVisualTransform.localScale = localScale;
        }
    }
}

