using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NavMeshGenerator : MonoBehaviour
{
    public NavMeshSurface surface;

    [SerializeField] private GameObject navMeshPlayer;
    [SerializeField] private GameObject destination;

    private float x;
    private float z;

    //NavMesh PathExists
    public bool pathAviable;
    public NavMeshPath navMeshPath;
    private Vector3 newVector;
    public NavMeshAgent test;

    //Notificatoin
    public GameObject UI_Alert;
    public Notification_Scriptable UI_Alert_Path;

    //UIs
    [SerializeField] private GameObject DoorUI;
    [SerializeField] private GameObject FurnitureUI;

    public GameObject furnitureCollection;
    public GameObject VRCollection;
    public GameObject mainCamera;
    public GameObject Gamehandler;
    public GameObject TutorialChamber;

    public GameObject Map;
    public GameObject noPath;
    public GameObject path;
    public GameObject moreFurniture;

    private GameObject database;
    public string switchHere;

    void Start()
    {
        database = GameObject.Find("Database");
    }

    public void buildMesh()
    {
        if (this.name == "NavMesh_Furniture" && furnitureCollection.transform.childCount <= 5)
        {
            moreFurniture.SetActive(true);
            Globals.notification = true;

        } else {

            navMeshPath = new NavMeshPath();

            x = Mathf.RoundToInt(destination.transform.position.x);
            z = Mathf.RoundToInt(destination.transform.position.z);
            newVector = new Vector3(x, 1, z);

            surface.BuildNavMesh();

            if (CalculateNewPath() == true)
            {
                path.SetActive(true);
                Globals.notification = true;

                if (this.name != "NavMesh_Furniture")
                {
                    DoorUI.SetActive(false);
                    FurnitureUI.SetActive(true);
                    database.GetComponent<DatabaseManagement>().SendLog("Es wurden ausreichend Türen platziert. Platzierung von Fenstern wird gestartet.");
                }
                else
                {
                    if (Globals.objectID != 0)
                    {
                        string test = Globals.objectID.ToString();
                        var activeFurniture = GameObject.Find(test);
                        activeFurniture.GetComponent<FurnitureHandler>().placeObject();
                    }
                    if (switchHere == "Yes")
                    {
                        activateVR(); //Zweite Phase
                        Map.GetComponent<CeilingBuilder>().Counter();
                    }
                }
                    Globals.alert = true;
                    Globals.buildMode = false;
            }
            else
            {
                noPath.SetActive(true);
                database.GetComponent<DatabaseManagement>().SendLog("Es wurde eine Überprüfung gestartet, doch es gibt keinen Weg durch das Haus.");
                Globals.notification = true;
            }
        }
    }


    bool CalculateNewPath()
    {
        test.CalculatePath(newVector, navMeshPath);

        if(navMeshPath.status != NavMeshPathStatus.PathComplete)
        {
            return false;
        } else
        {
            return true;
        }
    }

    private void activateVR()
    {
        VRCollection.SetActive(true);
        mainCamera.SetActive(false);
        Gamehandler.GetComponent<VRSwitch>().test();
        TutorialChamber.SetActive(true);
    }
}
