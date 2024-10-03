using PlayerNamespace;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
namespace BunnyNamespace
{
    public class TrackPlayer : MonoBehaviour
    {
        private Player player;
        private NavMeshAgent agent;

        private void Start()
        {
            agent = GetComponent<NavMeshAgent>();
            agent.updateRotation = false;
            agent.updateUpAxis = false;
            player = Player.Instance;
        }

        private void Update()
        {
            agent.SetDestination(player.GetFootPosition());
        }
    }
}

