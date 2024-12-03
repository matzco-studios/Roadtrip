using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;
using Animator = UnityEngine.Animator;


namespace Enemies.Sunburned
{
    public class SunburnedEnemy : EnemyController
    {
        [SerializeField] private PostProcessVolume postProcess;
        [SerializeField] private float activationDistance;
        
        private Renderer _renderer;
        private Animator _animator;
        
        private bool _isWatched;

        private void Awake()
        {
            _renderer = GetComponent<Renderer>();
            _animator = transform.GetChild(1).GetComponent<Animator>();
        }

        void Update()
        {
            _isWatched = _renderer.isVisible;
            
            /*
             * Check if player is looking at the sunburned
             * if no, Sunburned is looking at the player & move towards it  
             */
            if (!_isWatched) 
            {
                transform.LookAt(new Vector3(Target.position.x, transform.position.y, Target.position.z));
                transform.position = Vector3.MoveTowards(transform.position, Target.position, speed * Time.deltaTime);
            } 
            _animator.enabled = !_isWatched;
            
            /*
             * Check if player is near, if yes,
             * make use of the Post Process effect
             */
            if (Vector3.Distance(Target.position, transform.position) < activationDistance) {
                postProcess.weight = Mathf.Lerp(
                    postProcess.weight,
                    (_isWatched ? 1f : 0f),
                    speed * Time.deltaTime
                );
            }

        }
    }
}
