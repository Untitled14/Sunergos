using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallBreak : MonoBehaviour {
    public ParticleSystem WallParticles;
    public int WallParticleNumber = 30;
    public List<string> Characters;

    private Collider2D _collider;
    private SpriteRenderer _sr;
   
	// Use this for initialization
	void Start () {
        _collider = GetComponent<Collider2D>();
        _sr = GetComponent<SpriteRenderer>();
        WallParticles.Stop();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    private void BreakWall()
    {
        AudioController.Instance.PlaySound("break wall", 0);
        _collider.enabled = false;
        _sr.enabled = false;
        WallParticles.Emit(WallParticleNumber);
        //Destroy(gameObject);
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
