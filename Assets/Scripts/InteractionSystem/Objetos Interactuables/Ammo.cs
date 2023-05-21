using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ammo : MonoBehaviour, IInteractable
{
    public GameObject arma;
    public bool isInteracting = false;
    void Start()
    {
        arma = GameObject.FindWithTag("weapon");
    }

    public string GetDescription()
    {
        return "Para coger munición";
    }

    public void interact()
    {
        Debug.Log("Interactuando Ammo");
        if (arma.GetComponent<Gun>().magazine < 3)
        {
            arma.GetComponent<Gun>().magazine = arma.GetComponent<Gun>().magazine + 1;
            isInteracting = true;
        Debug.Log(isInteracting);
        }
        else
        {
            //Add code to play sound or show message to player telling them they cannot pick up any more ammo.
        }

        if(isInteracting == true)
        {
            Destroy(this.gameObject);
            isInteracting = false;
        }
    }

}
