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

    public void sendScreenshotTutorial()
    {
        ScreenCapture.CaptureScreenshot(userID + "_tutorial.jpg");
        StorageReference uploadRef = storageReference.Child(userID + "_tutorial.jpg");
        StorageReference uploadImageRef = storageReference.Child(userID + "_tutorial.jpg");
        string url = Application.persistentDataPath + "/" + userID + "_tutorial.jpg";
        byte[] bytes = File.ReadAllBytes(url);

        //StorageReference uploadRef = storageReference.Child("assets/" + userID + "_tutorial.png");
        uploadRef.PutBytesAsync(bytes).ContinueWithOnMainThread((task) =>
        {
            if (task.IsFaulted || task.IsCanceled)
            {
                Debug.Log(task.Exception.ToString());
            }
            else { }
        });
    }


    public void sendScreenshotBuild()
    {
        if (Application.platform == RuntimePlatform.Android)
        {
            ScreenCapture.CaptureScreenshot(userID + "_build.jpg");
            StorageReference uploadRef = storageReference.Child(userID + "_build.jpg");
            StorageReference uploadImageRef = storageReference.Child(userID + "_build.jpg");
            string url = Application.persistentDataPath + "/" + userID + "_build.jpg";
            byte[] bytes = File.ReadAllBytes(url);

            uploadRef.PutBytesAsync(bytes).ContinueWithOnMainThread((task) =>
            {
                if (task.IsFaulted || task.IsCanceled)
                {
                    Debug.Log(task.Exception.ToString());
                }
                else { }
            });
        }
    }

    public void sendScreenshotDrawMP()
    {
        if (Application.platform == RuntimePlatform.Android)
        {
            ScreenCapture.CaptureScreenshot(userID + "_drawMP.jpg");
            StorageReference uploadRef = storageReference.Child(userID + "_drawMP.jpg");
            StorageReference uploadImageRef = storageReference.Child(userID + "_drawMP.jpg");
            string url = Application.persistentDataPath + "/" + userID + "_drawMP.jpg";
            byte[] bytes = File.ReadAllBytes(url);

            uploadRef.PutBytesAsync(bytes).ContinueWithOnMainThread((task) =>
            {
                if (task.IsFaulted || task.IsCanceled)
                {
                    Debug.Log(task.Exception.ToString());
                }
                else { }
            });
        }
    }

    public void sendScreenshotDrawOP()
    {
        if (Application.platform == RuntimePlatform.Android)
        {
            ScreenCapture.CaptureScreenshot(userID + "_drawOP.jpg");
            StorageReference uploadRef = storageReference.Child(userID + "_drawOP.jpg");
            StorageReference uploadImageRef = storageReference.Child(userID + "_drawOP.jpg");
            string url = Application.persistentDataPath + "/" + userID + "_drawOP.jpg";
            byte[] bytes = File.ReadAllBytes(url);

            uploadRef.PutBytesAsync(bytes).ContinueWithOnMainThread((task) =>
            {
                if (task.IsFaulted || task.IsCanceled)
                {
                    Debug.Log(task.Exception.ToString());
                }
                else { }
            });
        }
    }

    public void setVersion(int number)
    {
        string key = "version";
        var parent = reference.Child("users").Child(userID).Child(version).Push();
        parent.SetValueAsync(number);
    }

    public void setFeeling(string text)
    {
        var parent = reference.Child("users").Child(userID).Child(feeling).Push();
        parent.SetValueAsync(text);
    }
}
