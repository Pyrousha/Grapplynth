﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Grapplynth {
    public class UISettingsMenu : MonoBehaviour {

        #region Editor

        [SerializeField]
        private Button m_closeButton;

        #endregion

        #region Unity Callbacks

        private void Awake() {
            m_closeButton.onClick.AddListener(HandleClose);
        }

        #endregion

        #region ButtonHandlers

        private void HandleClose() {
            this.gameObject.SetActive(false);
        }

        #endregion
    }
}
