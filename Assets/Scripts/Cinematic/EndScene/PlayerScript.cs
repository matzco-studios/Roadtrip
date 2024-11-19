using System;
using UnityEngine;

namespace Cinematic.EndScene
{
    public class PlayerScript : MonoBehaviour
    {
        [SerializeField] private float _movementSpeed = 4;
        [SerializeField] private CharacterController _controller;
        private Vector3 _direction;
        private Vector3 _velocity = Vector3.zero;

        private void OnEnable()
        {
            transform.position = new (15.334f, 1.08f, transform.position.z);
            transform.SetParent(null);
            _controller.enabled = true;
        }

        private void Movement()
        {
            Vector3 targetVelocity = _movementSpeed * _direction;
            _velocity += (targetVelocity - _velocity) / 15;
            _controller.Move(_velocity * Time.fixedDeltaTime);
        }

        void FixedUpdate()
        {
            Movement();
        }
    }
}