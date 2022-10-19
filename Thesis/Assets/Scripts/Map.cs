using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map : MonoBehaviour
{
    public static string[,] mapping = new string[11, 11];
    public GameObject wall;
    public GameObject key;

    public void createMap()
    {
        var test = GameObject.FindGameObjectsWithTag("Floor");

        foreach (GameObject testing in test)
        {
            int x = Mathf.FloorToInt(testing.transform.position.x / 10);
            int z = Mathf.FloorToInt(testing.transform.position.z / 10);

            if (x > -1 && x < 10 && z > -1 && z < 10)
            {
                mapping[x, z] = "x";
            }
        }
    }

    public bool removeDoors()
    {
        var test = GameObject.FindGameObjectsWithTag("Door");

        foreach (GameObject testing in test)
        {
            int x = Mathf.FloorToInt(testing.transform.position.x / 10);
            int z = Mathf.FloorToInt(testing.transform.position.z / 10);

            if (testing.transform.rotation.y > 0)
            {
                if (x == 0 || mapping[x - 1, z] == null || mapping[x, z] == null)
                {
                    Instantiate(wall, testing.transform.position, testing.transform.rotation);
                    testing.gameObject.SetActive(false);
                    Debug.Log("hier");
                    //Log
                    return true;
                }
            }
            else if (testing.transform.rotation.y == 0 && z != 0)
            {
                if (mapping[x, z - 1] == null || mapping[x, z] == null)
                {
                    Instantiate(wall, testing.transform.position, testing.transform.rotation);
                    testing.gameObject.SetActive(false);
                    //Log
                    return true;
                }
            }
        }
        return false;
    }

    public void mapFurniture()
    {
        var test = GameObject.FindGameObjectsWithTag("Furniture");

        foreach (GameObject testing in test)
        {
            int x = Mathf.FloorToInt(testing.transform.position.x / 10);
            int z = Mathf.FloorToInt(testing.transform.position.z / 10);

            if (x > -1 && x < 10 && z > -1 && z < 10)
            {
                mapping[x, z] = "F";
            }
        }
    }

    public void setKeys()
    {
        int placedKeys = 0;
        while (placedKeys < 3)
        {
            int pos = Random.Range(0, 9);
            int posZ = Random.Range(0, 9);

            if (mapping[pos, posZ] == "x")
            {
                Instantiate(key, new Vector3(pos * 10, 2, posZ * 10), Quaternion.identity);
                mapping[pos, posZ] = "K";
                placedKeys++;
            }
        }
    }

    public string returnMap(int x, int z)
    {
        try
        {
            return mapping[x, z];
        }
        catch (System.IndexOutOfRangeException)
        {
            return " ";
        }
    }

    public void changeMap(int x, int z, string p)
    {
        try
        {
            mapping[x, z] = p;
        }
        catch (System.IndexOutOfRangeException)
        {
            
        }
    }
}
