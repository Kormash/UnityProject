using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControllerAnimations : MonoBehaviour
{
    Animator anim;

    //public float test;
    public float currentspeed;
    bool isGrounded;
    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;


    void Start()
    {
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        #region currentspeed 
        //Copys currentspeed from ThirdPersonMovement (player).
        GameObject go = GameObject.Find("Player");
        ThirdPersonMovement sc = go.GetComponent<ThirdPersonMovement>();
        currentspeed = sc.currentspeed;
        anim.SetFloat("currentspeed", currentspeed);
        #endregion


        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);


    }
}