using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Threading.Tasks;

public class Menu : MonoBehaviour
{
    public static Stack<Menu> menuStack = new Stack<Menu>();


  
    /// <summary>
    /// Takes a gameobject of the menu that should be activated
    /// </summary>
    /// <param name="menu">Provide the gameobject for the menu that needs to be displayed</param>
    public void Show(Menu menu)
    {
        if (menuStack != null) // hopefully means it cant push if its null
        {
            Debug.Log($"Pushing menu: {menu.gameObject.name}");
            
            menuStack.Push(menu);
            menu.gameObject.SetActive(true);
            PrintStack();
        }    
        
    }

    /// <summary>
    /// When called it will pop the top element of menuStack
    /// </summary>
    public void Hide()
    {
        
        if (menuStack.Count > 0) // hopefully means it cant try to pop nothing
        {
            Menu menuOnTop = menuStack.Pop();
            Debug.Log($"Hiding menu: {menuOnTop.gameObject.name}");
            menuOnTop.gameObject.SetActive(false);
            PrintStack();
        }
        else
        {
            Debug.Log("Menu stack is empty, cannot pop.");
        }
    }

    /// <summary>
    /// Prints the current menu stack
    /// </summary>
    private void PrintStack()
    {
        string stackContents = "Menu Stack: ";
        foreach (var menu in menuStack)
        {
            stackContents += menu.gameObject.name + " ";
        }
        Debug.Log(stackContents);
    }

}