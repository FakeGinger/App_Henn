using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Logs : MonoBehaviour
{
    public GameObject database;
    public string text;

    public void sendLog()
    {
        database.GetComponent<DatabaseManagement>().SendLog(Globals.worldTime + ": " + text);
    }
}
