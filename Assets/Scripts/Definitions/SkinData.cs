using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Grapplynth {
    [CreateAssetMenu(fileName = "NewSkinData", menuName = "Grapplynth/Skins/SkinData")]
    public class SkinData : ScriptableObject {
        public string ID {
            get { return m_id; }
        }
        public Material Material {
            get { return m_material; }
        }

        [SerializeField]
        private string m_id;
        [SerializeField]
        private Material m_material;
    }
}