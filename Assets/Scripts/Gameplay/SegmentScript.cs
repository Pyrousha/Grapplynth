﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SegmentScript : MonoBehaviour
{
    public int width;   // x
    public int height;  // y
    public int length;  // z
    // Start is called before the first frame update
    void Start()
    {
        
    }

    void OnTriggerEnter(Collider collider) {
        //GenerateLevel generatelevel = FindGameObjectOfType(GenerateLevel);
        //generateLevel.GetCurrentSegment(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
