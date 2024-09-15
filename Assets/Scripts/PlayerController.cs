using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float movementSpeed = 4;
    [SerializeField] private float jumpForce = 5;
    [SerializeField] private Transform head;
    [SerializeField] private float coyoteTimeMax = 0.21f;
    [SerializeField] private float jumpBufferMax = 0.21f;
    [SerializeField] private float gravityStrength = -9.81f;
    private float coyoteTime;
    private float jumpBuffer;
    private CharacterController controller;
    private Vector3 velocity = Vector3.zero;
    private Vector3 gravityVelocity = Vector3.zero;
    // Start is called before the first frame update
    void Start()
    {
        UnityEngine.Cursor.lockState = CursorLockMode.Locked;
        controller = GetComponent<CharacterController>();
    }

    void Update(){
        // Handle jump mechanics
        coyoteTime -= Time.deltaTime;
        jumpBuffer -= Time.deltaTime;

        if (Input.GetKeyDown(KeyCode.Space)){
            jumpBuffer = jumpBufferMax;
        }

        if (jumpBuffer>=0 && coyoteTime>=0){
            gravityVelocity.y = jumpForce;
        }
    }
    
    void FixedUpdate()
    {
        // Get input direction
        // (Using "GetAxisRaw" instead of "GetAxis" because "GetAxis" has smoothing applied, which makes the controls feel slightly unresponsive)
        Vector3 inputDir = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical")).normalized;
        Vector3 direction = Quaternion.AngleAxis(head.eulerAngles.y, Vector3.up) * inputDir;

        // Handle movement (velocity)
        Vector3 targetVelocity = movementSpeed * direction;

        velocity += (targetVelocity-velocity)/15;
        if (inputDir.Equals(Vector3.zero)){
            velocity += (Vector3.zero-velocity)/10;
        }
        
        controller.Move(velocity * Time.fixedDeltaTime);

        // Handle gravity
        gravityVelocity.y += gravityStrength * Time.fixedDeltaTime * ( (gravityVelocity.y<0) ? 1.65f : 1 );
        
        controller.Move(gravityVelocity * Time.fixedDeltaTime);

        if (controller.collisionFlags == CollisionFlags.Below){
            gravityVelocity.y = 0;
            coyoteTime = coyoteTimeMax;
        }
    }
}
