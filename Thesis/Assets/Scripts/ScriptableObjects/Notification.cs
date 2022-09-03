using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Notification : MonoBehaviour
{
    [Header("UI Content")]
    [SerializeField] public Text notificationTextUI;
    [SerializeField] public Image notificationIconUI;

    [Header("ScriptableObject")]
    [SerializeField] public Notification_Scriptable noteScriptable;

    void Start()
    {
        EnableNotification();
    }
    void EnableNotification()
    {
        if (noteScriptable == null) { }
        else
        {
            notificationTextUI.text = noteScriptable.notificationMessage;
            notificationIconUI.sprite = noteScriptable.icon;
        }
       
    }

    public void ResetNotification()
    {
        notificationTextUI.text = noteScriptable.notificationMessage;
        notificationIconUI.sprite = noteScriptable.icon;
    }
    
}
