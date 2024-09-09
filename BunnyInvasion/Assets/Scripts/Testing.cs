using CodeMonkey.Utils;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Testing : MonoBehaviour
{
    [SerializeField] private Player player;
    private PlayerVisual playerVisual;

    private void Start()
    {
        playerVisual = player.GetComponent<PlayerVisual>();
        playerVisual.OnAttack += PlayerVisual_OnAttack;
    }

    private void PlayerVisual_OnAttack(object sender, PlayerVisual.OnAttackEventArgs e)
    {
        Debug.Log("Attack from: " + e.attackEndPointPosition + " to: " + e.attackPosition);
    }
}
