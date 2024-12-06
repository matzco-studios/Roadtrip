using System;
using System.Collections;
using Map;
using UnityEngine;

namespace Cinematic.EndScene
{
    public class PlayerScript : MonoBehaviour
    {
        [SerializeField] private float _movementSpeed = 4f;
        [SerializeField] private CharacterController _controller;
        [SerializeField] private Animator _animator;
        private Zone _zone;
        private CameraScript _camera;
        private bool _stopped;

        public void Stop() => _stopped = true;
      
        private void Start() {
            _zone = GameObject.FindWithTag("Zone").GetComponent<Zone>();
            _camera = GameObject.FindWithTag("Camera").GetComponent<CameraScript>();
        }

        public IEnumerator PlayerMovement()
        {
            _camera.PlayerZoom();
            _animator.enabled = true;
            yield return null;

            transform.SetParent(null);
            transform.position = new (transform.position.x - 2f, transform.position.y, transform.position.z);
            _controller.enabled = true;
            _zone.Speed = 9f;
            
            while (!_stopped)
            {
                yield return null;
                _controller.Move(transform.TransformDirection(new Vector3(0, 0f, _movementSpeed * Time.deltaTime)));
                
                if (_zone.IsInTheZone() && !_camera.IsEndRotation())
                {
                    StartCoroutine(_camera.EndRotation());
                }
            }

            _animator.enabled = false;
        }
    }
}