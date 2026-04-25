using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIGameTimer : MonoBehaviour
{

    [Header("Timer UI")]
    public TextMeshProUGUI timerText;

    //used to start timer when game round starts
    public bool startTimer = false;

    // how much time passed (seconds)
    public float timeElapsed;

    //checks to see if the timer is running
    public bool timerRunning;

    // Start is called before the first frame update
    void Start()
    {
        timerRunning = startTimer;
        timeElapsed = 0f;
        UpdateTimerUI();
    }

    // Update is called once per frame
    void Update()
    {
        //checks if the timer is running
        if(!timerRunning){
            return;
        }

        timeElapsed += Time.deltaTime;
        UpdateTimerUI();
    }

    void UpdateTimerUI(){
        if(timerText == null){
            return;
        }

        //calculates the seconds and minutes
        int seconds = Mathf.FloorToInt(timeElapsed % 60f);
        int minutes = Mathf.FloorToInt(timeElapsed / 60f);

        //update the values in the timer text
        timerText.text = "Timer: " + minutes + ":" + seconds.ToString("00");
    }

    //starts the timer
    public void StartTimer(){
        timerRunning = true;
    } 

    //stops the timer
    public void StopTimer(){
        timerRunning = false;
    } 

    //used to get the time elapsed
    public float GetTimeElapsed(){
        return timeElapsed;
    }

    //resets the timer
    public void ResetTimer(){
        timeElapsed = 0f;
        UpdateTimerUI();
    }
}
