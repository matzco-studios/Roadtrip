using System;
using UnityEngine;

namespace Cinematic.EndScene
{
    public class PlayerScript : MonoBehaviour
    {
        [SerializeField] private float _movementSpeed = 4;
        [SerializeField] private CharacterController _controller;
        private Vector3 _direction;

        private void OnEnable()
        {
            transform.position = new (15.135f, 1.08f, transform.position.z);
            transform.SetParent(null);
            _controller.enabled = true;
        }

        private void Movement()
        {
            _controller.Move(transform.TransformDirection(new Vector3(0, 0f, -0.1f)));
        }

        void FixedUpdate()
        {
            Movement();
        }
    }
}