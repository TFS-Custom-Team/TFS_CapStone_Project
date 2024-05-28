using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class MenuManager : MonoBehaviour
{
    
    // If the canavsParent in LevelScene is made the Prefab instead of the UIcanvas, all this funcionality 
    // should move seemlessly to other levels that are made, if you need help implementing that
    // ping Makenzie on discord

    bool TestMode = false;

    [Header("Button")]
    public Button playButton;
    
    public Button quitButton;
    public Button pauseButton;
    public Button resumeButton;
    public Button resetButton;
    public Button returnToMenu;
    public Button backButton;

    [Header("Menus")]
    public GameObject mainMenu;
    public GameObject pauseMenu;
    public GameObject settingsMenu;
    public GameObject GeneralUI;

    

    // Start is called before the first frame update
    void Start()
    {
        

       

        if (quitButton)
            quitButton.onClick.AddListener(Quit);

        if (playButton)
            playButton.onClick.AddListener(() => ChangeScene(1));

        if (pauseButton)
        {
            pauseButton.onClick.AddListener(() => PauseGame());
        }

        if (resumeButton)
            resumeButton.onClick.AddListener(() => PauseGame());
    }

    // Update is called once per frame
    void Update()
    {
        
    }



    /// <summary>
    /// This will update the scene to the provived int
    /// </summary>
    /// <param name="scene"></param>
    public void ChangeScene(int scene)
    {
        if (TestMode)
            Debug.Log($"Changing scene to {scene}");
        SceneManager.LoadScene(scene);
    }


    /// <summary>
    /// check if the editor is running, if yes, end playmode, if not close application
    /// </summary>
    void Quit()
    {
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #else
        Application.Quit();
        #endif
    }

    /// <summary>
    /// first call will pause the game, calling it again will unpause
    /// </summary>
    void PauseGame()
    {
        if (!pauseMenu) return;

        
        pauseMenu.SetActive(!pauseMenu.activeSelf);
        
        
        if (pauseMenu.activeSelf)
        { // pause by stopping 
            Time.timeScale = 0.0f;
            GeneralUI.SetActive(false);
        }
        else
        { // unpause by resuming time
            Time.timeScale = 1.0f;
            GeneralUI.SetActive(true);
        }
        
    }
}
