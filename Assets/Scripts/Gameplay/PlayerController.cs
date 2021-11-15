﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Grapplynth
{
    public class PlayerController : MonoBehaviour
    {
        private Rigidbody playerRB;
        [Header("Settings")]
        [SerializeField] private float gravityStrength;

        [SerializeField] private LayerMask whatIsGrapplable;
        public LayerMask WhatIsGrapplable => whatIsGrapplable;

        [SerializeField] private bool canHookThroughWalls;
        public bool CanHookThroughWalls => canHookThroughWalls;

        [SerializeField] private float maxGrappleDistance;
        public float MaxGrappleDistance => maxGrappleDistance;

        [Header("References")]
        [SerializeField] private GrappleGun leftGrapple;
        [SerializeField] private GrappleGun rightGrapple;
        [SerializeField] private CameraFollow cameraFollow;

        // Start is called before the first frame update
        void Start()
        {
            playerRB = GetComponent<Rigidbody>();

            leftGrapple.LoadVarsFromPlayerController(this);
            rightGrapple.LoadVarsFromPlayerController(this);
            cameraFollow.LoadVarsFromPlayerController(this);
        }

        // Update is called once per frame
        void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                leftGrapple.StartGrapple();
            }
            else
            {
                if (Input.GetMouseButtonUp(0))
                    leftGrapple.EndGrapple();
            }

            if (Input.GetMouseButtonDown(1))
            {
                rightGrapple.StartGrapple();
            }
            else
            {
                if (Input.GetMouseButtonUp(1))
                    rightGrapple.EndGrapple();
            }
        }

        private void FixedUpdate()
        {
            //Apply gravity
            playerRB.velocity -= new Vector3(0, gravityStrength, 0);
        }

        public void OnCollisionEnter(Collision col)
        {
            switch (col.gameObject.layer) {
                // landing on spikes: game over
                case 11:
                    EventManager.OnGameOver.Invoke();
                    break;
                // landing on trampoline: bounce up
                case 13:
                    // set velocity components based on trampoline attributes
                    Trampoline trampoline = col.gameObject.GetComponent<Trampoline>();
                    Vector3 newVelocity = new Vector3(0,0,0);
                    newVelocity.x = (trampoline.strengthX > 0 ? trampoline.strengthX : playerRB.velocity.x);
                    newVelocity.y = (trampoline.strengthY > 0 ? trampoline.strengthY : playerRB.velocity.y);
                    newVelocity.z = (trampoline.strengthZ > 0 ? trampoline.strengthZ : playerRB.velocity.z);
                    playerRB.velocity = newVelocity;
                    break;
            }
        }
    }
}