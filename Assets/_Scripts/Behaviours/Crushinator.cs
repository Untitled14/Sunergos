using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crushinator : MonoBehaviour {

    private Rigidbody2D _rb;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnCollisionStay2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Circle" || collision.gameObject.tag == "Square")
        {
            foreach (var contact in collision.contacts)
            {
                Debug.DrawLine(transform.position, contact.point, Color.red);
            }
        }
    }
}
