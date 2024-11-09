using System;
using UnityEngine;

namespace Map
{
    public class Zone : MonoBehaviour
    {
        private Transform _player;
        private const float ActivationDistance = 0.76f;
        
        private void Start()
        {
            _player = GameObject.FindGameObjectWithTag("Player").transform;
        }

        private void Update()
        {
            if (_player.transform.position.z - transform.position.z <= ActivationDistance)
            {
                print("Player is in the zone.");
            }
        }
    }
}