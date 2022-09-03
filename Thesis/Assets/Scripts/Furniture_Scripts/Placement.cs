using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Placement : MonoBehaviour
{
    public GameObject placeObject;
    public Sprite active;
    public Sprite inactive;
    Image ImageComp;

    public GameObject activeGameObject;

    void Start()
    {
        placeObject = this.transform.parent.gameObject;

        ImageComp = GetComponent<Image>();
    }

    void Update()
    {
        if(Globals.placementOkay)
        {
            ImageComp.sprite = active;
        } else if(!Globals.placementOkay)
        {
            ImageComp.sprite = inactive;
        }
    }

    public void placement()
    {
        if (ImageComp.sprite != inactive)
        {
            activeGameObject = Globals.activeObject;
            if (activeGameObject.tag == "Furniture")
            {
                activeGameObject.GetComponent<FurnitureHandler>().placeObject();
            }
            else
            {
                activeGameObject.GetComponent<Room_Building>().placeObject();
            }
        }
    }
}
