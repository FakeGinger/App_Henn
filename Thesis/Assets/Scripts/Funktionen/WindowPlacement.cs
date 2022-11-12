using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindowPlacement : MonoBehaviour
{
    public GameObject window;
    public GameObject database;
    private bool deactivateBuild;


    void Start()
    {
        //database = GameObject.Find("Database");
        deactivateBuild = false;
    }

    public void setBuild()
    {
        deactivateBuild = true;
    }

    public void getBuild()
    {
        deactivateBuild = false;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && deactivateBuild == false)
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
                    Instantiate(window, placeholder, rotation);

                    database.GetComponent<DatabaseManagement>().SendLog(Globals.worldTime + ": Fenster wurde bei " + placeholder + " platziert");
                }
            }
        }
    }

}
