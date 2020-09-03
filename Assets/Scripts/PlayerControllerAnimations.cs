using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControllerAnimations : MonoBehaviour
{
    Animator anim;

    bool isGrounded;
    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;


    void Start()
    {
        anim = GetComponent<Animator>();
        anim.SetBool("isWalking", false);
    }

    void Update()
    {

        bool wPress = Input.GetKey("w");
        bool aPress = Input.GetKey("a");
        bool sPress = Input.GetKey("s");
        bool dPress = Input.GetKey("d");
        bool shiftPress = Input.GetKey(KeyCode.LeftShift);

        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if (shiftPress && isGrounded)
        {
            anim.SetBool("isSprinting", true);
            anim.SetBool("isWalking", false);
        }

        if (!shiftPress && isGrounded)
        {
            anim.SetBool("isSprinting", false);
            anim.SetBool("isWalking", true);
        }


        if ((wPress || aPress || sPress || dPress) && isGrounded)
        {
            anim.SetBool("isWalking", true);
        }
        if ((!wPress && !aPress && !sPress && !dPress) && isGrounded)
        {
            anim.SetBool("isWalking", false);
        }
    }
}