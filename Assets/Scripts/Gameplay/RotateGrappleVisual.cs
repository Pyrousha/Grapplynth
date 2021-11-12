﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateGrappleVisual : MonoBehaviour
{
    [SerializeField] private GrappleGun grappleGun;

    private Quaternion desiredRotation;
    private float rotationSpeed = 10;

    // Update is called once per frame
    void Update()
    {
        if (grappleGun.IsGrappling == false)
        {
            desiredRotation = Quaternion.Euler(Vector3.zero);
            transform.localRotation = Quaternion.Lerp(transform.localRotation, desiredRotation, Time.deltaTime * rotationSpeed);
        }
        else
        {
            desiredRotation = Quaternion.LookRotation(grappleGun.GetGrapplePoint - transform.position);
            transform.rotation = Quaternion.Lerp(transform.rotation, desiredRotation, Time.deltaTime * rotationSpeed);
        }
    }
}
