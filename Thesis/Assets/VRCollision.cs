using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VRCollision : MonoBehaviour
{
    public GameObject player;
    public GameObject ceiling;
    public Transform DoorCol;
    public Text text;
    public GameObject message;

    public Material material;
    public GameObject vrNotification;
    public GameObject _map;

    void Update()
    {

        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, 2))
        {
            if (hit.collider.gameObject.tag == "Door")
            {
                hit.collider.gameObject.SetActive(false);
            }
            else if (hit.collider.gameObject.tag == "Puzzle")
            {
                Destroy(hit.collider.gameObject);
                Globals.puzzleCounter++;
            }
        }
    }

    public void resetPlayer()
    {
        //Reset Türen
        foreach (Transform child in DoorCol)
        {
            child.gameObject.SetActive(true);
        }

        //Reset Player
        Vector3 pos = new Vector3(80, 8, -50);
        player.transform.position = pos;
        vrNotification.SetActive(false);

        //Decke tiefer
        Vector3 POSY = new Vector3(ceiling.transform.position.x, -2, ceiling.transform.position.z);
        ceiling.transform.position = POSY;

        //Dunkelheit
        RenderSettings.skybox = material;
        var lights = GameObject.FindGameObjectsWithTag("Light");
        for (int i = 0; i < lights.Length; i++)
        {
            lights[i].GetComponent<Light>().range = 22f;
        }

        _map.GetComponent<Map>().setKeys();
        Globals.puzzleCounter = 0;
        Globals.secondRun = true;
    }

}
