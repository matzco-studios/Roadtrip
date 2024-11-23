using System;
using Items.Mechanics;
using Player;
using Unity.VisualScripting;
using UnityEngine;

namespace Items
{
    public class Gun : GrabbableItem
    {
        [SerializeField]
        private int _magSize = 7;
        [SerializeField]
        private float _damage = 13f;
        [SerializeField]
        private float _recoil = .8f;
        [SerializeField]
        private float _cooldown = .425f;
        private CameraController _cameraController;
        private Animator _animator;
        private int _magazine;
        private float _shootCooldown;
        private bool isReloading = false;

        private void Shoot(){
            if (_shootCooldown<0 && _magazine>0)
            {
                _cameraController.ApplyRecoilEffect(_recoil);
                _shootCooldown = _cooldown;
                _magazine--;
                isReloading = false;
                _animator.SetTrigger("Shoot");
            }
        }

        private void Reload(){
            isReloading = true;
            _animator.SetTrigger("Reload");
        }

        public Gun()
        {
            ActionDictionary.Add(KeyCode.Mouse0, Shoot);
            ActionDictionary.Add(KeyCode.R, Reload);
        }

        void Start(){
            _cameraController = GameObject.FindGameObjectWithTag("Player").GetComponent<CameraController>();
            _magazine = _magSize;
            _animator =GetComponent<Animator>();
        }

        void Update()
        {
            _shootCooldown -= Time.deltaTime;
        }
    }
}