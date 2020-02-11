using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CountdownTimer : MonoBehaviour {

	[SerializeField] public Text digitalTimer;
	[SerializeField] public Image radialTimer;

	[SerializeField] private int timeLimit;

    public bool isRunning = false;

	// Use this for initialization
	void Start () {
        StartCoroutine("BeginCount", timeLimit);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	///<summary>
    /// Starts the timer with an integer passed as the time limit. The passed integer is the number of seconds you want the timer to run for. Ie. for 1 minute you should pass in 60.
    ///</summary>
    public IEnumerator BeginCount(int tlimit)
    {
        Debug.Log("Timer has been started");

        isRunning = true;

		timeLimit = tlimit;
        float radialInterval = 1.0f / timeLimit;

        for (int x = timeLimit; x > 0; x--)
        //for (int x = 0; x < 15; x++)
        {
            int minutes;
            int seconds;
            yield return new WaitForSeconds(1);
            timeLimit -= 1;

            seconds = timeLimit % 60;
            minutes = (timeLimit - seconds) / 60;

            string secStr = "";
            if (seconds < 10){
                secStr = "0" + seconds.ToString();
            }
            else{
                secStr = seconds.ToString();
            }
            
            digitalTimer.text = String.Format("{0}:{1}", minutes, secStr);
            radialTimer.fillAmount = radialTimer.fillAmount - radialInterval;
            if (radialTimer.fillAmount < 0.25){
                radialTimer.color = Color.red;
                BlinkTimer();
            }
  
            //Debug.Log("TIME is : " + x.ToString());
        }

        StopTimer();
    }

	///<summary>
    /// Changes the color of the digitalTimer game object's text between black and red
    ///</summary>
    private void BlinkTimer(){
        if (digitalTimer.color == Color.black){
            digitalTimer.color = Color.red;
        }
        else{
            digitalTimer.color = Color.black;
        }
    }

	private void StopTimer(int x = 0){
		StopCoroutine(BeginCount(x));
        isRunning = false;

        //Resets to default settings
        radialTimer.color = Color.green;
        radialTimer.fillAmount = 1;
        digitalTimer.color = Color.black;

        //Add other functionality here
	}
}
