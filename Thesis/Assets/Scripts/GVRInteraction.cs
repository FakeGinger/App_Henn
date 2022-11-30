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

    public int distanceOfRay = 10;
    private RaycastHit _hit;

    void Update()
    {
        if(GVRStatus)
        {
            GVRTimer += Time.deltaTime;
            circle.fillAmount = GVRTimer / totalTime;
        }

        Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));
        if(Physics.Raycast(ray, out _hit, distanceOfRay))
        {
            if(circle.fillAmount == 1)
            {
                _hit.transform.gameObject.GetComponent<AnswerCubes>().logAnswer();
            }
        }
    }

    public void LookOn()
    {
        GVRStatus = true;
        Debug.Log("hit");
    }

    public void LookOff()
    {
        GVRStatus = false;
        GVRTimer = 0;
        circle.fillAmount = 0;
    }
}
