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
    public RectTransform Health;
    public Text TimeText;
    public Text ScoreText;

    [Header("Prefabs")]
    public GameObject HealthImage;

    private List<GameObject> _healthPacks = new List<GameObject>();

    private void Awake()
    {
        Instance = this;
    }

    // Use this for initialization
    void Start()
    {
        CloseGameOverPanel();
        UpdateHealthPacks();
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
        ScoreText.text = LevelController.Instance.Score.ToString();
    }
    public void UpdateHealthPacks()
    {
        foreach (var item in _healthPacks)
        {
            Destroy(item);
        }
        _healthPacks = new List<GameObject>();
        for (int i = 0; i < LevelController.Instance.Health; i++)
        {
            GameObject health = Instantiate(HealthImage);
            _healthPacks.Add(health);
            health.transform.SetParent(Health);
            health.transform.localPosition = Vector3.zero;
            RectTransform healthTransform = health.GetComponent<RectTransform>();
            healthTransform.anchoredPosition = new Vector2(48 + i * 80, 0);
            healthTransform.localScale = new Vector3(1, 1, 1);
        }
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
