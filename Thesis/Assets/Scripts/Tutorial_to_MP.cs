using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Tutorial_to_MP : MonoBehaviour
{
    public GameObject success;
    public GameObject noSuccess;
  public void switchScenes()
    {
        SceneManager.LoadScene("MitPUT");
    }

   public void countFurniture()
    {
        var furniture = GameObject.FindGameObjectsWithTag("Furniture");
        int counter = 0;

        foreach(GameObject test in furniture) {
            counter++;
        }

        if(counter == 0)
        {
            noSuccess.SetActive(true);
        } else
        {
            success.SetActive(true);
        }
    }
}
