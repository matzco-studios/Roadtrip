using System;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

namespace Map
{
    public class Zone : MonoBehaviour
    {
        [SerializeField] private PostProcessVolume _ppv;
        private Transform _player;
        private const float ActivationDistance = 0.02f;
        
        private void Start()
        {
            _player = GameObject.FindGameObjectWithTag("Player").transform;
            _ppv.weight = 0;
        }

        private bool IsInTheZone() => _player.transform.position.z - transform.position.z <= ActivationDistance;

        private void Update()
        {
            var inZone = IsInTheZone();
            
            _ppv.weight = Mathf.Lerp(
                _ppv.weight, 
                inZone ? 1f : 0f, 
                inZone ? 3 * Time.deltaTime : Time.deltaTime / 4
            );

            transform.position = new (transform.position.x, transform.position.y, transform.position.z + 0.75f * Time.deltaTime);
        }
    }
}