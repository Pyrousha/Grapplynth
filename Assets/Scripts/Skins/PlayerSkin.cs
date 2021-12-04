using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Grapplynth {
    public class PlayerSkin : MonoBehaviour {
        [SerializeField]
        private MeshRenderer m_meshRenderer;

        private SkinData m_skinData;

        private void OnEnable() {
            SetSkin();
        }

        private void SetSkin() {
            m_skinData = GameManager.instance.GetCurrSkin();

            m_meshRenderer.material = m_skinData.Material;
        }
    }
}
