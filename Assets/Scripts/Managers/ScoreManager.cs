﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Grapplynth {
    public class ScoreManager : MonoBehaviour {

        private int m_currScore = 0;
        private int m_highScore = -1;
        private bool m_wasHighScore = false;

        public static ScoreManager instance;

        public int CurrScore {
            get { return m_currScore; }
        }

        public int HighScore {
            get { return m_highScore; }
        }

        public bool WasHighScore {
            get { return m_wasHighScore; }
        }

        private void Awake() {
            if (instance == null) {
                instance = this;
                DontDestroyOnLoad(this.gameObject);
            }
            else if (this != instance) {
                Destroy(this.gameObject);
                return;
            }

            EventManager.OnStart.AddListener(ResetScore);
            EventManager.OnRestart.AddListener(ResetScore);
            EventManager.OnTurnCorner.AddListener(TurnScore);
            EventManager.OnBarScore.AddListener(BarScore);
            EventManager.OnGameOver.AddListener(SaveScore);
        }

        private void OnDisable() {
            EventManager.OnStart.RemoveListener(ResetScore);
            EventManager.OnRestart.RemoveListener(ResetScore);
            EventManager.OnTurnCorner.RemoveListener(TurnScore);
            EventManager.OnBarScore.RemoveListener(BarScore);
            EventManager.OnGameOver.RemoveListener(SaveScore);
        }

        private void TurnScore() {
            m_currScore += 100;
            Debug.Log("Update score: " + m_currScore);
            EventManager.OnScoreChanged.Invoke();
            // AudioManager.instance.PlayOneShot("turn_score");
        }

        private void BarScore() {
            m_currScore += 100;
            Debug.Log("Update score: " + m_currScore);
            EventManager.OnScoreChanged.Invoke();
            // AudioManager.instance.PlayOneShot("turn_score");
        }

        private void ResetScore() {
            m_currScore = 0;
            m_wasHighScore = false;

            EventManager.OnScoreChanged.Invoke();
        }

        private void SaveScore() {
            if (m_currScore > m_highScore) {
                m_highScore = m_currScore;
                m_wasHighScore = true;
            }
        }
    }

}
