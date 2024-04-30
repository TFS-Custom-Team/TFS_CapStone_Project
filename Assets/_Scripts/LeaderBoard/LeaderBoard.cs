using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro; // TextMeshPro
using Dan.Main; // Dan's Main

public class LeaderBoard : MonoBehaviour
{

    [SerializeField] private List<TextMeshProUGUI> names;
    [SerializeField] private List<TextMeshProUGUI> scores;

    private string publicLeaderboardTestKey = "cfd99aaccdb895a91b1d337754ce334954947766c0f3eb6f517a6962e986b124";

    public void GetLeaderBoardTest()
    {
        LeaderboardCreator.GetLeaderboard(publicLeaderboardTestKey, ((msg) =>
        { 
            int leaderBoardLength = (msg.Length < names.Count) ? msg.Length : names.Count;
            for (int i = 0; i < names.Count; i++)
            {
                names[i].text = msg[i].Username;
                scores[i].text = msg[i].Score.ToString();
            }
        }));
    }

    public void SetLeaderboardEntry(string username, int score)
    {
        LeaderboardCreator.UploadNewEntry(publicLeaderboardTestKey, username, score, ((msg) =>
        {
            //if (System.Array.IndexOf(wordsNotPermitted, name) != -1) return;
            GetLeaderBoardTest();
            Debug.Log(msg);
        }));
    }

    // Start is called before the first frame update
    private void Start()
    {
        GetLeaderBoardTest();
    }

    // Update is called once per frame
    void Update()
    {
 
    }
}
