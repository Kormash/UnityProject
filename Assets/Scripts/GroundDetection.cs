using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundDetection : MonoBehaviour
{
    public bool isInAir;
    public bool isGrounded;

    [Header("Ground & Air Detection Stats")]
    [SerializeField]
    float groundDetectionRayStartPoint = 0.5f;
    [SerializeField]
    float minimumDistanceNeededToBeginFall = 1f;
    [SerializeField]
    LayerMask ignoreForGroundCheck;
    [SerializeField]
    public float inAirTimer;
    [SerializeField]
    float fallingSpeed = 45;



    void Update()
    {
        
    }
}
