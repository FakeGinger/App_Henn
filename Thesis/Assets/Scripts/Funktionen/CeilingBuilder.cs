using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class CeilingBuilder : MonoBehaviour
{
    int counter;
    public Vector3[] positionen;

    public Transform ceiling;
    public Transform CeilingPlaceholder;

 
    public void Counter()
    {
        var go = GameObject.FindGameObjectsWithTag("Floor");

        for (var i = 0; i < go.Length; i++)
        {
            counter++;
        }

        positionen = new Vector3[counter]; //Array hat nun die Größe der Anzahl der Floor-Tiles

        for(var i = 0; i < positionen.Length; i++)
        {
            Vector3 q = transform.position; //Positionen wurden nicht sauber übertragen, deshalb werden sie konvertiert
            q.x = Mathf.RoundToInt(go[i].transform.position.x);
            q.y = Mathf.RoundToInt(go[i].transform.position.y);
            q.z = Mathf.RoundToInt(go[i].transform.position.z);

            positionen[i] = q;
        }
        

        bauDecke();
    }

    void bauDecke()
    {
        for (int i = 0; i < positionen.Length; i++)
        {
            Instantiate(ceiling, new Vector3(positionen[i].x, positionen[i].y + 14, positionen[i].z), Quaternion.identity, CeilingPlaceholder);
        }
    }
}
