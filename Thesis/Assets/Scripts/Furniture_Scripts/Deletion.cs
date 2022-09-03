using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deletion : MonoBehaviour
{
    public GameObject deleteThis;

    public GameObject activeObject;


    void Start()
    {
        deleteThis = this.transform.parent.gameObject;
    }


    public void deleteObject()
    {
        activeObject = Globals.activeObject;
        if (activeObject.tag == "Furniture")
        {
            activeObject.GetComponent<FurnitureHandler>().delete();
        }
        else
        {
            activeObject = Globals.activeObject;
            activeObject.GetComponent<Room_Building>().delete();
        }
    }
}
