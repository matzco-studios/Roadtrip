using System;
using System.Collections;
using Player;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

namespace Map
{
    public class Zone : MonoBehaviour
    {
        [SerializeField] private PostProcessVolume _ppv;
        private PlayerController _player;
        private const float ActivationDistance = 0.02f;
        private bool _inZone;
        
        private void Start()
        {
            _player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
            _ppv.weight = 0;
            if(_player) StartCoroutine(RemovePlayerHealth());
        }

        private bool IsInTheZone() => _player.transform.position.z - transform.position.z <= ActivationDistance;

        IEnumerator RemovePlayerHealth()
        {
            while (true)
            {
                yield return null;
                _inZone = IsInTheZone();
                
                if (_inZone)
                {
                    _player.ReduceHealth(2 * Time.deltaTime);
                    print(_player.Health);
                }
            }
        }
        
        private void Update()
        {
            _ppv.weight = Mathf.Lerp(
                _ppv.weight, 
                _inZone ? 1f : 0f, 
                _inZone ? 3 * Time.deltaTime : Time.deltaTime / 4
            );

            transform.position = new (transform.position.x, transform.position.y, transform.position.z + 0.75f * Time.deltaTime);
        }
    }
}