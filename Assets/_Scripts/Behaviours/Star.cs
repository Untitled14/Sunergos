using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Star : MonoBehaviour {

    public int Score = 1;
    public float MovingSpeed = 10;
    public float YDistance = 0.25f;
    public List<string> PlayerTag;

    private Vector2 _originPosition;

    private float _t = 0;
    // Use this for initialization
	void Start () {
        _originPosition = transform.position;
        _t = Random.Range(0, 360);
	}
	
	// Update is called once per frame
	void Update () {
        UpdateStarPosition();

    }

    void UpdateStarPosition()
    {
        Vector2 diff = new Vector2(0, Mathf.Sin(_t*Mathf.Deg2Rad) * YDistance);
        _t += Time.deltaTime * MovingSpeed;
        transform.position = _originPosition + diff;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        foreach (var item in PlayerTag)
        {
            if(collision.tag == item)
            {
                if(item == "Circle")
                {
                    LevelController.Instance.AddCircles();
                }else if(item == "Square")
                {
                    LevelController.Instance.AddSquares();
                }
                LevelController.Instance.AddTime(10);
                Die();
                break;
            }
        }
    }
    void Die()
    {
        AudioController.Instance.PlaySound("taking object", 0.05f);
        ParticleController.Instance.SpawnParticles("star", transform.position);
        Destroy(gameObject);
    }
}
