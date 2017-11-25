using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallBreak : MonoBehaviour {

    public List<string> Characters;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void BreakWall()
    {
        Destroy(gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        foreach (var item in Characters)
        {
            if(collision.gameObject.tag == item)
            {
                BreakWall();
            }
        }
    }
}
