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

        #endregion

        #region Unity Callbacks

        private void Awake() {
            m_playGameButton.onClick.AddListener(HandlePlayGame);
            m_settingsButton.onClick.AddListener(HandleSettings);
        }

        #endregion

        #region ButtonHandlers

        private void HandlePlayGame() {
            SceneManager.LoadScene("Labyrinth");
        }
        private void HandleSettings() {

        }

        #endregion
    }
}