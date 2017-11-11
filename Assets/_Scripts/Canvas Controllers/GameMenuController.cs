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

    [Space(10)]
    //public RectTransform Health;
    public Text TimeText;
    public Text CirclesText;
    public Text SquaresText;
    //public GameObject HealthImage;

    private List<GameObject> _healthPacks = new List<GameObject>();

    private void Awake()
    {
        Instance = this;
    }

    // Use this for initialization
    void Start()
    {
        CloseGameOverPanel();
        //UpdateHealthPacks();
    }

    // Update is called once per frame
    void Update()
    {
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
    public void UpdateHealthPacks()
    {
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
    }
    public void ExitToMenu()
    {
        SceneManager.LoadScene("Game", LoadSceneMode.Single);
    }
    public void PlayAgain()
    {
        CloseGameOverPanel();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name, LoadSceneMode.Single);
    }
}
