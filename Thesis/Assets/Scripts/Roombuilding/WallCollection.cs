using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallCollection : MonoBehaviour
{

    private Vector3 screenPoint;
    private Vector3 offset;

    private Vector3 start;

    public GameObject Room;
    public GridBuildingSystem findScript;

    //Snap to Grid etc
    private Vector3 testPos;
    private Vector3 endPos;

    public new List<GameObject> WallTilesWest;
    public new List<GameObject> WallTilesEast;
    public new List<Transform> WallTilesNorth;
    public new List<Transform> WallTilesSouth;

    public Transform WallSouthT;
    public Transform WallNorthT;

    public Transform WestWallHolderS;
    public Transform WestWallHolderN;

    public Transform placeholder;

    void Start()
    {
        WallTilesWest.AddRange(GameObject.FindGameObjectsWithTag("WallWest"));
        WallTilesEast.AddRange(GameObject.FindGameObjectsWithTag("WallEast"));
       
        foreach(GameObject Cube in GameObject.FindGameObjectsWithTag("WallNorth"))
        {
            WallTilesNorth.Add(Cube.GetComponent<Transform>());
        }

        foreach(GameObject Cube in GameObject.FindGameObjectsWithTag("WallSouth"))
        {
            WallTilesSouth.Add(Cube.GetComponent<Transform>());
        }

        findScript = GameObject.Find("GridBuildHolder").GetComponent<GridBuildingSystem>();
    }

    
   void OnMouseDown()
    {
        start = this.transform.position;
        screenPoint = Camera.main.WorldToScreenPoint(gameObject.transform.position);
        offset = gameObject.transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z));
    }
    
    void OnMouseDrag()
    {
        if (this.tag == "EasternWall" || this.tag == "WesternWall")
        {
            Vector3 mousePosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z);
            Vector3 newPosition = Camera.main.ScreenToWorldPoint(mousePosition);
            newPosition.z = transform.position.z;
            newPosition.y = transform.position.y;
            transform.position = newPosition;

        } else if(this.tag == "NorthernWall" || this.tag == "SouthernWall")
        {
            Vector3 mousePosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z);
            Vector3 newPosition = Camera.main.ScreenToWorldPoint(mousePosition);
            newPosition.x = transform.position.x;
            newPosition.y = transform.position.y;
            transform.position = newPosition;
        }
    }

    public void OnMouseUp()
    {
 
            //Wenn nicht mehr geklickt wird, soll die Wand an das Grid "snappen"
            endPos = this.transform.position;
            int IntX = Mathf.FloorToInt(endPos.x);
            int IntZ = Mathf.FloorToInt(endPos.z);

            //Rückgabe der Position auf dem Grid und Fixierung auf Knotenpunkt
            testPos = findScript.GridPosition(IntX, IntZ);
            int calcX = Mathf.FloorToInt(testPos.x);
            int calcZ = Mathf.FloorToInt(testPos.z);
            this.transform.position = testPos;

            buildFloor(IntX, IntZ, calcX / 10, calcZ / 10);
            
       
     }
    

    void buildFloor(int x, int z, int calcX, int calcZ)
    {
        int calc = 0;
        int StartX = Mathf.FloorToInt(start.x);
        int StartZ = Mathf.FloorToInt(start.z);
        if (this.tag == "WesternWall")
        {
            //Kalkulation der Überwundenden Distanz auf der x-Achse
            calc = (StartX - x) / 10 + 1;

            for (int i = 0; i < calc; i++)
            {
                for (int j = 0; j < WallTilesWest.Count; j++)
                {
                   if(j == 0)
                    {
                        //Transform placeholder = new Transform();
                        placeholder = (Transform) findScript.buildWall(i + calcX, calcZ, WallSouthT);
                        placeholder.transform.parent = WestWallHolderS;
                        GameObject.FindGameObjectWithTag("SouthernWall").GetComponent<WallCollection>().WallTilesSouth.Add(placeholder);
                    }

                   else if(j == WallTilesWest.Count - 1)
                    {
                        //Transform placeholder = new Transform();
                        placeholder = (Transform)findScript.buildWall(i + calcX, calcZ + 1, WallNorthT);
                        placeholder.transform.parent = WestWallHolderN;
                        GameObject.FindGameObjectWithTag("NorthernWall").GetComponent<WallCollection>().WallTilesNorth.Add(placeholder);
                    }

                    findScript.buildGround(i + calcX, j + calcZ);
                }
            }

        } else if(this.tag == "EasternWall")
          {
            calc = (x - StartX) / 10;
            for (int i = 0; i < calc; i++)
            {
                for (int j = 0; j < WallTilesWest.Count; j++)
                {
                    findScript.buildGround(calcX - i - 1, calcZ + j);
                }
            }

        } else if(this.tag == "SouthernWall") 
         {
            calc = (StartZ - z) / 10 + 1;
            for (int i = 0; i < WallTilesSouth.Count; i++)
            {
                for (int j = 0; j < calc; j++)
                {
                   findScript.buildGround(calcX - i - 1, j + calcZ);
                }
            }

        } else if(this.tag == "NorthernWall")
        {
            calc = (z - StartZ) / 10;
            for (int i = 0; i < WallTilesNorth.Count; i++)
            {
                for (int j = 0; j < calc; j++)
                {
                    findScript.buildGround(calcX - i - 1, calcZ - j - 1);

                }
            }
        }


      
    }


}



