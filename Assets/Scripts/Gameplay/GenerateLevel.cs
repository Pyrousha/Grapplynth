using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Grapplynth {
    public class GenerateLevel : MonoBehaviour {
        // object for spawn segment
        public Object spawnSegment;
        // track the ID of the current generator
        public int genID;


        // TODO: have this reset every time the scene is restarted
        // tracks the current generator ID to give to a new generator
        public static int currentGenID;

        // track the player object
        public GameObject player;
        // list of segment prefabs to use for the spawning process
        public List<GameObject> segmentPrefabs = new List<GameObject>();
        // x, y, z of this spawner
        int x;
        int y;
        int z;
        // random variable for generation
        private System.Random r;
        // number of segments this spawner has made
        int numSegments;
        private int maxSimultaneousTurns;
        // angle of level generator
        int rotation;
        // track how many turns to make
        int lastTurn = 0;
        int turnThreshold = 10;
        int numTurns = 0;

        // class used for segment initialization
        private class SegVals {
            public SegmentScript segmentScript;
            public GameObject segment;
        }

        // stores the initial hallway
        List<GameObject> initialHallway;
        bool isSpawningInitial;

        // store all the current hallways in list, deleting old ones as needed
        private Queue<List <GameObject>> hallwayQueue;
        int hallwayQueueSize;
        int maxHallwayQueueSize = 3;

        private void OnEnable() {
            EventManager.OnTurnCorner.AddListener(GenerateNextSegment);
        }

        private void OnDisable() {
            EventManager.OnTurnCorner.RemoveListener(GenerateNextSegment);
        }

        // Start is called before the first frame update
        void Start() {
            // Debug.Log("Generate level created at: " + x + "," + y + "," + z);
            // set seed
            GameDB gameDB = GameObject.Find("GameDB").GetComponent<GameDB>();
            gameDB.gameSeed = (gameDB.textSeed == 0 ? Random.Range(-2000000000, 2000000000) : gameDB.textSeed);
            int seed = gameDB.gameSeed;
            r = new System.Random(seed);
            // set generator ID
            this.genID = currentGenID;
            // want initial path to branch a bit, others shouldn't branch as much to prevent lag
            isSpawningInitial = (this.genID == 0 ? true : false);
            maxSimultaneousTurns = (this.genID == 0 ? 3 : 1);
            // initial hallway creation
            initialHallway = new List <GameObject>();
            // create spawn segment in case this is the first generator
            if (this.genID == 0) {
                // set hallway queue variables
                hallwayQueue = new Queue<List <GameObject>>();
                hallwayQueueSize = 0;
                // spawn segment initialization
                SegVals segvals = InstantiateSegment(-1);
                MoveToNewPos(segvals.segmentScript);
                initialHallway.Add(segvals.segment);
                rotation = 0;
                // place player on perch
                player = GameObject.Find("Player Hitbox");
                player.transform.position = new Vector3(0, 15, 0);
            }
            // increment for next generator ID
            currentGenID++;
        }

        // Update is called once per frame
        void Update() {
            if (isSpawningInitial) {
                // Spawn player and initial segments
                InitialSpawn();
            }
        }

        private void InitialSpawn() {
            if ((numTurns < maxSimultaneousTurns)) {
                // pick a random piece, excluding the turns if one has been placed recently
                //int startInd = (lastTurn > turnThreshold ? 0 : 4);
                int startRand = (lastTurn > turnThreshold ? 0 : 4);
                // Ranomly generate an int between 0 and 6 (inclusive)
                int randomInd = r.Next(startRand, segmentPrefabs.Count);

                // pick piece based on probability
                SegVals segvals = InstantiateSegment(randomInd);

                RotateForTurn(randomInd);
                if (randomInd < 4) {
                    lastTurn = 0;
                    numTurns++;
                }

                MoveToNewPos(segvals.segmentScript);

                initialHallway.Add(segvals.segment);

                lastTurn++;
                numSegments++;
            }
            else {
                isSpawningInitial = false;
                hallwayQueue.Enqueue(initialHallway);
            }
        }

        private void GenerateNextSegment() {
            // Generate a number between 2 and 9 for the number of segments
            int numNewPieces = r.Next(2, 10); // the number of segments in this hallway

            // Generate those segments
            List <GameObject> hallway = new List<GameObject>();

            for (int s = 0; s < numNewPieces; s++) {
                int randomInd = r.Next(4, segmentPrefabs.Count); // intermediate pieces are never turns

                SegVals segvals = InstantiateSegment(randomInd);
                MoveToNewPos(segvals.segmentScript);
                hallway.Add(segvals.segment);
                numSegments++;
            }
            
            // Generate a turn
            int turnInd = r.Next(0, 4); // left or right

            SegVals segvalsturn = InstantiateSegment(turnInd);

            RotateForTurn(turnInd);

            MoveToNewPos(segvalsturn.segmentScript);
            hallway.Add(segvalsturn.segment);
            numSegments++;

            hallwayQueue.Enqueue(hallway);
            hallwayQueueSize++;
            //Debug.Log("number of hallways generated: " + hallwayQueueSize);
            if (hallwayQueueSize > maxHallwayQueueSize) {
                //Debug.Log("deleting a hallway: " + hallwayQueueSize);
                List <GameObject> segmentsToDelete = hallwayQueue.Dequeue();
                DestroyHallway(segmentsToDelete);
                hallwayQueueSize--;
            }
        }

        private void RotateForTurn(int randomInd) {
            // deal with turns by resetting the angle and rotating in the correct direction
            // want to rotate 270 unity degrees if turning left, 90 if turning right
            switch (randomInd) {
                case 0:
                case 2:
                    rotation = rotation + 270;
                    break;
                case 1:
                case 3:
                    rotation = rotation + 90;
                    break;
            }
        }

        private void MoveToNewPos(SegmentScript segmentScript) {
            // set new x, y, z positions
            x = x + ((rotation % 180 == 90 ? segmentScript.length : 0) * (rotation % 360 == 90 ? 1 : -1));
            y = y + segmentScript.deltay;
            z = z + ((rotation % 180 == 0 ? segmentScript.length : 0) * (rotation % 360 == 0 ? 1 : -1));
            // move level generator to new position
            transform.position = new Vector3(x, y, z);
        }

        private SegVals InstantiateSegment(int segmentInd) {
            // pick piece based on probability
            SegVals segvals = new SegVals();
            // special case: spawn segment
            if (segmentInd == -1) {
                segvals.segment = Instantiate((GameObject)spawnSegment, new Vector3(x, y, z), Quaternion.Euler(new Vector3(0, rotation, 0)));
                segvals.segmentScript = segvals.segment.gameObject.GetComponent<SegmentScript>();
            }
            // spawn segment
            else {
                segvals.segment = Instantiate(segmentPrefabs[segmentInd], new Vector3(x, y, z), Quaternion.Euler(new Vector3(0, rotation, 0)));
                segvals.segmentScript = segvals.segment.gameObject.GetComponent<SegmentScript>();
                // fork segments
                if (segmentInd == 2 || segmentInd == 3) {
                    GameObject generateLevel = GameObject.Find("GenerateLevel");
                    Vector3 generateLevelPos = new Vector3(generateLevel.transform.position.x, generateLevel.transform.position.y, generateLevel.transform.position.z);
                    GameObject generateClone = Instantiate(generateLevel, generateLevelPos, Quaternion.Euler(new Vector3(0, rotation, 0)));
                    GenerateLevel generateLevelClone = generateClone.GetComponent<GenerateLevel>();
                    // TODO: move this to object constructor?
                    generateLevelClone.x = x;
                    generateLevelClone.y = y;
                    generateLevelClone.z = z;
                    generateLevelClone.hallwayQueue = new Queue<List <GameObject>>(hallwayQueue);
                    generateLevelClone.hallwayQueueSize = hallwayQueueSize;
                    generateLevelClone.rotation = rotation;
                    generateLevelClone.MoveToNewPos(segvals.segmentScript);
                }
            }
            return segvals;
        }

        private void DestroyHallway(List <GameObject> hallway) {
            foreach (GameObject segment in hallway){
                Destroy(segment);
            }
        }
    }
}