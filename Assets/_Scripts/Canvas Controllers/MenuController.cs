using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuController : MonoBehaviour {

    public GameObject MainMenuPanel;
    public GameObject CreditsPanel;
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
        UpdateInputs();
    }
    void UpdateInputs()
    {
        if (Input.GetButtonDown("Cancel"))
        {
            OpenMainMenuWindow();
        }
    }
    public void LoadLevel(string level)
    {
        if (Application.CanStreamedLevelBeLoaded(level))
        {
            SceneManager.LoadScene(level, LoadSceneMode.Single);
        }
        else
        {
            Debug.Log("Scene '" + level +"' does not exist");
        }
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
        CreditsPanel.SetActive(false);
    }
    public void OpenCreditsWindow()
    {
        MainMenuPanel.SetActive(false);
        CreditsPanel.SetActive(true);
    }
    public void ExitGame()
    {
        Application.Quit();
    }
}
