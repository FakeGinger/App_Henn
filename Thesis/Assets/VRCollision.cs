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
    public GameObject wall;
    public GameObject rq;

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
                puzzleProgress();
            } else if (hit.collider.gameObject.tag == "RQ")
            {
                hit.collider.gameObject.GetComponent<rq_script>().logFeeling();
                hit.collider.gameObject.SetActive(false);
            }
        }
    }

    void puzzleProgress()
    {
        Globals.puzzleCounter++;
        message.SetActive(true);

        if (Globals.puzzleCounter < 3)
        {
            var keysLeft = 3 - Globals.puzzleCounter;
            text.text = "You found a key! Only " + keysLeft + " left.";
        }
        else if (Globals.puzzleCounter == 3)
        {
            text.text = "You found all of the keys! The final door is now open.";
        }

        StartCoroutine(RemoveAfterSeconds(2));
    }

    IEnumerator RemoveAfterSeconds(int seconds)
    {
        yield return new WaitForSeconds(seconds);
        message.SetActive(false);
    }

    public void resetPlayer()
    {
        //Reset Türen
        foreach (Transform child in DoorCol)
        {
            child.gameObject.SetActive(true);
        }

        //Reset Player
        Vector3 pos = new Vector3(40, 8, -110);
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
        wall.SetActive(false);
        rq.SetActive(true);
    }

}
