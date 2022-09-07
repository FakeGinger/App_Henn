using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rq_script : MonoBehaviour
{
    public string feeling;
    public GameObject database;

    public void logFeeling()
    {
        database.GetComponent<DatabaseManagement>().setFeeling(feeling);
    }
}
