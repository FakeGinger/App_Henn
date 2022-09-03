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
    public string Name;
    private DatabaseReference reference;

    void Start()
    {
        reference = FirebaseDatabase.DefaultInstance.RootReference;
        userID = SystemInfo.deviceUniqueIdentifier;
        eventLog = "EventLog";
        pictures = "Screenshots";
        CreateUser();
        SendLog("Die App wurde gestartet.");
    }

    public void CreateUser()
    {
    //    User newUser = new User(Name, eventLog);
    //    string json = JsonUtility.ToJson(newUser);

        //reference.Child("users").Child(userID).SetRawJsonValueAsync(json).ContinueWith(task =>
        //{
        //    if (task.IsCompleted)
        //    {
        //        Debug.Log("SUCESSO");

        //    }
        //    else
        //    {
        //        Debug.Log("já foste");
        //    }
        //});
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
}
