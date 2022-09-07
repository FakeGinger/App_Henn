﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    public float timeRemaining = 10;
    public bool timerRunning = false;
    public Text timeText;

    public GameObject notification;
    public GameObject vrCollision;
    public string description;
    public GameObject VRComps;
    public GameObject mainCamera;
    public GameObject canvasTimer;
    public GameObject final_not;

    void Update()
    {
        if (timerRunning)
        {
            if (timeRemaining > 0)
            {
                timeRemaining -= Time.deltaTime;
                calculateTime(timeRemaining);
            }
            else
            {
                timeRemaining = 0;
                nextStep();
                timerRunning = false;
            }
        }
    }

    void calculateTime(float timeToDisplay)
    {
        timeToDisplay += 1;
        float minutes = Mathf.FloorToInt(timeToDisplay / 60);
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);
        timeText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    public void StartTimer()
    {
        timerRunning = true;
    }

    void nextStep()
    {
        if (description == "Memory")
        {
            Globals.alert = true;
            notification.SetActive(true);
        } else if(description == "Reset")
        {
            notification.SetActive(false);
            vrCollision.GetComponent<VRCollision>().resetPlayer();
        } else if(description == "End")
        {
            VRComps.SetActive(false);
            mainCamera.SetActive(true);
            canvasTimer.SetActive(false);
            final_not.SetActive(true);
            Globals.notification = true;

        }
    }
    public void alertOff()
    {
        Globals.alert = false;
    }
}
