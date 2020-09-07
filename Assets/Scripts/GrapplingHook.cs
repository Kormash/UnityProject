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
    public GameObject bullet;

    [Header("Grappling Hook Stats")]
    [SerializeField]
    public float HookSpeed;


    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            GrapplingHookHandler();
        }

    }

    public void GrapplingHookHandler()
    {
        Vector3 playerPosition = new Vector3(transform.position.x, transform.position.y + 2f, transform.position.z);
        UnityEngine.Debug.DrawRay(playerPosition, cam.transform.forward * 100, Color.red, 2f);

        GameObject clone;
        clone = Instantiate(bullet, playerPosition, Quaternion.identity);
        clone.GetComponent<Rigidbody>().AddForce(cam.transform.forward * HookSpeed, ForceMode.Impulse);
        Destroy(clone, 2);
    }
}
