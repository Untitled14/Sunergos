﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Star : MonoBehaviour {

    public int Score = 1;

    public List<string> PlayerTag;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        foreach (var item in PlayerTag)
        {
            if(collision.tag == item)
            {
                LevelController.Instance.AddScore(Score);
                LevelController.Instance.AddStar();
                Destroy(gameObject);
                break;
            }
        }
    }
}
