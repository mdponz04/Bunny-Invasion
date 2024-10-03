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
        private Bunny bunny;

        private void Start()
        {
            bunny = GetComponent<Bunny>();
            agent = GetComponent<NavMeshAgent>();
            agent.updateRotation = false;
            agent.updateUpAxis = false;
            agent.speed = bunny.GetMoveSpeed();
            player = Player.Instance;
        }

        private void Update()
        {
            agent.SetDestination(player.GetFootPosition());
            
        }
    }
}

