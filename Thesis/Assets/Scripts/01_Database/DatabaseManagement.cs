using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase;
using Firebase.Database;
using UnityEngine.UI;

public class DatabaseManagement : MonoBehaviour
{
    private string userID;
    private string eventLog;
    private string pictures;
    private string feeling;
    private string version;
    public string Name;
    private DatabaseReference reference;

    void Start()
    {
        reference = FirebaseDatabase.DefaultInstance.RootReference;
        userID = SystemInfo.deviceUniqueIdentifier;
        eventLog = "EventLog";
        pictures = "Screenshots";
        version = "Version";
        feeling = "How anxious did you feel?";
    }

 
    public void SendLog(string text)
    {
        var parent = reference.Child("users").Child(userID).Child(eventLog).Push();
        parent.SetValueAsync(text);
    }

    public void windowsDone()
    {
        SendLog("Der Fensterbau wurde abgeschlossen. Aufgabe Möbelbau wird gestartet.");
    }

    public void sendScreenshot()
    {

    }


    public void setVersion(int number)
    {
        var parent = reference.Child("users").Child(userID).Child(version).Push();
        parent.SetValueAsync(number);
    }

    public void setFeeling(string text)
    {
        var parent = reference.Child("users").Child(userID).Child(feeling).Push();
        parent.SetValueAsync(text);
    }
}
