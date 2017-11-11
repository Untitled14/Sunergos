using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelController : MonoBehaviour {
    public static LevelController Instance;

    [HideInInspector]
    public bool Alive = true;

    public GameObject Player_1;
    public GameObject Player_2;

    public int MaxSquares = 1;
    public int MaxCircles = 1;
    [HideInInspector]
    public int Score = 0;
    [HideInInspector]
    public int Circles = 0;
    [HideInInspector]
    public int Squares = 0;
    public float LevelTime = 300;
    [HideInInspector]
    public float TimePassed = 0;

    public int MaxHealth = 3;

    [HideInInspector]
    private int _health;
    public int Health { get { return _health; } }

    [HideInInspector]
    public List<Player> Players;

    private void Awake()
    {
        Instance = this;
    }
    // Use this for initialization
    void Start () {
        Players.Add(Player_1.GetComponent<Player>());
        Players.Add(Player_2.GetComponent<Player>());

        _health = MaxHealth;
        GameMenuController.Instance.UpdateHealthPacks();
    }
	
	// Update is called once per frame
	void Update () {
        UpdateTime();
	}
    void CheckIfWon()
    {
        if(Circles >= MaxCircles && Squares >= MaxSquares)
        {
            GameOver();
        }
    }
    void UpdateTime()
    {
        if(Alive)
            TimePassed += Time.deltaTime;
        if (TimePassed >= LevelTime)
            GameOver();
    }
    public void TakeDamage()
    {
        //_health--; 
        //if (_health <= 0)
        //{
        //    _health = 0;
        //    GameOver();
        //}
        TimePassed += 15;
        GameMenuController.Instance.UpdateHealthPacks();
    }
    public void AddScore(int score)
    {
        Score += score;
    }
    public void AddCircles()
    {
        Circles++;
        CheckIfWon();
    }
    public void AddSquares()
    {
        Squares++;
        CheckIfWon();
    }
    public void GameOver()
    {
        Alive = false;
        GameMenuController.Instance.OpenGameOverPanel();
    }
    public void LevelWon()
    {

    }
}
