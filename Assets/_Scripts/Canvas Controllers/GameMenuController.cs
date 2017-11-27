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
    public Image DamagedImage;
    private Color _damageImageColor;
    private bool _showDamagedImage;
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

    private float _t = 0;
    private float _menuT = 0;

    private void Awake()
    {
        Instance = this;
    }

    // Use this for initialization
    void Start()
    {
        _damageImageColor = DamagedImage.color;
        DamagedImage.gameObject.SetActive(false);

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
        UpdateDamageImage();
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
        StartCoroutine(OpenGameOverDelay());
       
    }
    IEnumerator OpenGameOverDelay()
    {
        yield return new WaitForSeconds(1);
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
        string sceneName = "Level " + (LevelController.Instance.LevelNumber + 1);
        if (Application.CanStreamedLevelBeLoaded(sceneName))
        {
            SceneManager.LoadScene(sceneName, LoadSceneMode.Single);
        }
        else
        {
            Debug.Log("No more scenes");
        }
    }
    public void PlayAgain()
    {
        //CloseGameOverPanel();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name, LoadSceneMode.Single);
    }

    void UpdateDamageImage()
    {
        if (_showDamagedImage)
        {
            Color newColor = _damageImageColor;
            newColor.a = 0;
            DamagedImage.color = Color.Lerp(_damageImageColor, newColor, _t);
            _t += Time.deltaTime * 2;
            if (_t >= 1)
            {
                _showDamagedImage = false;
                DamagedImage.gameObject.SetActive(false);
            }
        }
    }
    public void ShowDamagePlayerImage()
    {
        _showDamagedImage = true;
        DamagedImage.gameObject.SetActive(true);
        _t = 0;
    }

}
