using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrappleGun : MonoBehaviour
{
    private LineRenderer lr;
    private Vector3 grapplePoint;
    [SerializeField] private LayerMask whatIsGrapplable;
    [SerializeField] private Transform shootPoint, cameraTransform, playerTransform;
    [SerializeField] private float maxDistance = 100f;
    private SpringJoint joint;

    // Start is called before the first frame update
    void Start()
    {
        lr = GetComponent<LineRenderer>();
    }

    private void LateUpdate()
    {
        if (joint != null)
            DrawRope();
    }

    public void StartGrapple()
    {
        RaycastHit hit;

        Vector3 pos = Input.mousePosition;
        pos.z = 0;
        Ray ray = Camera.main.ScreenPointToRay(pos);

        if (Physics.Raycast(ray, out hit, maxDistance, whatIsGrapplable))
        {
            grapplePoint = hit.point;

            joint = playerTransform.gameObject.AddComponent<SpringJoint>();
            joint.autoConfigureConnectedAnchor = false;
            joint.connectedAnchor = grapplePoint;

            float distanceFromPoint = Vector3.Distance(playerTransform.position, grapplePoint);

            joint.maxDistance = 0; //distanceFromPoint * 0.5f;
            joint.minDistance = 0;

            joint.spring = 15;
            joint.damper = 7f;
            joint.massScale = 4.5f;

            lr.positionCount = 2;
        }
    }

    private void DrawRope()
    {
        lr.SetPosition(0, shootPoint.position);
        lr.SetPosition(1, grapplePoint);
    }

    public void EndGrapple()
    {
        lr.positionCount = 0;
        Destroy(joint);
    }

    public bool IsGrappling => joint != null;

    public Vector3 GetGrapplePoint => grapplePoint;
}
