using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPlacement : MonoBehaviour
{
    public Transform prefabToInstantiate;
    public GameObject editing;

    public GameObject roomParent;
    private GameObject furnitureParent;
  
    public void placePrefab()
    {
        if (Globals.notification != true && Globals.placementOkay)
        {
            if (prefabToInstantiate.tag == "Room")
            {
                Globals.buildRoom = true;
                Instantiate(prefabToInstantiate, new Vector3(70, 1, 70), prefabToInstantiate.transform.rotation, roomParent.transform);
         
            } else if(prefabToInstantiate.tag == "Furniture")
            {
                furnitureParent = GameObject.Find("Furniture");
                Globals.buildRoom = true;
                Instantiate(prefabToInstantiate, new Vector3(70, 1, 70), prefabToInstantiate.transform.rotation, furnitureParent.transform);
            }
            editing.SetActive(true);
        }
    }
}
