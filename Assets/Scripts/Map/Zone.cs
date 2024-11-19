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
        [SerializeField] private GameObject _player;
        private PlayerController _playerController;
        private const float ActivationDistance = 0.02f;
        private bool _inZone;
        public float Speed = 0.75f;
        
        private void Start()
        {
            _player = GameObject.FindGameObjectWithTag("Player");
            _playerController = _player.GetComponent<PlayerController>();
            _ppv.weight = 0;
            if(_playerController) StartCoroutine(RemovePlayerHealth());
        }

        private bool IsInTheZone() => _player.transform.position.z - transform.position.z <= ActivationDistance;

        IEnumerator RemovePlayerHealth()
        {
            while (true)
            {
                yield return null;
                
                if (_inZone)
                {
                    _playerController.ReduceHealth(2 * Time.deltaTime);
                    print(_playerController.Health);
                }
            }
        }
        
        private void Update()
        {
            _inZone = IsInTheZone();

            _ppv.weight = Mathf.Lerp(
                _ppv.weight, 
                _inZone ? 1f : 0f, 
                _inZone ? 3 * Time.deltaTime : Time.deltaTime / 4
            );

            transform.position = new (transform.position.x, transform.position.y, transform.position.z + Speed * Time.deltaTime);
        }
    }
}