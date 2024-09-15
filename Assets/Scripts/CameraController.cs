using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private float _sensitivity = 2.0f;
    [SerializeField] private Transform _head;
    private float _verticalRotation = 0;

    void Update()
    {
        // Rotate camera and head
        float mouseX = Input.GetAxis("Mouse X")*_sensitivity;
        float mouseY = Input.GetAxis("Mouse Y")*_sensitivity;

        _verticalRotation = Mathf.Clamp(_verticalRotation - mouseY, -90f, 90f);

        _head.localEulerAngles=Vector3.right*_verticalRotation;
        transform.Rotate(Vector3.up*mouseX);
    }
}
