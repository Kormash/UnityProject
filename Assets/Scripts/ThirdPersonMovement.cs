using System;
using System.Collections;


using System.Collections.Generic;
using System.Collections.Specialized;
using System.Diagnostics;
using System.Security.Cryptography;
using System.Security.Permissions;
using System.Threading;
using UnityEngine;

public class ThirdPersonMovement : MonoBehaviour
{
    public CharacterController controller;
    public Transform cam;
    public Rigidbody rb;

    [Header("Movement Stats")]
    [SerializeField]
    public float currentspeed;
    [SerializeField]
    public float speed = 8;
    [SerializeField]
    public float gravity = -9.81f;
    [SerializeField]
    public float jumpHeight = 3;
    public Vector3 velocity;
    public Vector3 moveDir;
    public bool isGrounded;

    float turnSmoothVelocity;
    public float turnSmoothTime = 0.1f;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        isGrounded = false;
    }

    void Update()
    {

        #region Jumping
        //jump
        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -10f;
        }

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2 * gravity);
        }
        #endregion

        //gravity
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);

        #region walking

        if (Input.GetKey(KeyCode.LeftShift) && isGrounded)
        {
            if(speed < 16)
            {
                speed = speed + 0.1f;
            }
        }

        if (!Input.GetKey(KeyCode.LeftShift) && isGrounded)
        {
            if (speed > 8)
            {
                speed = speed - 0.2f;
            }
        }


        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;

        if (direction.magnitude >= 0.1f)
        {

            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            controller.Move(moveDir.normalized * speed * Time.deltaTime);

            currentspeed = speed;
        }
        else
        {
            currentspeed = 0;
        }

        #endregion
    }

    /*   
    void OnCollisionEnter(UnityEngine.Collision collision)
    {        
        if (collision.collider.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }
    */
    

    void OnCollisionExit(UnityEngine.Collision collision)
    {
        if (collision.collider.CompareTag("Ground"))
        {
            isGrounded = false;
        }
    }
    

    void OnCollisionStay(UnityEngine.Collision collision)
    {
        if (collision.collider.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }

    void OnTriggerStay(Collider collision)
    {
        if (collision.GetComponent<Collider>().CompareTag("Ground"))
        {
            //Player is touching a Wall.
            if(!isGrounded && speed > 0.5)
            {
                speed = speed - 0.1f;
            }
        }
    }

}