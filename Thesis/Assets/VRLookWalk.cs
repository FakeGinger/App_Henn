using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VRLookWalk : MonoBehaviour
{
    public Transform vrCamera;
    public float toggleAngle = 30.0f;
    public float speed = 3.0f;
    public bool moveForward;

    private CharacterController cc;
    public GameObject finish;
    public GameObject timer;
    public GameObject endOfStudy;

    // Use this for initialization
    void Start()
    {
        cc = GetComponent<CharacterController>();
    }

    void Update()
    {
        if (transform.position.z > 100 && !Globals.secondRun)
        {
            finish.SetActive(true);
            timer.GetComponent<Timer>().timerRunning = true;
            Globals.waiting = true;
        } else if(transform.position.z > 100 && Globals.secondRun)
        {
            endOfStudy.SetActive(true);
        }

        if (vrCamera.eulerAngles.x >= toggleAngle && vrCamera.eulerAngles.x < 90.0f && Globals.waiting == false)
        {
            moveForward = true;
        }
        else
        {
            moveForward = false;
        }

        if (moveForward)
        {
            Vector3 forward = vrCamera.TransformDirection(Vector3.forward);
            forward.y = 1;
            cc.SimpleMove(forward * speed);
        }
        
    }
}
