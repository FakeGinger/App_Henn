using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class counter : MonoBehaviour
{
   private int counting;

   void OnCollisionEnter(Collision col)
    {

        counting++;
        Debug.Log(counting);
    }
}
