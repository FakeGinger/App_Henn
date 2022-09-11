using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateManager : MonoBehaviour
{
    public GameObject VRCollection;
    public GameObject mainCamera;
    public GameObject TutorialChamber;
    public GameObject Gamehandler;
    public GameObject database;
    private string time;
    private float timer;
    

    void Start()
    {
        time = Globals.worldTime;
        timer = Globals.worldTimer;
    }

    void Update()
    {
        timer += Time.deltaTime;
        calculateTime(timer);
    }

    void calculateTime(float timeToDisplay)
    {
        timeToDisplay += 1;
        float minutes = Mathf.FloorToInt(timeToDisplay / 60);
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);
        time = string.Format("{0:00}:{1:00}", minutes, seconds);
        Globals.worldTime = time;
    }



    public void notificationOn()
    {
        Globals.notification = true;
    }

    public void notificationOff()
    {
        Globals.notification = false;
        Globals.alert = false;
    }

    public void buildRooms()
    {
        Globals.placeRoom = true;
    }

    public void noRooms()
    {
        Globals.placeRoom = false;
    }

    public void noBuild()
    {
        Globals.buildRoom = false;
        Globals.placeDoor = true;
    }

    public void buildWindows()
    {
        Globals.placeWindow = true;
    }

    public void noWindows()
    {
        Globals.placeWindow = false;
    }

    public void takeCaptureOne()
    {
        ScreenCapture.CaptureScreenshot("Building.png");
    }

    public void activateVR()
    {
        VRCollection.SetActive(true);
        mainCamera.SetActive(false);
        Gamehandler.GetComponent<VRSwitch>().test();
        TutorialChamber.SetActive(true);
    }
}