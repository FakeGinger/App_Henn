using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class GVRInteraction : MonoBehaviour
{
    public Image circle;
    public float totalTime = 2;
    bool GVRStatus;
    public float GVRTimer;
    public UnityEvent GVRClick;

    void Update()
    {
        if(GVRStatus)
        {
            GVRTimer += Time.deltaTime;
            circle.fillAmount = GVRTimer / totalTime;
        }

        GVRClick.Invoke();
    }

    public void LookOn()
    {
        GVRStatus = true;
    }

    public void LookOff()
    {
        GVRStatus = false;
        GVRTimer = 0;
        circle.fillAmount = 0;
    }
}
