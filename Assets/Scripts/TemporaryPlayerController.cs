using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float movementSpeed = 7;
    [SerializeField] private float jumpForce = 5;
    private Collider col;
    private Rigidbody body;
    [SerializeField] private Transform head;
    // Start is called before the first frame update
    void Start()
    {
        UnityEngine.Cursor.lockState = CursorLockMode.Locked;
        col = GetComponent<Collider>();
        body = GetComponent<Rigidbody>();
    }

    static float GetKey(KeyCode key) 
    {
        return Input.GetKey(key) ? 1.0f : 0.0f;
    }

    void Update(){
        if (Input.GetKeyDown(KeyCode.Space)){
            body.velocity += Vector3.up*jumpForce;
        }
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 inputDir = new(GetKey(KeyCode.D)-GetKey(KeyCode.A), 0, GetKey(KeyCode.W)-GetKey(KeyCode.S));
        inputDir.Normalize();
        Vector3 direction = Quaternion.AngleAxis(head.eulerAngles.y, Vector3.up) * inputDir;

        float amntToReduce = new Vector3(body.velocity.x, 0, body.velocity.z).magnitude;
        body.drag = amntToReduce;
        body.AddForce(movementSpeed * direction);
    }
}
