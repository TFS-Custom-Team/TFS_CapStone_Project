using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UIElements;

/*
 * Asher Thom
 * April 19th, 2024
 * This component handles the player changing colors. 
 */
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(SpriteRenderer))]
public class ColorSwitching : MonoBehaviour
{
    // Start is called before the first frame update

    //This enumeration represent the different colors the player can turn into
    enum Colors
    {
        Blank = 0,
        Red,
        Green ,
        Blue ,
        Black,
    }
    enum Direction
    {
        Up = 0,
        Down = 1,
        Left = 2,
        Right = 3,
    }

    Rigidbody2D rb;
    SpriteRenderer sr;
    Colors currColor = Colors.Red;
    int maxListSize = 3;

    List<Direction> inputs;

    List<Direction> redCode;
    List<Direction> greenCode;
    List<Direction> blueCode;
    List<Direction> blackCode;

    void Start()
    {
        redCode = new List<Direction>();
        greenCode = new List<Direction>();
        blueCode = new List<Direction>();
        blackCode = new List<Direction>();

        inputs = new List<Direction>();

         rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();

        ChangeColor(Colors.Red);

        redCode.Add(Direction.Up);
        redCode.Add(Direction.Down);
        redCode.Add(Direction.Left);

        blueCode.Add(Direction.Down);
        blueCode.Add(Direction.Left);
        blueCode.Add(Direction.Right);

        blackCode.Add(Direction.Left);
        blackCode.Add(Direction.Right);
        blackCode.Add(Direction.Left);

        greenCode.Add(Direction.Down);
        greenCode.Add(Direction.Up);
        greenCode.Add(Direction.Left);

}

    // Update is called once per frame
    void Update()
    {




        if (Input.GetKeyDown(KeyCode.UpArrow))
            OnKeyDown(KeyCode.UpArrow);

        if(Input.GetKeyDown(KeyCode.DownArrow))
            OnKeyDown(KeyCode.DownArrow);

        if(Input.GetKeyDown(KeyCode.RightArrow)) 
            OnKeyDown(KeyCode.RightArrow);

        if(Input.GetKeyDown(KeyCode.LeftArrow))
            OnKeyDown(KeyCode.LeftArrow);

        
    }
 
    //This function handles Arrow key input. It takes a keypress event, adds the keypress to the inputs list, and deletes the last entry in the list if the list is over maxSize. It also checks if the current code is equal to a code, and changes color if so.
    void OnKeyDown(KeyCode key)
    {
        Debug.Log("Key pressed: " +  key);
        switch (key)
        {
            case KeyCode.UpArrow:
                inputs.Add(Direction.Up);
                break;
            case KeyCode.DownArrow:
                inputs.Add(Direction.Down); 
                break;
            case KeyCode.LeftArrow:
                inputs.Add(Direction.Left);
                break;
            case KeyCode.RightArrow:
                inputs.Add(Direction.Right);
                break;
        }
        if (inputs.Count > maxListSize)
            inputs.RemoveAt(0);
        //SequentialEqual checks if every entry in a list is equal to every entry in another list.
        if (inputs.SequenceEqual(redCode))
        {
            ChangeColor(Colors.Red);
            inputs.Clear();
        }
        else if (inputs.SequenceEqual(blueCode))
        {
            ChangeColor(Colors.Blue);
            inputs.Clear();
        }
        else if (inputs.SequenceEqual(greenCode))
        {
            ChangeColor(Colors.Green);
            inputs.Clear();
        }
        else if (inputs.SequenceEqual(blackCode))
        {
            ChangeColor(Colors.Black);
            inputs.Clear();
        }
    }
    void ChangeColor(Colors color) 
    {
        switch (color)
        {
            case Colors.Red:
                sr.color = Color.red;
                currColor = Colors.Red;
                break;
            case Colors.Green:
                sr.color = Color.green;
                currColor = Colors.Green;
                break;
            case Colors.Blue:
                sr.color = Color.blue;
                currColor = Colors.Blue;    
                break;
            case Colors.Black: 
                sr.color = Color.black;
                currColor = Colors.Black;
                break;
        }
    }
}
