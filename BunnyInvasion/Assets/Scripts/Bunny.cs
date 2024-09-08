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
        HandleRoaming();
        HandleInteraction();
    }

    private void HandleRoaming()
    {
        //path-finding
        
    }
    private void HandleInteraction()
    {
        //Touch player then do damage
    }

    private void SpawnBunny()
    {
        
    }
}
