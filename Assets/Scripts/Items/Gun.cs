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
        private AudioSource _soundReload;
        private AudioSource _soundShoot;
        private int _magazine;
        private float _shootCooldown;
        private bool isReloading = false;

        private void Shoot(){
            if (isReloading) return;
            if (_shootCooldown>0) return;
            if (_magazine>0)
            {
                _cameraController.ApplyRecoilEffect(_recoil);
                _shootCooldown = _cooldown;
                _magazine--;
                isReloading = false;
                _soundShoot.Play();
                _animator.SetTrigger("Shoot");
            }else{ _animator.SetTrigger("ShootEmpty");}
        }

        private void Reload(){
            isReloading = true;
            _animator.SetTrigger("Reload");
            _soundReload.Play();
        }

        public void AddAmmo(int amnt)
        {
            isReloading = false;
            _magazine = Mathf.Clamp(_magazine+amnt, 0, _magSize);
            if (_magazine==_magSize){
                _animator.SetTrigger("StopReload");
            }
            _soundReload.Play();
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
            _soundShoot = GetComponents<AudioSource>()[0];
            _soundReload = GetComponents<AudioSource>()[1];
        }

        void Update()
        {
            _shootCooldown -= Time.deltaTime;
        }
    }
}