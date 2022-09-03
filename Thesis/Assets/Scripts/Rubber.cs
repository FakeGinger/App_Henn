using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rubber : MonoBehaviour
{
    private bool delete;
    private GameObject database;
    public GameObject newWall;
    
    void Start()
    {
        database = GameObject.Find("Database");
    }

    public void activateDeletion()
    {
        delete = true;
    }

    public void build()
    {
        delete = false;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && delete == true)
        {
            RaycastHit hit = new RaycastHit();
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.tag == "Window" && Globals.placeWindow)
                {
                    var placeholder = new Vector3();
                    placeholder = hit.collider.transform.position;

                    var rotation = new Quaternion();
                    rotation = hit.collider.transform.rotation;

                    Destroy(hit.collider.gameObject);
                    Instantiate(newWall, placeholder, rotation);
                    database.GetComponent<DatabaseManagement>().SendLog("Objekt " + hit.collider.tag + " auf der Position " + placeholder + " wurde gelöscht.");
                }

                else if (hit.collider.tag == "Door" && !Globals.placeWindow)
                {
                    var placeholder = new Vector3();
                    placeholder = hit.collider.transform.position;

                    var rotation = new Quaternion();
                    rotation = hit.collider.transform.rotation;

                    Destroy(hit.collider.gameObject);
                    Instantiate(newWall, placeholder, rotation);
                    database.GetComponent<DatabaseManagement>().SendLog("Objekt " + hit.collider.tag + " auf der Position " + placeholder + " wurde gelöscht.");
                }

                }
            }
        }
    }


