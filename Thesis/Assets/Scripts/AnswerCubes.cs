using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnswerCubes : MonoBehaviour
{
    public GameObject database;
    public string answer;
    public GameObject[] otherCubes;
    public GameObject wall;
   
    
    public void logAnswer() {

        database.GetComponent<DatabaseManagement>().setFeeling(answer);
        wall.SetActive(false);

        foreach(GameObject cubes in otherCubes)
        {
            cubes.GetComponent<Renderer>().material.color = new Color(0, 0, 0);
        }
        this.GetComponent<Renderer>().material.color = new Color(0, 178, 68);
    }
}
