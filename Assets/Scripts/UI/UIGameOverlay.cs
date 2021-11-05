using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Grapplynth {
    public class UIGameOverlay : MonoBehaviour {

        #region Editor

        [SerializeField]
        private Button m_pauseButton;
        [SerializeField]
        private GameObject m_pauseMenu;

        #endregion

        #region Unity Callbacks

        private void Awake() {
            m_pauseButton.onClick.AddListener(HandlePause);
            EventManager.OnPause.AddListener(HandleOnPause);
            EventManager.OnResume.AddListener(HandleOnResume);
            EventManager.OnRestart.AddListener(HandleOnRestart);
        }

        private void OnDestroy() {
            m_pauseButton.onClick.RemoveListener(HandlePause);
        }

        #endregion

        #region Button Handlers

        private void HandlePause() {
            EventManager.OnPause.Invoke();
        }

        #endregion

        #region Event Handlers

        private void HandleOnPause() {
            m_pauseMenu.SetActive(true);
            m_pauseButton.interactable = false;
        }

        private void HandleOnResume() {
            m_pauseMenu.SetActive(false);
            m_pauseButton.interactable = true;
        }

        private void HandleOnRestart() {
            m_pauseMenu.SetActive(false);
            m_pauseButton.interactable = true;
        }

        #endregion
    }
}