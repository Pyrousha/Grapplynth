using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Grapplynth {
    public class EventManager : MonoBehaviour {
        public static UnityEvent OnStart;
        public static UnityEvent OnPause;
        public static UnityEvent OnResume;
        public static UnityEvent OnRestart;
        public static UnityEvent OnGameOver;

        public static UnityEvent OnTurnCorner;

        public static UnityEvent OnBarScore;

        public static UnityEvent OnScoreChanged;

        private void OnEnable() {
            OnStart = new UnityEvent();
            OnPause = new UnityEvent();
            OnResume = new UnityEvent();
            OnRestart = new UnityEvent();
            OnGameOver = new UnityEvent();

            OnTurnCorner = new UnityEvent();

            OnBarScore = new UnityEvent();

            OnScoreChanged = new UnityEvent();
        }
    }
}