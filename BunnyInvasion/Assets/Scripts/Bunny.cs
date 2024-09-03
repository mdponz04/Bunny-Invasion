using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class Bunny : MonoBehaviour
{
    private double healthPoint;
    private double damage;
    [SerializeField] private float moveSpeed = .5f;

    private void Update()
    {
        HandleMovement();
        HandleInteraction();
    }

    private void HandleMovement()
    {
        //path-finding
        Vector3 moveDir = new Vector3(0f,-1f,0f);
        transform.position += moveDir * moveSpeed * Time.deltaTime;
    }
    private void HandleInteraction()
    {
        //Touch the fence and do damage
    }

    private void SpawnBunny()
    {

    }
}
