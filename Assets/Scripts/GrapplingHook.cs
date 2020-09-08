using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using UnityEngine;

public class GrapplingHook : MonoBehaviour
{
    public Transform cam;
    public GameObject Player;
    public bool isHooked = false;
    public Vector3 hookPosition;
    public Vector3 playerToHook;

    [Header("Grappling Hook Stats")]
    [SerializeField]
    public float HookSpeed;


    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            if (!isHooked)
            {
                GrapplingHookHandler();
            }
            else
            {
                isHooked = false;
            }
        }
        if (isHooked)
        {
            
            UnityEngine.Debug.Log("isHooked true and working");
            Vector3 playerPosition = new Vector3(transform.position.x, transform.position.y + 2f, transform.position.z);
            playerToHook = (hookPosition - playerPosition);

            //if(playerToHook.magnitude > 5) - kommentard ut, minimum distance så player inte hamnar i väggen.

            Player.GetComponent<Rigidbody>().AddForce(playerToHook * HookSpeed, ForceMode.Impulse);

        }
    }

    public void GrapplingHookHandler()
    {
        Vector3 playerPosition = new Vector3(transform.position.x, transform.position.y + 2f, transform.position.z);
        UnityEngine.Debug.DrawRay(playerPosition, cam.transform.forward * 100, Color.red, 2f);
        Ray ray = new Ray(playerPosition, cam.transform.forward);

        RaycastHit hit;
        if(Physics.Raycast (ray, out hit))
        {
            hookPosition = hit.point;
            isHooked = true;
        }



        /*GameObject clone;
        clone = Instantiate(bullet, playerPosition, Quaternion.identity);
        clone.GetComponent<Rigidbody>().AddForce(cam.transform.forward * HookSpeed, ForceMode.Impulse);
        Destroy(clone, 3);*/



    }
}
