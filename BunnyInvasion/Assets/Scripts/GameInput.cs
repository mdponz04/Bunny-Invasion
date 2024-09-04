using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameInput : MonoBehaviour
{
    private PlayerInputActions playerInputActions;
    
    private void Awake()
    {
        playerInputActions = new PlayerInputActions();
        playerInputActions.Player.Enable();
    }

    public Vector2 GetMovementVectorNormalized()
    {
        //Input system package
        Vector2 inputVector = playerInputActions.Player.Move.ReadValue<Vector2>();
        
        //legacy input
        /*Vector2 inputVector = new Vector2(0f, 0f);

        if (Input.GetKey(KeyCode.W))
        {
            inputVector.y++;
        }

        if (Input.GetKey(KeyCode.S))
        {
            inputVector.y--;
        }

        if (Input.GetKey(KeyCode.A))
        {
            inputVector.x--;
        }

        if (Input.GetKey(KeyCode.D))
        {
            inputVector.x++;
        }*/

        inputVector = inputVector.normalized;

        return inputVector;
    }
}
