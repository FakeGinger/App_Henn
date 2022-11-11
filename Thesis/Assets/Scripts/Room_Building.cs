using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room_Building : MonoBehaviour
{
    public GameObject database;
    public GridBuildingSystem gridBuilder;
    private GameObject editingElements;
    private BoxCollider collider;

    //Lokation des Objekts
    private Vector3 screenPoint;
    private Vector3 offset;
    private Vector3 startPos;
    private Vector3 returnPos;

    public Transform inhalt;

    public  bool buildMode;
   
    public bool placement;
    public GameObject placeholder;
 

    //Datenbank und Logging
    public string roomSize; //Manuell einfügen

    void Start()
    {
        database = GameObject.Find("Database");
        gridBuilder = GameObject.Find("GridBuildHolder").GetComponent<GridBuildingSystem>();
        editingElements = GameObject.Find("Room_Edit_Obj"); //Sucht das GUI-Element für die Raumbearbeitung
        collider = this.GetComponent<BoxCollider>();

        buildMode = true;
        placement = false;

        var outline = inhalt.GetComponent<Outline>();
        outline.enabled = true;

        if (Globals.objectID != 0)
        {
            placeholder = GameObject.Find(Globals.objectID.ToString());
            placeholder.GetComponent<Room_Building>().placeObject();
        }
        
        this.name = this.GetInstanceID().ToString();
        database.GetComponent<DatabaseManagement>().SendLog(Globals.worldTime + ": Raum " + this.name + " mit der Größe " + roomSize + " wurde auf der Position " + this.transform.position + " erschaffen.");

        Globals.objectID = this.GetInstanceID();
        Globals.activeObject = this.transform.gameObject;
        editingElements.SetActive(true); //Reaktiviert das GUI-Element für die Raumbearbeitung
        Globals.buildRoom = false;
    }

    void OnMouseDown()
    {
        if (Globals.placeRoom && !Globals.notification);
        {
            Globals.buildRoom = true;

            if (Globals.notification != true && Globals.placementOkay)
            {
                if (Globals.objectID != this.GetInstanceID() && Globals.objectID != 0)
                {
                    placeholder = GameObject.Find(Globals.objectID.ToString());
                    placeholder.GetComponent<Room_Building>().placeObject();
                }
                if (Globals.buildMode == true)
                {
                    if (placement == true)
                    {
                        placement = false;
                        Globals.objectID = this.GetInstanceID();
                    }

                    else if (placement == false)
                    {
                        this.transform.GetComponent<Collider>().isTrigger = false;
                        Globals.objectID = this.GetInstanceID();
                        Globals.activeObject = this.transform.gameObject;

                        startPos = this.transform.position;
                        screenPoint = Camera.main.WorldToScreenPoint(gameObject.transform.position);
                        offset = gameObject.transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z));

                        var outline = inhalt.GetComponent<Outline>();
                        outline.enabled = true;

                        editingElements.SetActive(true);

                        if (buildMode == false && Globals.objectID == this.GetInstanceID())
                        {
                            buildMode = true;
                        }
                    }
                }
            }
        }
    }

    void OnMouseDrag()
    {
        if (Globals.buildMode == true && Globals.notification != true)
        {
            if (Globals.objectID != this.GetInstanceID() && Globals.objectID != 0) //Warum ist das hier?
            {
                placeholder = GameObject.Find(Globals.objectID.ToString());
                placeholder.GetComponent<Room_Building>().placeObject();
            }
            Vector3 mousePosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z);
            Vector3 newPosition = Camera.main.ScreenToWorldPoint(mousePosition);
            newPosition.y = transform.position.y;
            transform.position = newPosition;               
        }
    }

    void OnMouseUp()
    {

        if (Globals.buildMode == true)
        {
            if (Globals.objectID != this.GetInstanceID() && Globals.objectID != 0) //Warum ist das hier?
            {
                placeholder = GameObject.Find(Globals.objectID.ToString());
                placeholder.GetComponent<Room_Building>().placeObject();
            }
            
            if (placement == false)
            {
                //Berechnung der Ints, um die Position auf dem Grid zu bestimmen
                int x = Mathf.FloorToInt(this.transform.position.x);
                int z = Mathf.FloorToInt(this.transform.position.z);
                returnPos = gridBuilder.GridPosition(x, z);

                //Positioniert Wand korrekt auf dem Grid
                this.transform.position = returnPos;
            }
        }

        //Globals.buildRoom = false;
        database.GetComponent<DatabaseManagement>().SendLog(Globals.worldTime + ": Raum " + this.name + " wurde auf die Position " + this.transform.position + " bewegt.");
    }

    void OnTriggerEnter(Collider col)
    {
        if (Globals.placeRoom)
        {
            if (Globals.objectID == this.GetInstanceID())
            {
                if (col = col.GetComponent<Collider>())
                {
                    var outline = inhalt.GetComponent<Outline>();
                    outline.enabled = false;
                    outline.OutlineColor = Color.red;
                    outline.enabled = true;
                    Globals.placementOkay = false;
                }
            }
        }
    }

    void OnTriggerExit(Collider col)
    {
        if (Globals.placeRoom)
        {
            if (Globals.objectID == this.GetInstanceID())
            {
                Globals.placementOkay = true;
                var outline = inhalt.GetComponent<Outline>();
                outline.enabled = false;
                outline.OutlineColor = Color.white;
                outline.enabled = true;
            }
        }
    }


    public void rotate()
    {
        this.transform.Rotate(0, 90f, 0);
        database.GetComponent<DatabaseManagement>().SendLog(Globals.worldTime + ": Raum " + this.name + " wurde rotiert.");
    }

    public void delete()
    {
        database.GetComponent<DatabaseManagement>().SendLog(Globals.worldTime + ": Raum " + this.name + " wurde gelöscht.");
        Globals.objectID = 0;
        Globals.activeObject = null;
        editingElements.SetActive(false);
        Globals.placementOkay = true;
        Destroy(this.transform.gameObject);
    }

    public void placeObject()
    {
        if (Globals.placementOkay)
        {
            database.GetComponent<DatabaseManagement>().SendLog(Globals.worldTime + ": Raum " + this.name + " wurde auf der Position " + this.transform.position + " platziert.");
            var outline = inhalt.GetComponent<Outline>();
            outline.enabled = false;
            buildMode = false;
            Globals.objectID = 0;
            editingElements.SetActive(false);
            this.transform.GetComponent<Collider>().isTrigger = true;
            Globals.buildRoom = false;
        } else{}
    }
}
