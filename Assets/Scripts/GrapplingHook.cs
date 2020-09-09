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
    public Vector3 playerPosition;
    Vector3 playerPosForDrawRope;
    public LineRenderer lr;

    [Header("Grappling Hook Stats")]
    [SerializeField]
    public float HookSpeed;

    void Start()
    {
        lr = GetComponent<LineRenderer>();
    }

    void Update()
    {
        if (isHooked)
        {
            DrawRope();
        }
        else
        {
            RemoveRope();
        }

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
    }

    public void GrapplingHookHandler()
    {
        playerPosition = new Vector3(transform.position.x, transform.position.y + 2f, transform.position.z);
        UnityEngine.Debug.DrawRay(playerPosition, cam.transform.forward * 100, Color.red, 2f);
        Ray ray = new Ray(playerPosition, cam.transform.forward);

        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            hookPosition = hit.point;
            isHooked = true;
        }
    }

    void DrawRope()
    {
        playerPosForDrawRope = new Vector3(transform.position.x, transform.position.y + 2f, transform.position.z);
        lr = GetComponent<LineRenderer>();
        lr.SetPosition(0, playerPosForDrawRope);
        lr.SetPosition(1, hookPosition);
    }

    void RemoveRope()
    {
        Vector3 normal = new Vector3(0f, 0f, 0f);
        lr = GetComponent<LineRenderer>();
        lr.SetPosition(0, normal);
        lr.SetPosition(1, normal);
    }
}
