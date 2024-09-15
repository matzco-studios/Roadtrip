using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private float _sensitivity = 2.0f;
    private float _verticalRotation = 0;
    [SerializeField] private Transform _head;

    void Update(){
        float mouseX = Input.GetAxis("Mouse X")*_sensitivity;
        float mouseY = Input.GetAxis("Mouse Y")*_sensitivity;

        _verticalRotation = Mathf.Clamp(_verticalRotation - mouseY, -90f, 90f);

        _head.localEulerAngles=Vector3.right*_verticalRotation;
        transform.Rotate(Vector3.up*mouseX);
        
    }
}
