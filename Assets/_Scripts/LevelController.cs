using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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

    private TriggerConnection _exit;
    private bool _levelOver;

    [HideInInspector]
    public int LevelNumber;

    private void Awake()
    {
        Instance = this;
    }
    // Use this for initialization
    void Start () {
        string sceneName = SceneManager.GetActiveScene().name;
        LevelNumber = int.Parse(sceneName.Split(' ')[1]);

        Players.Add(Player_1.GetComponent<Player>());
        Players.Add(Player_2.GetComponent<Player>());

        _health = MaxHealth;

        _exit = GetComponent<TriggerConnection>();
        GameMenuController.Instance.UpdateHealthPacks();
    }
	
	// Update is called once per frame
	void Update () {
        UpdateTime();

        if(_exit != null)
        {
            if(_exit.Triggered && !_levelOver)
            {
                LevelWon();
            }
        }
	}
    void CheckIfWon()
    {
        return;
        if(Circles >= MaxCircles && Squares >= MaxSquares && !_levelOver)
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
        _levelOver = true;
        GameMenuController.Instance.OpenGameOverPanel();
    }
    public void LevelWon()
    {
        Alive = false;
        _levelOver = true;
        Score = (int)LevelTime - (int)TimePassed + (Circles + Squares) * 10;
        GameMenuController.Instance.OpenGameWonPanel();
    }
}
