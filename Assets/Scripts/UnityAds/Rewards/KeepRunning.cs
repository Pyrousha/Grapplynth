﻿using UnityEngine;
using UnityEngine.UI;

namespace Grapplynth {
    public class KeepRunning : MonoBehaviour {
        [SerializeField]
        private Button m_keepRunningButton;
        [SerializeField]
        private GameObject m_AdRewardMenu;

        private void OnEnable() {
            m_keepRunningButton.onClick.AddListener(HandleKeepRunning);
        }

        private void OnDisable() {
            m_keepRunningButton.onClick.RemoveListener(HandleKeepRunning);
        }

        private void HandleKeepRunning() {
            EventManager.OnNewLife.Invoke();
            m_AdRewardMenu.SetActive(false);
            GameManager.instance.IncrementKeepRunnings();
        }
    }
}