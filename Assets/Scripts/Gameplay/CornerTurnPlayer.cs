using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CornerTurnPlayer : MonoBehaviour
{
    private bool rotatePlayer;
    private Transform playerTransform;
    private float startingRotation;

    float dAngle;

    [System.Serializable] private enum CornerTypeEnum
    {
        left,
        right
    }
    [SerializeField] private CornerTypeEnum cornerType;

    // Start is called before the first frame update
    void Start()
    {
        rotatePlayer = false;
        dAngle = 0;
    }

    private void OnTriggerEnter(Collider other)
    {
        rotatePlayer = true;
        startingRotation = transform.parent.rotation.y;
        Debug.Log("Start rotation: "+startingRotation);
        playerTransform = other.transform;
    }

    // Update is called once per frame
    void Update()
    {
        if(rotatePlayer)
        {
            switch(cornerType)
            {
                case CornerTypeEnum.left:
                    {
                        dAngle = (Mathf.Atan2(playerTransform.position.z - transform.position.z, playerTransform.position.x - transform.position.x) * Mathf.Rad2Deg * -1);
                        break;
                    }
                case CornerTypeEnum.right:
                    {
                        dAngle = (Mathf.Atan2(playerTransform.position.x - transform.position.x, playerTransform.position.z - transform.position.z) * Mathf.Rad2Deg + 90);
                        break;
                    }
            }

            //Mathf.Clamp(dAngle, startingRotation - 90, startingRotation + 90);
            Debug.Log(dAngle);
            playerTransform.rotation = Quaternion.Euler(new Vector3(0, startingRotation + dAngle, 0));
        }
    }

    private void OnTriggerExit(Collider other)
    {
        rotatePlayer = false;
        playerTransform = null;
    }
}
