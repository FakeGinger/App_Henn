using UnityEngine;
using System.Collections;
using Firebase;
using Firebase.Database;
using Firebase.Storage;
using System.IO;
using Firebase.Extensions;

public class DatabaseManagement : MonoBehaviour
{
    private string userID;
    private string eventLog;
    private string feeling;
    private string version;
    private DatabaseReference reference;
    private FirebaseStorage storage;
    private StorageReference storageReference;

    void Start()
    {
        reference = FirebaseDatabase.DefaultInstance.RootReference;
        storage = FirebaseStorage.DefaultInstance;
        storageReference = storage.GetReferenceFromUrl("gs://bachelorarbeit-57851.appspot.com");
        userID = SystemInfo.deviceUniqueIdentifier;
        eventLog = "EventLog";
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
        //Händisch eingegeben, damit es funktioniert. Tests für Android nötig
        ScreenCapture.CaptureScreenshot("Assets/test.png");
        //für Android persistendData..
        string url = Application.dataPath + "/test.png";
        byte[] bytes = File.ReadAllBytes("Assets/test.png");

        StorageReference uploadRef = storageReference.Child("uploads/newFile.png");
        uploadRef.PutBytesAsync(bytes).ContinueWithOnMainThread((task) =>
        {
            if (task.IsFaulted || task.IsCanceled)
            {
                Debug.Log(task.Exception.ToString());
            }
            else { }
        });
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
