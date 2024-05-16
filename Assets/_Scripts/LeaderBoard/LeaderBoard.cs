using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro; // TextMeshPro
using Dan.Main; // Dan's Main
using UnityEngine.UI;
using System; // UI

public class LeaderBoard : MonoBehaviour
{

    private Transform entryContainer;
    private Transform entryTemplate;

    private void Awake()
    {
       entryContainer = transform.Find("highScoreEntryContainer");
       entryTemplate = entryContainer.Find("highScoreEntryTemplate"); 


       //Hides the template on the screen once the game starts on Awake
       entryTemplate.gameObject.SetActive(false);


       //Add a new highscore entry to the leaderboard
       float templateHeight = 30f;
        for (int i = 0; i < 10; i++)
        {
              Transform entryTransform = Instantiate(entryTemplate, entryContainer);
              RectTransform entryRectTransform = entryTransform.GetComponent<RectTransform>();
              entryRectTransform.anchoredPosition = new Vector2(0, -templateHeight * i);
              entryTransform.gameObject.SetActive(true);

            int rank = i + 1;
            string rankString;
            switch (rank)
            {
                default:
                    rankString = rank + "TH"; break;

                case 1: rankString = "1ST"; break;
                case 2: rankString = "2ND"; break;
                case 3: rankString = "3RD"; break;
            }

            entryTransform.Find("rankText").GetComponent<TextMeshProUGUI>().text = rankString;

            //int score = UnityEngine.Random.Range(0, 100);
            //entryTransform.Find("scoreText").GetComponent<TextMeshProUGUI>().text = score.ToString();

            string name = "AAA";
            entryTransform.Find("nameText").GetComponent<TextMeshProUGUI>().text = name;
        }
    }
}
