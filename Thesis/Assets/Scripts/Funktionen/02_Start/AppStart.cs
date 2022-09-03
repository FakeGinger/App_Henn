using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AppStart : MonoBehaviour
{
    public Notification_Scriptable start;
    public GameObject notification;
    
    void Start()
    {
        Globals.alert = true;
        this.GetComponent<Notification>().noteScriptable = start;
        this.GetComponent<Notification>().ResetNotification(); //Überschreibt Notification mit korrekter Meldung. Ggf. später noch einen Reset hierfür einbauen?
        notification.SetActive(true);
    }

    public void alertOff()
    {
        Globals.alert = false;
    }
}
