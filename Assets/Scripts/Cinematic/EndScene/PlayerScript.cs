using System;
using System.Collections;
using Map;
using UnityEngine;

namespace Cinematic.EndScene
{
    public class PlayerScript : MonoBehaviour
    {
        [SerializeField] private float _movementSpeed = 4;
        [SerializeField] private CharacterController _controller;
        private Vector3 _direction;
        [SerializeField] private Zone _zone;
        [SerializeField] private CameraScript _camera;
        private bool _stopped;
        
        public void StopPlayer() => _stopped = true;
        
        private void OnEnable()
        {
            transform.position = new (15.024f, 1.08f, transform.position.z);
            transform.SetParent(null);
            _controller.enabled = true;
            _zone.Speed = 5f;
            StartCoroutine(PlayerMovement());
        }

        private IEnumerator PlayerMovement()
        {
            while (!_stopped)
            {
                yield return null;
                _controller.Move(transform.TransformDirection(new Vector3(0, 0f, -2f * Time.deltaTime)));
                
                if (_zone.IsInTheZone() && !_camera.enabled)
                {
                    _camera.enabled = true;
                }
            }
        }
    }
}