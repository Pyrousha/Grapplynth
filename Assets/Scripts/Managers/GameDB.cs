using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Grapplynth {
    public class GameDB : MonoBehaviour {
        [SerializeField]
        private AudioData[] m_audioData;
        [SerializeField]
        private SkinData[] m_skinData;

        private Dictionary<string, AudioData> m_audioMap;
        private Dictionary<string, SkinData> m_skinMap;

        public int textSeed;
        public int gameSeed;
        public int currentGenID;

        public static GameDB instance;

        private void Awake() {
            gameSeed = textSeed;
            if (instance == null) {
                instance = this;
            }
            else if (instance != this) {
                Destroy(this.gameObject);
            }
        }

        public static AudioData GetAudioData(string id) {
            // initialize the map if it does not exist
            if (instance.m_audioMap == null) {
                instance.m_audioMap = new Dictionary<string, AudioData>();
                foreach (AudioData data in instance.m_audioData) {
                    instance.m_audioMap.Add(data.ID, data);
                }
            }
            if (instance.m_audioMap.ContainsKey(id)) {
                return instance.m_audioMap[id];
            }
            else {
                throw new KeyNotFoundException(string.Format("No Audio " +
                    "with id `{0}' is in the database", id
                ));
            }
        }

        public static SkinData GetSkinData(string id) {
            // initialize the map if it does not exist
            if (instance.m_skinMap == null) {
                instance.m_skinMap = new Dictionary<string, SkinData>();
                foreach (SkinData skin in instance.m_skinData) {
                    instance.m_skinMap.Add(skin.ID, skin);
                }
            }
            if (instance.m_skinMap.ContainsKey(id)) {
                return instance.m_skinMap[id];
            }
            else {
                throw new KeyNotFoundException(string.Format("No Skin " +
                    "with id `{0}' is in the database", id
                ));
            }
        }
    }
}