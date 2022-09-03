using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ObjectHandling : MonoBehaviour
{
    public GridBuildingSystem gridBuilder;

    //Lokation des Objekts
    private Vector3 screenPoint;
    private Vector3 offset;
    private Vector3 startPos;
    private Vector3 returnPos;
    
    public GameObject inhalt;

    private bool objectTransformation;
    private bool objectPlacementOkay;


    void Start()
    {
      gridBuilder = GameObject.Find("GridBuildHolder").GetComponent<GridBuildingSystem>();
      objectPlacementOkay = false;

      objectTransformation = true;
    }

    void OnMouseDown()
    {
        if (Globals.buildMode == true)
        {
            startPos = this.transform.position;
            screenPoint = Camera.main.WorldToScreenPoint(gameObject.transform.position);
            offset = gameObject.transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z));
        }

        if(objectTransformation == false)
        {
            objectTransformation = true;
        }
    }

    void OnMouseDrag()
    {
        if (Globals.buildMode == true)
        {
            Vector3 mousePosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z);
            Vector3 newPosition = Camera.main.ScreenToWorldPoint(mousePosition);
            newPosition.y = transform.position.y;
            transform.position = newPosition;
        }

    }


    //Überprüfung der Kollison mit Floor-Tiles

    void OnTriggerEnter(Collider col)
    {
        if(col.GetComponent<Collider>().tag == "FloorDetection" && this.tag == "Furniture")
        {
            var outline = inhalt.GetComponent<Outline>();
            outline.enabled = false;
            objectPlacementOkay = true;
        }
    }


    void OnTriggerExit(Collider col)
    {
        if (col.GetComponent<Collider>().tag == "FloorDetection" && this.tag == "Furniture")
        {
            var outline = inhalt.GetComponent<Outline>();
            outline.enabled = true;
            outline.OutlineColor = Color.red;
            objectPlacementOkay = false;
        }
    }





    //BUTTON-FUNKTIONEN

    public void rotate()
    {
        this.transform.Rotate(0, 90f, 0);
    }

   public void delete()
    {
        Destroy(gameObject);
    }

    public void placeObject()
    {
        //Berechnung der Ints, um die Position auf dem Grid zu bestimmen
        int x = Mathf.FloorToInt(this.transform.position.x);
        int z = Mathf.FloorToInt(this.transform.position.z);
        returnPos = gridBuilder.GridPosition(x, z);

        //Positioniert Wand korrekt auf dem Grid
        this.transform.position = returnPos;

        var outline = inhalt.GetComponent<Outline>();
        outline.enabled = false;

        objectTransformation = false;
    }
}
