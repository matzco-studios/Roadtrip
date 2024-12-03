using System;
using UnityEngine;

namespace Player
{
    public class CameraController : MonoBehaviour
    {
        #region Members
        
        [SerializeField] private float _sensitivity = 2.0f;
        [SerializeField] private Transform _head;

        // [SerializeField] [Range(10f, 20f)] private float _bobbingSpeed = 10f;
        // private Vector3 _bobbingStartPos;
        // private Vector3 _bobbingPos;

        private float _verticalRotation = 0;
        private Vector2 _recoil;
        
        #endregion

        #region CustomMethods

        public Transform GetHead() => _head;

        public void ApplyRecoilEffect(float amnt){
            _recoil.y += amnt;
        }
        
        /**
         * Made by looking at concept displayed in this article
         * https://twhl.info/wiki/page/Tutorial%3A_View_bobbing%3A_Part_1#:~:text=View%20bobbing%20%2D%20the%20shaking%20or,down%20as%20the%20player%20runs.
         *
         * TODO - Make that a complete bobbing is made when player walk to it always reset to the start position
         * 
         */
        /*
         private void CalculateBobbing()
        {
            _bobbingPos = Vector3.zero;

            // Sin Wave Formula
            _bobbingPos.y += Mathf.Lerp(
                
                _bobbingPos.y, 
                // Frequency
                Mathf.Sin(Time.time * _bobbingSpeed) + (.5f * Time.deltaTime),
                // Amplitude
                1f * Time.deltaTime);

            _head.position += _bobbingPos;
        }
        */

        /*
        private void StopBobbing()
        {
            var tempPos = _head.position;
            tempPos.y = Mathf.Lerp(tempPos.y, _bobbingStartPos.y, .5f * Time.deltaTime);
            _head.position = tempPos;
        }
        */

        #endregion
        
        #region UnityGameMethods
        
        void Update()
        {
            // _bobbingStartPos = _head.position;

            // Rotate camera and head
            var mouseX = Input.GetAxis("Mouse X") * _sensitivity;
            var mouseY = Input.GetAxis("Mouse Y") * _sensitivity;
            
            mouseX += _recoil.x;
            mouseY += _recoil.y;

            _recoil = Vector2.Lerp(_recoil, Vector2.zero, Time.deltaTime*50);
            _verticalRotation = Mathf.Clamp(_verticalRotation - mouseY, -90f, 90f);
            _head.localEulerAngles = Vector3.right * _verticalRotation;
            
            transform.Rotate(Vector3.up * mouseX);

            /*
            if (PlayerController.IsWalking)
            {
                CalculateBobbing(); 
                StopBobbing();
            }
            */
        }
        
        #endregion
    }
}