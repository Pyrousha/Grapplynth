using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Grapplnyth {
    public class UIMainMenu : MonoBehaviour {

        #region Editor

        [SerializeField]
        private Button m_playGameButton;
        [SerializeField]
        private Button m_settingsButton;
        [SerializeField]
        private Button m_quitButton;
        [SerializeField]
        private GameObject m_settingsMenu;

        #endregion

        #region Unity Callbacks

        private void Awake() {
            m_playGameButton.onClick.AddListener(HandlePlayGame);
            m_settingsButton.onClick.AddListener(HandleSettings);
            m_quitButton.onClick.AddListener(HandleQuit);
        }

        #endregion

        #region ButtonHandlers

        private void HandlePlayGame() {
            SceneManager.LoadScene("Labyrinth");
        }
        private void HandleSettings() {
            m_settingsMenu.SetActive(true);
        }
        private void HandleQuit() {
            Application.Quit();

#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#endif
        }

        #endregion
    }
}