using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Puzzle_Keys : MonoBehaviour
{
    public GameObject key1;
    public GameObject key2;
    public GameObject key3;
    public GameObject door;

    bool light1On = false;
    bool light2On = false;
    bool light3On = false;


    void Update()
    {
        if(Globals.puzzleCounter == 1 && !light1On)
        {
            key1.GetComponent<Light>().enabled = true;
            light1On = true;
        }

        if (Globals.puzzleCounter == 2 && !light2On)
        {
            key2.GetComponent<Light>().enabled = true;
            light2On = true;
        }

        if (Globals.puzzleCounter == 3 && !light3On)
        {
            key3.GetComponent<Light>().enabled = true;
            light3On = true;
            door.SetActive(false);
        }

    }
}
