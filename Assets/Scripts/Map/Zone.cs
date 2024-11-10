using System;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

namespace Map
{
    public class Zone : MonoBehaviour
    {
        [SerializeField] private PostProcessVolume zone;
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
                zone.isGlobal = true;
            }
            else
            {
                print("Player is outside the zone.");
                zone.isGlobal = false;
            }

            transform.position = new (transform.position.x, transform.position.y, transform.position.z + 0.75f * Time.deltaTime);
        }
    }
}