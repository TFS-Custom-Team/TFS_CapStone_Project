using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro; // TextMeshPro
using Dan.Main; // Dan's Main
using UnityEngine.UI;
using System; // UI

public class LeaderBoard : MonoBehaviour
{
    [SerializeField] private List<TextMeshProUGUI> nameText;
    [SerializeField] private List<TextMeshProUGUI> scoreText;
    [SerializeField] private List<TextMeshProUGUI> rankText;
    [SerializeField] private TMP_InputField nameInputField;
    [SerializeField] private Button submitButton;
    [SerializeField] private TextMeshProUGUI score;

    private Transform entryContainer;
    private Transform entryTemplate;
    private List<HighScoreEntry> highScoreEntryList;
    private List<Transform> highScoreEntryTransformList;

    private readonly float templateHeight = 30f; // Height of each entry in the leaderboard

    private void Awake()
    {
        //Find the nameInputField in the scene
        nameInputField = GameObject.Find("InputField")?.GetComponent<TMP_InputField>();
        if (nameInputField == null)
        {
            Debug.LogError("InputField is not assigned.");
        }

        //Find the submitButton in the scene
        submitButton = GameObject.Find("Button")?.GetComponent<Button>();
        if (submitButton == null)
        {
            Debug.LogError("SubmitButton is not assigned.");
        }

        // Find highScoreEntryContainer in the scene
         entryContainer = GameObject.Find("highScoreEntryContainer")?.transform;

        // Find highScoreEntryTemplate within highScoreEntryContainer
         entryTemplate = entryContainer.Find("highScoreEntryTemplate");

        //Hide the template on the screen once the game starts on Awake
         entryTemplate.gameObject.SetActive(false);

        //Hide InputField inside the highScoreEntryContainer
         nameInputField.gameObject.SetActive(false);

        //Hide SubmitButton inside the highScoreEntryContainer
         submitButton.gameObject.SetActive(false);

        //Initialize and populate the highScoreEntryList with some example data
        highScoreEntryList = new List<HighScoreEntry>()
        {
          // Add a new high score entry to the list
            new HighScoreEntry { score = 5210850, name = "Dan" },
            new HighScoreEntry { score = 5220950, name = "Bob" },
            new HighScoreEntry { score = 5231050, name = "Alice" },
            new HighScoreEntry { score = 5241150, name = "Eve" },
            new HighScoreEntry { score = 5251250, name = "Mallory" },
            new HighScoreEntry { score = 5261350, name = "Trent" },
            new HighScoreEntry { score = 5271450, name = "Carol" },
            new HighScoreEntry { score = 5281550, name = "Dave" },
            new HighScoreEntry { score = 5291650, name = "Frank" },
            new HighScoreEntry { score = 5301750, name = "Frank2BFrank" }
        };

        //Sort the highScoreEntryList by score
        highScoreEntryList.Sort((a, b) => b.score.CompareTo(a.score));

        //Create a new entry for each new high score and or score entry
        highScoreEntryTransformList = new List<Transform>();
        for (int i = 0; i < highScoreEntryList.Count && i < 10; i++) // Display top 10
        {
            CreateHighScoreEntryTransform(i + 1, entryContainer, highScoreEntryList[i], highScoreEntryTransformList);
        }
          submitButton.onClick.AddListener(OnClickSubmitButton);
     }
    
