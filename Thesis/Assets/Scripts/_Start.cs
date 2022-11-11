using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class _Start : MonoBehaviour
{
    public string userID;
    public string link;
    public GameObject database;
    private string time;
    private float timer;

    void Start()
    {
        //Für LimeSurvey
        userID = SystemInfo.deviceUniqueIdentifier;
        link = "https://claustrophobiavr.limesurvey.net/329637?token=llDx866cZvDy24k&newtest=Y&Phone_ID=" + userID;  
        database.GetComponent<DatabaseManagement>().SendLog(Globals.worldTime + ": App-Start");
        database.GetComponent<DatabaseManagement>().SendLog("----------");
    }

    void Update()
    {
        timer += Time.deltaTime;
        calculateTime(timer);
    }

    public void open()
    {
        database.GetComponent<DatabaseManagement>().SendLog(Globals.worldTime + ": Der erste Fragebogen wird gestartet.");
        Application.OpenURL(link);
    }

    public void loadScene()
    {
        database.GetComponent<DatabaseManagement>().SendLog(Globals.worldTime + ": Der erste Fragebogen wurde beendet.");
        database.GetComponent<DatabaseManagement>().SendLog("----------");
        int scene = Random.Range(1, 2);
        database.GetComponent<DatabaseManagement>().SendLog(Globals.worldTime + ": Version " + scene +  " wird geladen.");
        database.GetComponent<DatabaseManagement>().setVersion(scene);
        database.GetComponent<DatabaseManagement>().SendLog("----------");
        Globals.worldTimer = timer;
        SceneManager.LoadScene(scene);
    }

    void calculateTime(float timeToDisplay)
    {
        timeToDisplay += 1;
        float minutes = Mathf.FloorToInt(timeToDisplay / 60);
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);
        time = string.Format("{0:00}:{1:00}", minutes, seconds);
        Globals.worldTime = time;
    }
}
