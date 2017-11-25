using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelController : MonoBehaviour {
    public static LevelController Instance;

    [HideInInspector]
    public bool Alive = true;
    public bool Paused;
    public GameObject Player_1;
    public GameObject Player_2;

    [Space(10)]
    public GameObject SquareExit;
    public GameObject CircleExit;

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

    [HideInInspector]
    public float ExtraTime = 0;

    [HideInInspector]
    public float TimeLeft = 0;
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

        if(Vector2.Distance(Player_2.transform.position, SquareExit.transform.position) < 0.2f
            && Vector2.Distance(Player_1.transform.position, CircleExit.transform.position) < 0.2f
            && !_levelOver)
        {
            LevelWon();
        }
        if(Vector2.Distance(Player_2.transform.position, SquareExit.transform.position) < 0.8f)
        {
            Player_2.transform.position = Vector2.MoveTowards(Player_2.transform.position, SquareExit.transform.position, 1 * Time.deltaTime);
        }
        if (Vector2.Distance(Player_1.transform.position, CircleExit.transform.position) < 0.8f)
        {
            Player_1.transform.position = Vector2.MoveTowards(Player_1.transform.position, CircleExit.transform.position, 1 * Time.deltaTime);
        }
        if (_levelOver)
        {
            Player_2.transform.position = SquareExit.transform.position;
            Player_1.transform.position = CircleExit.transform.position;
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
        TimeLeft = LevelTime - TimePassed + ExtraTime;
        if (TimeLeft <= 0)
        {
            TimeLeft = 0;
            GameOver();
        }
        
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
        UpdateTime();
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
    public void AddTime(float time)
    {
        ExtraTime += time;
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
        Score = (int)LevelTime - (int)TimePassed + (int)ExtraTime;
        GameMenuController.Instance.OpenGameWonPanel();
    }
}
