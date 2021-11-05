using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody playerRB;
    [SerializeField] private float gravityStrength;
    [SerializeField] GrappleGun leftGrapple, rightGrapple;

    // Start is called before the first frame update
    void Start()
    {
        playerRB = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0))
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
}
