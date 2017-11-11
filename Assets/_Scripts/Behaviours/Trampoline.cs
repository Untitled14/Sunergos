using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trampoline : MonoBehaviour {
    public float JumpSpeed = 10;
    public List<string> PlayerTag;
    private Rigidbody2D _rb;
	// Use this for initialization
	void Start () {
        _rb = GetComponent<Rigidbody2D>();
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        foreach (var item in PlayerTag)
        {
            if(collision.tag == item)
            {
                Rigidbody2D rigidbody = collision.GetComponent<Rigidbody2D>();
                Vector2 collisionVelocity = rigidbody.velocity;
                collisionVelocity.y = JumpSpeed;
                rigidbody.velocity = collisionVelocity;

                Player player = collision.GetComponent<Player>();
                player.ResetDoubleJump();
            }
        }
    }
}
