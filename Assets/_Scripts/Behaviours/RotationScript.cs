using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationScript : MonoBehaviour {

    public float RotationSpeed = 10;
    private Rigidbody2D _rb;
	// Use this for initialization
	void Start () {
        _rb = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update () {
		if(_rb == null)
        {
            transform.Rotate(new Vector3(0,0,RotationSpeed*Time.deltaTime));
        }
        else
        {
            _rb.angularVelocity = RotationSpeed;
        }
	}
}
