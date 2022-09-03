using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Puzzle_Block : MonoBehaviour
{
    public bool isLookedAt;

    void Update()
    {
        if(isLookedAt)
        {
            onWatch();
        }
    }

    public void onWatch()
    {
        Globals.puzzleCounter++;
        Debug.Log(Globals.puzzleCounter);
        this.gameObject.SetActive(false);
    }

    public void SetGazedAt(bool gazedAt)
    {
        isLookedAt = gazedAt;
    }
}
