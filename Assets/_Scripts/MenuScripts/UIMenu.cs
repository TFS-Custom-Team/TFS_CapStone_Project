using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIMenu : Menu
{
    public PauseMenu PauseMenu;
    [Header("UI Menu")]
    public Menu pauseMenu;
    public Menu UI;


    [Header("Buttons")]
    [SerializeField] private Button puaseButton;

    
    // Start is called before the first frame update
    void Start()
    {
        Show(UI);
    }
    public void PauseGame()
    {
        PauseMenu.FreezeGame();
        Show(pauseMenu);
        
    }
   
}
