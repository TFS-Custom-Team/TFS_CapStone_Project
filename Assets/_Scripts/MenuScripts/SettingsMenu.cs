using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingsMenu : Menu
{
    // Start is called before the first frame update
    [Header("Menus")]
    public Menu mainMenu;
    public Menu settingsMenu; // Reference to the SettingsMenu
    [Header("Buttons")]
    
    
    [SerializeField] private Button backButton;

   public void GoBack()
    {
        Debug.Log("Goback called");
        Hide();
    }
}
