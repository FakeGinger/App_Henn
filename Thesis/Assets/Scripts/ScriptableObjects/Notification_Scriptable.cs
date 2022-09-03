using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NotificationSC")]
public class Notification_Scriptable : ScriptableObject
{
    [Header("Message Customization")]
    public Sprite icon;
    public string notificationMessage;

    [Header("Notifiaction removal")]
    public bool removeAfterClick = false;
}
