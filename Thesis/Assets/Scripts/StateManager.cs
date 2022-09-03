using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateManager : MonoBehaviour
{
    public GameObject VRCollection;
    public GameObject mainCamera;
    public GameObject TutorialChamber;
    public GameObject Gamehandler;
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