using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Grapplynth {
    public class Chaser : MonoBehaviour {
        public float x, y, z;
        public float speed = 0.046875f;
        public int rotation;
        public int chaserID;
        private static int numChasers;
        public bool killed;

        private void OnEnable() {
            EventManager.OnRestart.AddListener(ResetNumChasers);
        }

        private void OnDisable() {
            EventManager.OnRestart.RemoveListener(ResetNumChasers);
        }

        private void OnDestroy() {
            if (killed == false) {
                ResetNumChasers();
            }
        }

        private void ResetNumChasers() {
            numChasers = 0;
        }

        // Start is called before the first frame update
        void Awake() {
            x = transform.position.x;
            y = transform.position.y;
            z = transform.position.z;
            gameObject.transform.eulerAngles = new Vector3(0, rotation, 0);
            chaserID = numChasers;
            if (chaserID == 0) {
                numChasers = 0;
            }
            numChasers++;
        }

        // Update is called once per frame
        void Update() {
            if (GameManager.instance.GameIsPaused) {
                return;
            }
            x += ((rotation + 360) % 180 == 90 ? speed : 0.0f);
            y += 0.0f;
            z += ((rotation + 360) % 180 == 0 ? speed : 0.0f);
            transform.position = new Vector3(x, y, z);
        }
    }
}