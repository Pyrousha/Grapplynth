using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateLevel : MonoBehaviour
{
    public Object spawnSegment;
    public GameObject player;
    public List <GameObject> levelSegments = new List <GameObject>();
    int x = 0;
    int y = 0;
    int z = 10;
    int numPieces = 0;
    [SerializeField] private int maxPieces;
    // player moves towards positive z when the game begins
    int rotation = 0;
    // track how many turns to make
    int lastTurn = 0;
    int turnThreshold = 10;

    // Start is called before the first frame update
    void Start()
    {
        Instantiate(spawnSegment, new Vector3(0,0,0), Quaternion.identity);
        // place player on perch
        player = GameObject.Find("Player Hitbox");
        player.transform.position = new Vector3(0,15,0);
    }

    public void GetCurrentSegment(GameObject segment) {

    }

    // Update is called once per frame
    void Update()
    {
        if (numPieces < maxPieces) {
            // pick a random piece, excluding the turns if one has been placed recently
            int startInd = (lastTurn > turnThreshold ? 0 : 2);
            int randomInd = Random.Range(startInd, levelSegments.Count);
            Instantiate(levelSegments[randomInd], new Vector3(x,y,z), Quaternion.Euler(new Vector3(0,rotation,0)));

            // deal with turns by resetting the angle and rotating in the correct direction
            if (randomInd < 2) {
                lastTurn = 0;
                // want to rotate 90 degrees if turning left, -90 (=270) if turning right
                rotation = (rotation + (randomInd == 0 ? 270 : 90)) % 360;
                Debug.Log("hello, coordinates are: " + x + " " + y + " " + z);
            }

            // set new x, y, z positions
            x = x + ((rotation % 180 == 90 ? 10 : 0) * (rotation % 360 == 90 ? 1 : -1));
            //y = 0;
            z = z + ((rotation % 180 == 0 ? 10 : 0) * (rotation % 360 == 0 ? 1 : -1));

            // move level generator to new position
            transform.position = new Vector3(x,y,z);

            lastTurn++;
            numPieces++;
        }
    }
}
