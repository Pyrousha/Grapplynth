using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateLevel : MonoBehaviour
{
    public Object spawnSegment;
    public GameObject player;
    public List <GameObject> levelSegments = new List <GameObject>();
    int lastTurn = 5;
    // Start is called before the first frame update
    void Start()
    {
        Instantiate(spawnSegment, new Vector3(0,0,0), Quaternion.identity);
        // place player on perch
        player = GameObject.Find("Player Hitbox");
        player.transform.position = new Vector3(0,15,0);
        // load all the segments and store them in the levelSegments list
        Object[] segments = Resources.LoadAll("Assets/Prefab/LevelSegments", typeof(GameObject));
        foreach (var seg in segments) {
            levelSegments.Add(seg);
        }
    }

    // Update is called once per frame
    void Update()
    {
        int z = 10;
        if (z < 100) {
            int randomInd = Random.Range(0, levelSegments.Count - 1);
            Instantiate(levelSegments[randomInd], new Vector3(0,0,z), Quaternion.identity);
            z = z + 10;
        }
    }
}
