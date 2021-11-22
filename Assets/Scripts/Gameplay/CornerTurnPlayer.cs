using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Grapplynth {
    public class CornerTurnPlayer : MonoBehaviour {
        private bool rotatePlayer;
        private Transform playerTransform;
        private float startingRotation;

        float dAngle;

        private bool visited = false;

        [System.Serializable]
        private enum CornerTypeEnum {
            left,
            right,
            up,
            down
        }
        [SerializeField] private CornerTypeEnum cornerType;

        // Start is called before the first frame update
        void Start() {
            rotatePlayer = false;
            dAngle = 0;
        }

        private void OnTriggerEnter(Collider other) {
            rotatePlayer = true;
            startingRotation = transform.parent.eulerAngles.y;
            Debug.Log("Start rotation: " + startingRotation);
            playerTransform = other.transform;

            if (visited == false) {
                EventManager.OnTurnCorner.Invoke();
                visited = true;
            }
        }

        // Update is called once per frame
        void Update() {
            if (rotatePlayer) {
                switch (cornerType) {
                    case CornerTypeEnum.left: {
                            dAngle = (Mathf.Atan2(playerTransform.position.z - transform.position.z, playerTransform.position.x - transform.position.x) * Mathf.Rad2Deg * -1);
                            float offset = (dAngle - startingRotation) % 360f;
                            Debug.Log("Startingrotation left: " + startingRotation);
                            Debug.Log("dAngle left: " + dAngle);
                            Debug.Log("Offset left: " + offset);
                            if ((offset > 0) || (offset < -90))
                                return;
                            break;
                        }
                    case CornerTypeEnum.right: {
                            dAngle = (Mathf.Atan2(playerTransform.position.x - transform.position.x, playerTransform.position.z - transform.position.z) * Mathf.Rad2Deg + 90);
                            float offset = ((dAngle - startingRotation) + 360f) % 360f;
                            Debug.Log("Offset right: " + offset);
                            if ((offset < 0) || (offset > 90))
                                return;
                            break;
                        }
                }

                //Mathf.Clamp(dAngle, startingRotation, startingRotation + 90);
                //Debug.Log("DAngle: "+dAngle);
                //Debug.Log("DAngle - Starting: " + (dAngle - startingRotation)%360f);
                //Debug.Log(Mathf.Abs(Mathf.DeltaAngle(dAngle, cornerType == CornerTypeEnum.left ? startingRotation - 45 : startingRotation + 45)));
                //if (Mathf.Abs(Mathf.DeltaAngle(dAngle, cornerType == CornerTypeEnum.left ? startingRotation - 45 : startingRotation + 45)) <= 45)
                playerTransform.rotation = Quaternion.Euler(new Vector3(0, dAngle, 0));
            }
        }

        private void OnTriggerExit(Collider other) {
            //Rotate player to nearest 90 degrees
            //Debug.Log("currRotation: " + (startingRotation + dAngle) + ", roundedRotation: "+ Mathf.Round((startingRotation + dAngle) / 90f) * 90);

            float offset = Mathf.Abs((dAngle - startingRotation) % 360f); //between 0 and 90
            if (offset < 45)
                playerTransform.rotation = Quaternion.Euler(new Vector3(0, startingRotation, 0));
            else
                playerTransform.rotation = Quaternion.Euler(new Vector3(0, cornerType == CornerTypeEnum.left ? startingRotation - 90 : startingRotation + 90, 0));

            rotatePlayer = false;
            playerTransform = null;
        }
    }
}