    //Create a new high score entry transform and set the values
    private void CreateHighScoreEntryTransform(int rank, Transform container, HighScoreEntry highScoreEntry, List<Transform> transformList)
    {
        Transform entryTransform = Instantiate(entryTemplate, container);
        RectTransform entryRectTransform = entryTransform.GetComponent<RectTransform>();
        entryRectTransform.anchoredPosition = new Vector2(0, -templateHeight * transformList.Count);
        entryTransform.gameObject.SetActive(true);

        // Set the rank
        string rankString = rank switch
        {
            1 => "1ST",
            2 => "2ND",
            3 => "3RD",
            _ => rank + "TH",
        };

        //Set the rankText
        var rankText = entryTransform.Find("rankText")?.GetComponent<TextMeshProUGUI>();
        rankText.text = rankString;

        // Set the score
        var scoreText = entryTransform.Find("scoreText")?.GetComponent<TextMeshProUGUI>();
        scoreText.text = highScoreEntry.score.ToString();

        // Set the name
        var nameText = entryTransform.Find("nameText")?.GetComponent<TextMeshProUGUI>();
        nameText.text = highScoreEntry.name;

        // Adjust the width of the nameText RectTransform
        RectTransform nameTextRectTransform = nameText.GetComponent<RectTransform>();
        if (nameTextRectTransform != null)
        {
            nameTextRectTransform.sizeDelta = new Vector2(300, nameTextRectTransform.sizeDelta.y); // Adjust 200 to the desired width
        }

        // Add the entry transform to the list
        transformList.Add(entryTransform);
        Debug.Log($"Added entry: {rankString} {highScoreEntry.name} {highScoreEntry.score}");
    }

    private void OnClickSubmitButton()
    {
        string playerName = nameInputField.text;
       //Check if the player name is empty
        if (string.IsNullOrEmpty( playerName ) )
        {
            Debug.LogError("Player Name is empty");
            return;
        }
        //PlayerScore to be dynamically set from the game
        //int playerScore = 7987750; // Temp Hard coded user score which should be the actual score of the player
        int playerScore = GameManager.Instance.score;
        AddToLeaderboard(playerName, playerScore);
        DisplayUserScore(playerScore);

        //Hide the InputField and SubmitButton after the player submits their name
        nameInputField.gameObject.SetActive(false);
        submitButton.gameObject.SetActive(false);
    }

    //Add a new high score entry to the list
    private void AddToLeaderboard(string playerName, int playerScore)
    {
       //Adds a new entry to the high score list and update ui if its a high score replacing the lowest score
       HighScoreEntry newEntry = new HighScoreEntry { score = playerScore, name = playerName };
        highScoreEntryList.Add(newEntry);
        highScoreEntryList.Sort((a, b) => b.score.CompareTo(a.score));
        highScoreEntryList = highScoreEntryList.GetRange(0, Mathf.Min(highScoreEntryList.Count, 10));
        GetLeaderBoard();
    }

    //Display User Score inside the highScoreEntryContainer
    private void DisplayUserScore(int userScore)
       {
       if (score != null)
        {
            score.text = $"Your Score: {userScore}";
            score.gameObject.SetActive(true);
            score.color = Color.red;
        }
      }

    //Check if the player score is a high score
    private bool IsNewHighScore(int playerScore)
    {
        //Check if the high score list has fewer than 10 entries,its a new high score
        if (highScoreEntryList.Count < 10)
        {
            return true;
        }
        //Otherwise, Compare aginst the lowest high score
        return playerScore > highScoreEntryList[^1].score;
    }

    //Called when the game ends and the final score is known
    public void GameOver(int playerScore)
    {
        //Check if the player score is a high score
        if (IsNewHighScore(playerScore))
        {
            //Show the InputField and SubmitButton
            nameInputField.gameObject.SetActive(true);
            submitButton.gameObject.SetActive(true);
        }
    }

    //Represents a single high score entry
    private class HighScoreEntry
    {
        public int score;
        public string name;
    }

    //Shows the Leaderboard on the screen[UI] After the game ends (Game Over) working in progress
    public void GetLeaderBoard()
    {
        Debug.Log("Get LeaderBoard is Called");

        //This method used to refresh the leaderboard
        foreach (Transform entryTransform in highScoreEntryTransformList)
        {
            Destroy(entryTransform.gameObject);
        }

        highScoreEntryTransformList.Clear();

        // Display top 10
        for (int i = 0; i < highScoreEntryList.Count && i < 10; i++)
        {
            CreateHighScoreEntryTransform(i + 1, entryContainer, highScoreEntryList[i], highScoreEntryTransformList);
        }
    } 
}
