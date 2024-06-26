using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting;

public class GameManager : MonoBehaviour
{
	public static GameManager Instance { get; private set; }

	public TextMeshProUGUI scoreText;
	public TextMeshProUGUI timeText;
	public TextMeshProUGUI waveCountText;

	public int score;
	private float elapsedTime;
	private int waveCount;

	public WaveManager waveManager;
	public TilemapLayoutEditor layoutLoader;
	private void Awake()
	{
		//Find the scoreText in the scene
		scoreText = GameObject.Find("ScoreTxt")?.GetComponent<TextMeshProUGUI>();
		if (scoreText == null)
		{
			Debug.LogError("Score is not assigned.");
		}

		//Find the timeText in the scene
		timeText = GameObject.Find("SurvivalTimeTxt")?.GetComponent<TextMeshProUGUI>();
		if (timeText == null)
		{
			Debug.LogError("Time is not assigned.");
		}

		//Find the waveCountText in the scene
		waveCountText = GameObject.Find("WaveTxt")?.GetComponent<TextMeshProUGUI>();
		if (waveCountText == null)
		{
			Debug.LogError("WaveCount is not assigned.");
		}

		//Score Instance 
		 if (Instance == null)
		{
			Instance = this;
			DontDestroyOnLoad(gameObject);
		}
		else
		{
			Destroy(gameObject);
			return;
		}
		BeginNewWave();
	}

	// Start is called before the first frame update
	void Start()
	{
		//Initialise score, time and wave count to 0
		score = 0;
		elapsedTime = 0;
		waveCount = 0;
		//find the wave manager
		//update the GUI
		//UpdateGUI();
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
		// Update the score text
		scoreText.text = "Score: " + score;

		// Update the time text with minutes and seconds
		int minutes = Mathf.FloorToInt(elapsedTime / 60F);
		int seconds = Mathf.FloorToInt(elapsedTime % 60F);
		timeText.text = string.Format("Time: {0:00}:{1:00}", minutes, seconds);

		// Update the wave count text
		waveCountText.text = "Wave: " + waveCount;
	}
	//function to handle wave started and update this in the GUI
	public void BeginNewWave() {
		layoutLoader.Loadlevel();
		waveManager.StartNextWave();
		waveCount++;
	}
}
