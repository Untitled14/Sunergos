using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button : Trigger {
    private List<Player> _players;
    void Start()
    {
        _players = new List<Player>();
    }
	// Update is called once per frame
	void Update () {
        foreach (var player in _players)
        {
            if(player.ActionKey)
            {
                Toggle();
            }
        }
	}
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Circle" || collision.tag == "Square")
        {
            _players.Add(collision.GetComponent<Player>());
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Circle" || collision.tag == "Square")
        {
            _players.Remove(collision.GetComponent<Player>());
        }
    }
}
