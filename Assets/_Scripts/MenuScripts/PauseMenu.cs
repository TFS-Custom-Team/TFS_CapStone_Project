using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class PauseMenu : Menu
{
    [Header("Menus/UI")]
    public Menu UI;
    

    [Header("Buttons")]
    [SerializeField] private Button backButton;
    
    [SerializeField] private Button quitButton;
    [SerializeField] private Button mainMenuButton;

    // Start is called before the first frame update
    void Start()
    {
        
    }


    public void FreezeGame()
    {
        if (!gameObject) return;


        gameObject.SetActive(!gameObject.activeSelf);


        if (gameObject.activeSelf)
        { // pause by stopping 
            Time.timeScale = 0.0f;
            
        }
        else
        { // unpause by resuming time
            Time.timeScale = 1.0f;
            
        }

    }

    public void QuitGame() 
    {
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #else
        Application.Quit();
        #endif
    }

    public void goToMainMenu()
    {
        FreezeGame();
        SceneManager.LoadScene(0);
    }

    public void GoBack()
    {
        FreezeGame();
        Hide();
        
    }
}