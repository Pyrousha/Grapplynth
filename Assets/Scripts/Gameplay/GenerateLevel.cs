using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Grapplynth {
    public class GenerateLevel : MonoBehaviour {
        public Object spawnSegment;
        public GameObject player;
        public List<GameObject> levelSegments = new List<GameObject>();
        int x = 0;
        int y = 0;
        int z = 10;
        int numPieces = 0;
        [SerializeField] private int maxPieces;
        [SerializeField] private int maxSimultaneousTurns; // e.g. 3 turns = 3 hallways (not including forks)
        // player moves towards positive z when the game begins
        int rotation = 0;
        // track how many turns to make
        int lastTurn = 0;
        int turnThreshold = 10;
        int numTurns = 0;

        bool isSpawningInitial = true;

        private void OnEnable() {
            EventManager.OnTurnCorner.AddListener(GenerateNextSegment);
        }

        private void OnDisable() {
            EventManager.OnTurnCorner.RemoveListener(GenerateNextSegment);
        }

        // Start is called before the first frame update
        void Start() {
            Instantiate(spawnSegment, new Vector3(0, 0, 0), Quaternion.identity);
            // place player on perch
            player = GameObject.Find("Player Hitbox");
            player.transform.position = new Vector3(0, 15, 0);
        }

        public void GetCurrentSegment(GameObject segment) {

        }

        // Update is called once per frame
        void Update() {
            if (isSpawningInitial) {
                // Spawn player and initial segments
                InitialSpawn();
            }
        }

        private void InitialSpawn() {
            if ((numPieces < maxPieces) && (numTurns < maxSimultaneousTurns)) {
                // pick a random piece, excluding the turns if one has been placed recently
                //int startInd = (lastTurn > turnThreshold ? 0 : 2);
                //int randomInd = Random.Range(startInd, levelSegments.Count);
                int startRand = (lastTurn > turnThreshold ? 0 : 1);
                // Ranomly generate an int between 0 and 6 (inclusive)
                int randomInd = Random.Range(startRand, 7);

                // pick piece based on probability
                GameObject segment = Instantiate(levelSegments[randomInd], new Vector3(x, y, z), Quaternion.Euler(new Vector3(0, rotation, 0)));
                SegmentScript segmentScript = segment.gameObject.GetComponent<SegmentScript>();

                RotateForTurn(randomInd);
                if (randomInd < 2) {
                    lastTurn = 0;
                    numTurns++;
                }

                MoveToNewPos(segmentScript);
                lastTurn++;
                numPieces++;
            }
            else {
                isSpawningInitial = false;
            }
        }

        private void GenerateNextSegment() {
            // Generate a number between 2 and 9 for the number of segments
            int numNewPieces = Random.Range(2, 10); // the number of segments in this hallway

            // Generate those segments

            for (int s = 0; s < numNewPieces; s++) {
                int randomInd = Random.Range(2, 7); // intermediate pieces are never turns

                GameObject segment = Instantiate(levelSegments[randomInd], new Vector3(x, y, z), Quaternion.Euler(new Vector3(0, rotation, 0)));
                SegmentScript segmentScript = segment.gameObject.GetComponent<SegmentScript>();

                MoveToNewPos(segmentScript);
            }
            
            // Generate a turn
            int turnInd = Random.Range(0, 2); // left or right
            GameObject turnSegment = Instantiate(levelSegments[turnInd], new Vector3(x, y, z), Quaternion.Euler(new Vector3(0, rotation, 0)));
            SegmentScript turnSegmentScript = turnSegment.gameObject.GetComponent<SegmentScript>();

            RotateForTurn(turnInd);

            MoveToNewPos(turnSegmentScript);
        }

        private void RotateForTurn(int randomInd) {
            // deal with turns by resetting the angle and rotating in the correct direction
            if (randomInd < 2) {
                // want to rotate 90 degrees if turning left, -90 (=270) if turning right
                rotation = (rotation + (randomInd == 0 ? 270 : 90)) % 360;
                // Debug.Log("coordinates are: " + x + " " + y + " " + z);
            }
        }

        private void MoveToNewPos(SegmentScript segmentScript) {
            // set new x, y, z positions
            x = x + ((rotation % 180 == 90 ? segmentScript.length : 0) * (rotation % 360 == 90 ? 1 : -1));
            //y = 0;
            z = z + ((rotation % 180 == 0 ? segmentScript.length : 0) * (rotation % 360 == 0 ? 1 : -1));

            // move level generator to new position
            transform.position = new Vector3(x, y, z);
        }
    }
}