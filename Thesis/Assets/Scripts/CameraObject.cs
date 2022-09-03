using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraObject : MonoBehaviour
{
    public Transform target;

    public float smoothSpeed = 0.125f;
    public Vector3 offset;

    //void FixedUpdate()
    //{
    //    if (Globals.buildRoom == true)
    //    {
    //        transform.position = target.position + offset;

    //        transform.LookAt(target);
    //    }
    //}
}
