﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameMenuController : MonoBehaviour
{
    public static GameMenuController Instance;
    [Header("Canvas objects")]
    public GameObject GamePanel;
    public GameObject GameOverPanel;
    public GameObject GameWonPanel;
    public GameObject PausePanel;

    [Space(10)]
    //public RectTransform Health;
    public Text TimeText;
    public Text CirclesText;
    public Text SquaresText;
    //public GameObject HealthImage;
    [Space(10)]
    public Text GameWonTextScore;
    public Text GameWonTimeText;
    private List<GameObject> _healthPacks = new List<GameObject>();

    private void Awake()
    {
        Instance = this;
    }

    // Use this for initialization
    void Start()
    {
        CloseGameOverPanel();
        ClosePausePanel();
        //UpdateHealthPacks();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateInputs();
        if (TimeText != null)
            UpdateTime();
        UpdateScore();
    }
    void UpdateTime()
    {
        TimeText.text = FormatTime(LevelController.Instance.TimeLeft);
    }
    string FormatTime(float sec)
    {
        float timeleft = sec;
        int minutes = (int)(timeleft / 60);
        int seconds = (int)(timeleft % 60);
        
        return string.Format("{0:00}:{1:00}", minutes, seconds);
    }
    void UpdateScore()
    {
        CirclesText.text = LevelController.Instance.Circles.ToString();
        SquaresText.text = LevelController.Instance.Squares.ToString();
    }
    void UpdateInputs()
    {
        if(Input.GetButtonDown("Cancel"))
        {
            if (PausePanel.activeSelf)
                ClosePausePanel();
            else
                OpenPausePanel();
        }
    }
    public void UpdateHealthPacks()
    {
    }
    void OpenPausePanel()
    {
        Time.timeScale = 0;
        LevelController.Instance.Paused = true;
        PausePanel.SetActive(true);
    }
    public void ClosePausePanel()
    {
        Time.timeScale = 1;
        LevelController.Instance.Paused = false;
        PausePanel.SetActive(false);
    }
    public void OpenGameOverPanel()
    {
        Time.timeScale = 0.5f;
        LevelController.Instance.Paused = true;

        GameOverPanel.SetActive(true);
        GamePanel.SetActive(false);
    }
    public void CloseGameOverPanel()
    {

        GamePanel.SetActive(true);
        GameOverPanel.SetActive(false);
        GameWonPanel.SetActive(false);
    }
    public void OpenGameWonPanel()
    {
        Time.timeScale = 0.5f;
        LevelController.Instance.Paused = true;

        GameWonTimeText.text = "Time: " + FormatTime(LevelController.Instance.TimePassed);
        GameWonTextScore.text = "Score: " + LevelController.Instance.Score.ToString();
        
        GameOverPanel.SetActive(false);
        GameWonPanel.SetActive(true);
        GamePanel.SetActive(false);
    }
    public void ExitToMenu()
    {
        SceneManager.LoadScene("Game", LoadSceneMode.Single);
    }
    public void PlayNextLevel()
    {
        SceneManager.LoadScene("Level " + (LevelController.Instance.LevelNumber + 1), LoadSceneMode.Single);
    }
    public void PlayAgain()
    {
        //CloseGameOverPanel();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name, LoadSceneMode.Single);
    }
    
}
