using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TemporaryCameraController : MonoBehaviour
{
    [SerializeField] private float sensitivity = 2.0f;
    private float verticalRotation = 0;
    [SerializeField] Transform head;

    void Update(){
        float mouseX = Input.GetAxis("Mouse X")*sensitivity;
        float mouseY = Input.GetAxis("Mouse Y")*sensitivity;

        verticalRotation = Mathf.Clamp(verticalRotation - mouseY, -90f, 90f);

        head.localEulerAngles=Vector3.right*verticalRotation;
        transform.Rotate(Vector3.up*mouseX);
        
    }
}
