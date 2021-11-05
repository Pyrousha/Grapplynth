﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Grapplynth {
    public class UIGameOverMenu : MonoBehaviour {

        #region Editor

        [SerializeField]
        private Button m_mainMenuButton;
        [SerializeField]
        private Button m_playAgainButton;

        #endregion

        #region Unity Callbacks

        private void Awake() {
            m_mainMenuButton.onClick.AddListener(HandleMainMenu);
            m_playAgainButton.onClick.AddListener(HandlePlayAgain);
        }

        #endregion

        #region ButtonHandlers

        private void HandleMainMenu() {
            SceneManager.LoadScene("MainMenu");
        }

        private void HandlePlayAgain() {
            EventManager.OnRestart.Invoke();
        }

        #endregion
    }
}