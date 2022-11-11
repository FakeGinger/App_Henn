using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Globals 
{
    //Editiermodus für den Hausbau
    public static bool roomBuilding = false;
    public static bool roomSelected = false;


    public static bool buildMode = true;
    public static bool deletion = false;


    public static float objectID;
    

    //Für das Build-Menü
    public static bool menuActive;
    public static string menuID;


    //Modes
    public static bool alert = false;
    public static bool buildRoom = false;
    public static bool furniture = false;
    public static bool placementOkay = true; //Platzierung des Raums ist in Ordnung


    //State-Management
    public static bool notification = false;
    public static bool placeRoom = false;
    public static bool placeDoor = false;
    public static bool placeWindow = false;
    public static bool placeFurniture = false;

    //Aktives
    public static GameObject activeObject;

    //Puzzle
    public static int puzzleCounter;
    public static bool secondRun = false;

    //Zeit
    public static string worldTime = "00:00";
    public static float worldTimer;
    public static bool waiting = false;

}
