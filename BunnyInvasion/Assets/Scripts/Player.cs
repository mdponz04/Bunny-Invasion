using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player Instance {  get; private set; }

    [SerializeField] private float moveSpeed = 7f;
    [SerializeField] private GameInput gameInput;
    private bool isWalking;
    private Rigidbody2D playerRigidbody2D;
    private Vector3 moveDir;

    private void Awake()
    {
        if(Instance != null)
        {
            Debug.LogError("There is more than 1 player!!!");
        }

        Instance = this;

        playerRigidbody2D = GetComponent<Rigidbody2D>();
    }
    private void Update()
    {
        HandleMovement();
        
    }

    private void HandleMovement()
    {
        Vector2 inputVector = gameInput.GetMovementVectorNormalized();

        moveDir = new Vector3(inputVector.x, inputVector.y, 0f);
        /*transform.position += moveDir * moveSpeed * Time.deltaTime;*/

        

        isWalking = moveDir != Vector3.zero;
    }
    private void FixedUpdate()
    {
        playerRigidbody2D.MovePosition(transform.position + moveDir * moveSpeed * Time.fixedDeltaTime);
    }
    public bool IsWalking()
    {
        return isWalking;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Enter Collision!!!");
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        Debug.Log("Stay Collision!!!");
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        Debug.Log("Exit Collision!!!");
    }
}
