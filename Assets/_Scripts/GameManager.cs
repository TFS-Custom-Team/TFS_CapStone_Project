using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    public Text scoreText;
    public Text timeText;
    public Text waveCountText;

    private int score;
    private float elapsedTime;
    private int waveCount;

    private WaveManager waveManager;

    // Start is called before the first frame update
    void Start()
    {
        //Initialise score, time and wave count to 0
        score = 0;
        elapsedTime = 0;
        waveCount = 0;

        //find the wave manager
        waveManager = FindObjectOfType<WaveManager>();
        //check here that the waveManager is found
        if(waveManager == null)
        {
            Debug.Log("Wave manager not found");
        }
        
        //update the GUI
        UpdateGUI();
    }

    // Update is called once per frame
    void Update()
    {
        //update the elasped time
        elapsedTime += Time.deltaTime;
        //update GUI
        UpdateGUI();
    }

    //function for updating the GUI 
    private void UpdateGUI()
    {
        scoreText.text = "Score: " + score;
        //timeText.text = "Time: " + think a mathf logic is needed here, just not sure how yet.
        waveCountText.text = "Wave: " + waveCount;
    }
    //function to handle wave started and update this in the GUI
    private void HandleWaveStarted()
    {
        //some logic here that I am not sure of
        UpdateGUI();
    }
}
