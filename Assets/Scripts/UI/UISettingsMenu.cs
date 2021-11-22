using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Grapplynth {
    public class UISettingsMenu : MonoBehaviour {

        #region Editor

        [SerializeField]
        private Button m_closeButton;
        [SerializeField]
        private InputField m_seedInput;

        #endregion

        #region Unity Callbacks

        private void Awake() {
            m_closeButton.onClick.AddListener(HandleClose);
        }

        #endregion

        #region ButtonHandlers

        private void HandleClose() {
            AudioManager.instance.PlayOneShot("click_default");
            GameDB gameDB = GameObject.Find("GameDB").GetComponent<GameDB>();
            gameDB.gameSeed = ( (m_seedInput.text == null || m_seedInput.text.Length == 0) ? 0 : (int.TryParse(m_seedInput.text, out gameDB.gameSeed) == false ? 0 : int.Parse(m_seedInput.text)) );
            this.gameObject.SetActive(false);
        }

        #endregion
    }
}

