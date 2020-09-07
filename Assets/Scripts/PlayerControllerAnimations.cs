using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControllerAnimations : MonoBehaviour
{
    Animator anim;

    public float currentspeed;
    public float fallingspeed;
    bool isGrounded;
    GameObject go;


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
        fallingspeed = sc.velocity.y;
        currentspeed = sc.currentspeed;
        anim.SetFloat("currentspeed", currentspeed);
        anim.SetFloat("fallingspeed", fallingspeed);

        #endregion

        #region checkIfGrounded

        //Copys Bool isGrounded from ThirdPersonMovement (player).
        isGrounded = sc.isGrounded;
        anim.SetBool("isGrounded", isGrounded);

        #endregion


        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            anim.Play("BasicMotions@JumpStart01");
        }

        anim.SetBool("isGrounded", isGrounded);
    }
}