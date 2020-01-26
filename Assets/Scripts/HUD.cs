using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

	//doing all the work with text
public class HUD : MonoBehaviour {

	[SerializeField]
	Text text;
	float elapsedSeconds = 0;
	const string ScorePrefix = "Time: ";
	bool timerIsRunning = true;

	// Use this for initialization
	void Start () {
		text.text =  ScorePrefix + elapsedSeconds.ToString();
		
	}
	
	// Update is called once per frame
	void Update () {
		if (timerIsRunning == true)
		{
		elapsedSeconds += Time.deltaTime;
		text.text =  ScorePrefix + elapsedSeconds.ToString("0.0");
		}
	
	
	}
	public void StopGameTimer()
	{
		timerIsRunning = false;
	}
}
