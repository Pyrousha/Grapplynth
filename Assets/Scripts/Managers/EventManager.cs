﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Grapplynth {
    public class EventManager : MonoBehaviour {
        public static UnityEvent OnPause;
        public static UnityEvent OnResume;
        public static UnityEvent OnRestart;
        public static UnityEvent OnGameOver;

        private void Awake() {
            OnPause = new UnityEvent();
            OnResume = new UnityEvent();
            OnRestart = new UnityEvent();
            OnGameOver = new UnityEvent();
        }
    }
}