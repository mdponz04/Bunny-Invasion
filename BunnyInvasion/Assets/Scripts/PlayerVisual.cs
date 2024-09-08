using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerVisual : MonoBehaviour
{
    [SerializeField] private Player player;
    private Transform playerVisualTransform;
    private Animator animator;
    private bool isWalking;

    private void Awake()
    {
        playerVisualTransform = transform.Find("PlayerVisual");
        animator = playerVisualTransform.GetComponent<Animator>();
        
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
            animator.SetTrigger("Attack");
            
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
