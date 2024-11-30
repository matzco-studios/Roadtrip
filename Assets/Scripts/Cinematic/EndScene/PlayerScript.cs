using System;
using System.Collections;
using Map;
using UnityEngine;

namespace Cinematic.EndScene
{
    public class PlayerScript : MonoBehaviour
    {
        [SerializeField] private float _movementSpeed = 5f;
        [SerializeField] private CharacterController _controller;
        [SerializeField] private Zone _zone;
        [SerializeField] private CameraScript _camera;
        private Animator _animator;
        private bool _stopped;
        
        public void StopPlayer() => _stopped = true;
        
        private void OnEnable()
        {
            _animator = GetComponent<Animator>();
            _animator.enabled = true;
            transform.SetParent(null);
            transform.position = new (transform.position.x - 2f, transform.position.y, transform.position.z);
            _controller.enabled = true;
            _zone.Speed = 7f;
            StartCoroutine(PlayerMovement());
        }

        private IEnumerator PlayerMovement()
        {
            while (!_stopped)
            {
                yield return null;
                _controller.Move(transform.TransformDirection(new Vector3(0, 0f, _movementSpeed * Time.deltaTime)));
                
                if (_zone.IsInTheZone() && !_camera.enabled)
                {
                    _camera.enabled = true;
                    _animator.enabled = false;
                }
            }
        }
    }
}