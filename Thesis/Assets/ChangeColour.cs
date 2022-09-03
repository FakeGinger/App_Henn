using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeColour : MonoBehaviour
{
   public void changeThis()
    {
            this.GetComponent<Image>().color = new Color32(255, 250, 150, 255);     
    }

    public void backToWhite()
    {
        this.GetComponent<Image>().color = new Color32(255, 255, 255, 255);
    }
}
