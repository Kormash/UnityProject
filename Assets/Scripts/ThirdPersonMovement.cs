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
    public bool hasMomentum;
    float turnSmoothVelocity;
    public float turnSmoothTime = 0.1f;

    [Header("Hook Stats")]
    [SerializeField]
    public bool isHooked;
    [SerializeField]
    public Vector3 hookPosition;
    [SerializeField]
    public float grappleSpeed = 1f;
    public Vector3 playerToHook;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        isGrounded = false;
        hasMomentum = false;
    }

    void Update()
    {

        #region check if isHooked
        GameObject go = GameObject.Find("Player");
        GrapplingHook sc = go.GetComponent<GrapplingHook>();
        isHooked = sc.isHooked;
        hookPosition = sc.hookPosition;

        if (isHooked)
        {
            hasMomentum = true;

            Vector3 playerPosition = new Vector3(transform.position.x, transform.position.y + 2f, transform.position.z);
            playerToHook = (hookPosition - playerPosition);

            if (playerToHook.magnitude > 2)
            {
                controller.Move(playerToHook.normalized * Time.deltaTime * grappleSpeed * 600);
            }

        }else if (isGrounded)
        {
            hasMomentum = false;
        }

        if (hasMomentum && !isHooked)
        {
            controller.Move(playerToHook.normalized * Time.deltaTime * grappleSpeed * 400);
            speed = 40;
        }

        #endregion

        #region Jumping
        //jump
        if (isGrounded && velocity.y < 0 && !isHooked)
        {
            velocity.y = -10f;
        }

        if (Input.GetButtonDown("Jump") && isGrounded && !isHooked)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2 * gravity);
        }
        #endregion

        #region gravity

        if (!isHooked)
        {
            velocity.y += gravity * Time.deltaTime;
            controller.Move(velocity * Time.deltaTime);
        }
        #endregion

        #region walking

        if (Input.GetKey(KeyCode.LeftShift) && isGrounded && !isHooked)
        {
            if(speed < 16)
            {
                speed = speed + 0.1f;
            }
            else
            {
                speed = speed - 0.2f;
            }
        }

        if (!Input.GetKey(KeyCode.LeftShift) && isGrounded && !isHooked)
        {
            if (speed > 8)
            {
                speed = speed - 0.2f;
            }
            else
            {
                speed = speed +0.2f;
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

            if (!isHooked)
            {
                moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
                controller.Move(moveDir.normalized * speed * Time.deltaTime);
            }

            currentspeed = speed;
        }
        else
        {
            currentspeed = 0;
        }

        #endregion
    }

    #region isGrounded / touching wall
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
    #endregion
}