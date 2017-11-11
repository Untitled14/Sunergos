using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressureButtoBehaviuor : MonoBehaviour {
    public float ResetSpeed = 1;
    public float ResetForce = 10;
    private Rigidbody2D _rb;
    // Use this for initialization
    void Start () {
        _rb = GetComponent<Rigidbody2D>();
    }
	
	// Update is called once per frame
	void Update () {
        UpdatePressureButton();

    }
    void UpdatePressureButton()
    {
        Vector2 velocity = _rb.velocity;
        if (velocity.y < ResetSpeed)
            _rb.AddForce(new Vector2(0, ResetForce));
        if (velocity.y < -ResetSpeed)
            velocity.y = -ResetSpeed;
        if (transform.localPosition.y > 0.3f)
        {
            velocity = Vector2.zero;
            transform.localPosition = new Vector2(0, 0.3f);
        }

        if (transform.localPosition.y < 0.1f)
        {
            transform.localPosition = new Vector2(0, 0.1f);
            velocity = Vector2.zero;
        }
        _rb.velocity = velocity;
    }
}
