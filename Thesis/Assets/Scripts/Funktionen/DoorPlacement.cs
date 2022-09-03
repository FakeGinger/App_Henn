using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorPlacement : MonoBehaviour
{
    public GameObject door;
    public GameObject database;
    private bool buildDoors;
    public GameObject doorCollection;
    public GameObject checking;
    public GameObject alert;

    void Start()
    {
        doorCollection = GameObject.Find("DoorsColl");
        database = GameObject.Find("Database");
        buildDoors = false;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && buildDoors && !Globals.notification)
        {
            RaycastHit hit = new RaycastHit();
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.tag == "Wall")
                {
                    var placeholder = new Vector3();
                    placeholder = hit.collider.transform.position;

                    var rotation = new Quaternion();
                    rotation = hit.collider.transform.rotation;

                    Destroy(hit.collider.gameObject);
                    Instantiate(door, placeholder, rotation, doorCollection.transform);

                    database.GetComponent<DatabaseManagement>().SendLog("Tür wurde bei " + placeholder + " platziert");

                    if (checking.GetComponent<Map>().removeDoors())
                    {
                        alert.SetActive(true);
                        Globals.notification = true;
                    }
                }
            }
        }
    }

    public void build()
    {
        buildDoors = true;
    }

    public void dontBuild()
    {
        buildDoors = false;
    }
}
