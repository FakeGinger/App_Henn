using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class question_check : MonoBehaviour
{
    public GameObject question;

    public void check()
    {
        if(Globals.notification)
        {

        } else
        {
            question.SetActive(true);
        }
    }
}
