using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuController : MonoBehaviour {

    public GameObject MainMenuPanel;
    public GameObject LevelsPanel;

    public RectTransform LevelContent;
    public GameObject Level;
    int LevelNumber = 2;
    private List<Button> _buttons;
	// Use this for initialization
	void Start () {
        OpenMainMenuWindow();

    }
	
	// Update is called once per frame
	void Update () {
		
	}
    public void LoadLevel(string level)
    {
        SceneManager.LoadScene(level, LoadSceneMode.Single);
    }
    public void NewGame()
    {
        SceneManager.LoadScene("Level 1", LoadSceneMode.Single);
    }
    public void OpenLevelsWindow()
    {
        MainMenuPanel.SetActive(false);
        LevelsPanel.SetActive(true);
        
    }
    public void OpenMainMenuWindow()
    {
        MainMenuPanel.SetActive(true);
        LevelsPanel.SetActive(false);
    }
    public void ExitGame()
    {
        Application.Quit();
    }
}
