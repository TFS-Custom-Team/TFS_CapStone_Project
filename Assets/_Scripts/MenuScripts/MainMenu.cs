using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : Menu
{
    [Header("Menus")]
    public Menu mainMenu;
    public Menu settingsMenu; // Reference to the SettingsMenu
    [Header("Buttons")]
    [SerializeField] private Button playButton;
    [SerializeField] private Button settingsButton;
    [SerializeField] private Button quitButton;
    
    
    private void Start()
    {

        Show(mainMenu);
    }

    public void ShowSettingsMenu()
    {
        Debug.Log("ShowSettingsMenu Triggered");
        Show(settingsMenu);

    }

    public void StartGame()
    {
        Hide();
        SceneManager.LoadScene(1);
    }

    public void QuitGame()
    {
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #else
        Application.Quit();
        #endif
    }

}