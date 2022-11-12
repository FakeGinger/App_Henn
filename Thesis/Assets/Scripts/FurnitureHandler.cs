using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FurnitureHandler : MonoBehaviour
{
    public GridBuildingSystem gridBuilder;

    //Lokation des Objekts
    private Vector3 screenPoint;
    private Vector3 offset;
    private Vector3 startPos;
    private Vector3 returnPos;

    public Transform inhalt;
    public GameObject editingElements; //GUI-Buttons
    public GameObject placeholder;
    public GameObject map;

    //Datenbank und Logging
    public GameObject database;
    public string typ;


    void Start()
    {
        database = GameObject.Find("Database");
        gridBuilder = GameObject.Find("GridBuildHolder").GetComponent<GridBuildingSystem>();
        editingElements = GameObject.Find("Room_Edit_Obj"); //Sucht das GUI-Element für die Raumbearbeitung
        map = GameObject.Find("Map_Functions");

        if (Globals.objectID != 0)
        {
            placeholder = GameObject.Find(Globals.objectID.ToString());
            placeholder.GetComponent<FurnitureHandler>().placeObject();
        }

        editingElements.SetActive(true);

        this.name = this.GetInstanceID().ToString();
        Globals.objectID = this.GetInstanceID();
        Globals.activeObject = this.transform.gameObject;
        Globals.buildRoom = false;

        var outline = inhalt.GetComponent<Outline>();
        outline.enabled = true;

        database.GetComponent<DatabaseManagement>().SendLog(Globals.worldTime + ": Möbelstück " + this.name + " der Art " + typ + " wurde auf der Position " + this.transform.position + " erschaffen.");

        int x = Mathf.FloorToInt(this.transform.position.x);
        int z = Mathf.FloorToInt(this.transform.position.z);
        checkMap(x, z);

    }

    void OnMouseDown()
    {
        if (Globals.objectID != this.GetInstanceID() && Globals.objectID != 0)
        {
            placeholder = GameObject.Find(Globals.objectID.ToString());
            placeholder.GetComponent<FurnitureHandler>().placeObject();
        }

        Globals.objectID = this.GetInstanceID();
        Globals.activeObject = this.transform.gameObject;
        startPos = this.transform.position;
        screenPoint = Camera.main.WorldToScreenPoint(gameObject.transform.position);
        offset = gameObject.transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z));
        Globals.buildRoom = true;

        var outline = inhalt.GetComponent<Outline>();
        outline.enabled = true;
        editingElements.SetActive(true);

        int x = Mathf.FloorToInt(this.transform.position.x);
        int z = Mathf.FloorToInt(this.transform.position.z);
        if (map.GetComponent<Map>().returnMap(x/10, z/10) != null)
        {
            Debug.Log(map.GetComponent<Map>().returnMap(x / 10, z / 10));
            map.GetComponent<Map>().changeMap(x / 10, z / 10, "x");
        }
    }

    void OnMouseDrag()
    {
        if (Globals.objectID != this.GetInstanceID() && Globals.objectID != 0)
        {
            placeholder = GameObject.Find(Globals.objectID.ToString());
            placeholder.GetComponent<Room_Building>().placeObject();
        }

        Vector3 mousePosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z);
        Vector3 newPosition = Camera.main.ScreenToWorldPoint(mousePosition);
        newPosition.y = transform.position.y;
        transform.position = newPosition;
    }

    void OnMouseUp()
    {
        int x = Mathf.FloorToInt(this.transform.position.x);
        int z = Mathf.FloorToInt(this.transform.position.z);
        returnPos = gridBuilder.GridPosition(x, z);

        this.transform.position = returnPos;

        Globals.buildRoom = false;
        database.GetComponent<DatabaseManagement>().SendLog(Globals.worldTime + ": Möbelstück " + this.name + " wurde auf die Position " + this.transform.position + " bewegt.");

        checkMap(x, z);
        
    }

    public void rotate()
    {
        this.transform.Rotate(0, 90f, 0);
        database.GetComponent<DatabaseManagement>().SendLog(Globals.worldTime + ": Möbelstück " + this.name + " wurde rotiert.");
    }

    public void delete()
    {
        database.GetComponent<DatabaseManagement>().SendLog(Globals.worldTime + ": Möbelstück " + this.name + " wurde gelöscht.");
        Globals.objectID = 0;
        Globals.activeObject = null;
        editingElements.SetActive(false);
        Destroy(this.transform.gameObject);
        Globals.placementOkay = true;
    }

    public void placeObject()
    {
        if (Globals.placementOkay)
        {
            Globals.objectID = 0;
            editingElements.SetActive(false);
            var outline = inhalt.GetComponent<Outline>();
            outline.enabled = false;
            map.GetComponent<Map>().mapFurniture();
            database.GetComponent<DatabaseManagement>().SendLog(Globals.worldTime + ": Möbelstück " + this.name + " wurde auf der Position " + this.transform.position + " platziert.");
        }
        else
        {
        }
    }

    void checkMap(int x, int z)
    {
        if(map.GetComponent<Map>().returnMap(x/10,z/10) == "x")
        {
            Globals.placementOkay = true;
            var outline = inhalt.GetComponent<Outline>();
            outline.enabled = false;
            outline.OutlineColor = Color.white;
            outline.enabled = true;
        }
        else
        {
            var outline = inhalt.GetComponent<Outline>();
            outline.enabled = false;
            outline.OutlineColor = Color.red;
            outline.enabled = true;
            Globals.placementOkay = false;
        }
    }
}
