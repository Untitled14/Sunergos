using System.Collections;
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
        float timeleft = LevelController.Instance.LevelTime - LevelController.Instance.TimePassed;
        int minutes = (int)(timeleft / 60);
        int seconds = (int)(timeleft % 60);
        TimeText.text = string.Format("{0}:{1}", minutes, seconds);
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
        PausePanel.SetActive(true);
    }
    public void ClosePausePanel()
    {
        PausePanel.SetActive(false);
    }
    public void OpenGameOverPanel()
    {
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
