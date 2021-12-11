using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Grapplynth {
    public class RewardUnlockSkin : MonoBehaviour {
        [SerializeField]
        private Button m_giftButton;
        [SerializeField]
        private GameObject m_rewardMenu;

        private void OnEnable() {
            m_giftButton.onClick.AddListener(HandleUnlockSkin);
        }

        private void OnDisable() {
            m_giftButton.onClick.RemoveListener(HandleUnlockSkin);
        }

        private void HandleUnlockSkin() {
            GameDB.instance.RandomlyUnlockNewSkin();
            m_rewardMenu.SetActive(false);
        }
    }
}
