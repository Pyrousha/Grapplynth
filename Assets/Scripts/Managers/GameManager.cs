using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Grapplynth {
    public class GameManager : MonoBehaviour {
        public static GameManager instance;

        #region Unity Callbacks

        private void Awake() {
            if (instance == null) {
                instance = this;
                DontDestroyOnLoad(this.gameObject);
            }
            else if (this != instance) {
                Destroy(this.gameObject);
            }
        }

        #endregion
    }
}