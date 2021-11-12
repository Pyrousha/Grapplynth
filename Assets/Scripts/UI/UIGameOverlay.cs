using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace Grapplynth {
    public class UIGameOverlay : MonoBehaviour {

        #region Editor

        [SerializeField]
        private Button m_pauseButton;
        [SerializeField]
        private GameObject m_pauseMenu;
        [SerializeField]
        private GameObject m_gameOverMenu;

        #endregion

        #region Unity Callbacks

        private void Awake() {
            m_pauseButton.onClick.AddListener(HandlePause);
            EventManager.OnPause.AddListener(HandleOnPause);
            EventManager.OnResume.AddListener(HandleOnResume);
            EventManager.OnRestart.AddListener(HandleOnRestart);
            EventManager.OnGameOver.AddListener(HandleOnGameOver);
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
            AudioManager.instance.PlayOneShot("click_default");

            m_pauseMenu.SetActive(true);
            m_pauseButton.interactable = false;
        }

        private void HandleOnResume() {
            AudioManager.instance.PlayOneShot("click_default");

            m_pauseMenu.SetActive(false);
            m_pauseButton.interactable = true;
        }

        private void HandleOnRestart() {
            AudioManager.instance.PlayOneShot("click_play");

            m_pauseMenu.SetActive(false);
            m_pauseButton.interactable = true;

            m_gameOverMenu.SetActive(false);

            SceneManager.LoadScene(1);
        }

        private void HandleOnGameOver() {
            m_gameOverMenu.SetActive(true);
            m_pauseButton.interactable = false;
        }

        #endregion
    }
}