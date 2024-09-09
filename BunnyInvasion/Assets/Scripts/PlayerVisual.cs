using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerVisual : MonoBehaviour
{
    public event EventHandler<OnAttackEventArgs> OnAttack;
    public class OnAttackEventArgs : EventArgs
    {
        public Vector3 attackEndPointPosition;
        public Vector3 attackPosition;
    }

    [SerializeField] private Player player;

    private Transform playerVisualTransform;
    private Transform attackEndPointPositionTransform;
    private Animator animator;
    private bool isWalking;

    private void Awake()
    {
        playerVisualTransform = transform.Find("PlayerVisual");
        animator = playerVisualTransform.GetComponent<Animator>();
        attackEndPointPositionTransform = playerVisualTransform.Find("AttackEndPointPosition");
    }

    private void Start()
    {
        
        
    }
    private void Update()
    {
        HandleAiming();
        HandleAttacking();
        HandleWalking();
    }
    private void HandleAttacking()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mousePosition = GetMouseWorldPosition2D();

            animator.SetTrigger("Attack");
            OnAttack?.Invoke(this, new OnAttackEventArgs
            {
                attackEndPointPosition = attackEndPointPositionTransform.position,
                attackPosition = mousePosition,
            });
        }
    }

    private void HandleWalking()
    {
        isWalking = player.IsWalking();
        animator.SetBool("IsWalking", isWalking);

        Debug.Log("IsWalking: " + isWalking);
    }

    private void HandleAiming()
    {
        Vector3 mousePosition = GetMouseWorldPosition2D();

        Vector3 aimDir = (mousePosition - transform.position).normalized;
        float angle = Mathf.Atan2(aimDir.y, aimDir.x) * Mathf.Rad2Deg;
        playerVisualTransform.eulerAngles = new Vector3(0f, 0f, angle);

        Debug.Log(angle);

        Vector3 localScale = new Vector3(5f,5f,5f);
        if(angle > 90 || angle < -90)
        {
            localScale.y = -5f;
        }
        else
        {
            localScale.y = +5f;
        }
        playerVisualTransform.localScale = localScale;
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
}
