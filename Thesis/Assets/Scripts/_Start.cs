using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class _Start : MonoBehaviour
{
    public string userID;
    public string link;

    void Start()
    {
        //Für LimeSurvey
        //userID = SystemInfo.deviceUniqueIdentifier;
        //link = "https://claustrophobiavr.limesurvey.net/329637?token=llDx866cZvDy24k&newtest=Y&Phone_ID=" + userID;  
    }

    public void open()
    {
        Application.OpenURL(link);
    }

    public void loadScene()
    {
        int scene = Random.Range(1, 3);
        SceneManager.LoadScene(scene);
    }
}
