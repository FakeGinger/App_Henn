using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotation : MonoBehaviour
{

    public GameObject activeGameobject;

    public void rotate()
    {
        activeGameobject = Globals.activeObject;

        if (activeGameobject.tag == "Furniture")
        {
            activeGameobject.GetComponent<FurnitureHandler>().rotate();
        }
        else
        {
            activeGameobject.GetComponent<Room_Building>().rotate();
        }
    }
}
