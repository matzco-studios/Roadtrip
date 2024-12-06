using System;
using Items.Mechanics;
using Player;
using Player.Mechanics;
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
        [SerializeField]
        private float _shootDist = 150f;
        [SerializeField]
        private float _falloffAmount = 5f;
        [SerializeField]
        private float _minDamage = 5f;
        [SerializeField]
        private int ammo = 40;
        private CameraController _cameraController;
        private Animator _animator;
        private AudioSource _soundReload;
        private AudioSource _soundShoot;
        private int _magazine;
        private float _shootCooldown;
        private bool isReloading = false;
        private RaycastHit _raycastHit;
        private Ray _ray;
        private ParticleSystem _muzzleFlash;
        private Light _light;

        private void Shoot(){
            if (ammo<=0 && _magazine<=0) {DropGun(); return;}
            if (isReloading || _shootCooldown>0) return;
            if (_magazine>0)
            {
                _cameraController.ApplyRecoilEffect(_recoil);
                _shootCooldown = _cooldown;
                _magazine--;
                isReloading = false;
                _soundShoot.Play();
                _animator.SetTrigger("Shoot");
                Transform head = _cameraController.GetHead().transform;
                _ray = new Ray(head.position, head.forward);
                if (Physics.Raycast(_ray, out _raycastHit, _shootDist)){
                    var obj = _raycastHit.collider.gameObject;
                    if (obj.CompareTag("Enemy") || obj.CompareTag("Scorchlet")){
                        float dmg = _damage;
                        _raycastHit.collider.GetComponent<EnemyController>()
                        .Hurt( Mathf.Max(dmg-(_raycastHit.distance/10*_falloffAmount), _minDamage) );
                    }
                }
                _muzzleFlash.Play();
                _light.intensity = 8;
            }else{ _animator.SetTrigger("ShootEmpty");}
        }

        private void Reload(){
            if (ammo<=0) {return;}
            if (isReloading || _shootCooldown>0 || _magazine==_magSize) return;
            isReloading = true;
            _animator.SetTrigger("Reload");
            _soundReload.Play();
        }

        private void DropGun(){
            GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<InventoryController>().DropCurrentItem();
        }

        public void AddAmmo(int amnt)
        {
            amnt = Mathf.Clamp(amnt, 0, _magSize);
            _magazine = Mathf.Clamp(_magazine+amnt, 0, _magSize);
            ammo -= amnt;
            if (_magazine==_magSize || ammo <= 0){
                _animator.SetTrigger("StopReload");
            }
            _soundReload.Play();
            isReloading = false;
        }

        public void PlayReloadSound() { _soundReload.Play(); isReloading = false; }

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
            _muzzleFlash = GetComponentInChildren<ParticleSystem>();
            _light = GetComponentInChildren<Light>();
            foreach (MeshRenderer mesh in GetComponentsInChildren<MeshRenderer>()) {
                mesh.receiveShadows = false;
                mesh.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.Off;
            }
        }

        void Update()
        {
            _shootCooldown -= Time.deltaTime;
            _light.intensity += (0-_light.intensity)*Time.deltaTime*16;
        }
    }
}