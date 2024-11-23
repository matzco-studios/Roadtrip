using UnityEngine;

namespace Player
{
    public class CameraController : MonoBehaviour
    {
        [SerializeField] private float _sensitivity = 2.0f;
        [SerializeField] private Transform _head;
        private float _verticalRotation = 0;
        private Vector2 _recoil;

        public void ApplyRecoilEffect(float amnt){
            _recoil.y += amnt;
        }

        void Update()
        {
            // Rotate camera and head
            float mouseX = Input.GetAxis("Mouse X") * _sensitivity;
            float mouseY = Input.GetAxis("Mouse Y") * _sensitivity;

            mouseX += _recoil.x;
            mouseY += _recoil.y;

            _recoil = Vector2.Lerp(_recoil, Vector2.zero, Time.deltaTime*50);

            _verticalRotation = Mathf.Clamp(_verticalRotation - mouseY, -90f, 90f);

            _head.localEulerAngles = Vector3.right * _verticalRotation;
            transform.Rotate(Vector3.up * mouseX);
        }
    }
}