using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Esta clase controla la interaccion con las cajas de munición
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

    /* 
        Este método añade 30 balas a la reserva en caso de que se interactue con la caja
        y no se tenga la munición al máximo
    */
    public void interact()
    {
        if (arma.GetComponent<Gun>().magazine < 3)
        {
            arma.GetComponent<Gun>().magazine = arma.GetComponent<Gun>().magazine + 1;
            isInteracting = true;
        Debug.Log(isInteracting);
        }
        else
        {
            //Añadir codigo para mostrar un mensaje diciendo que el jugador ya tiene la munición al maximo
        }

        if(isInteracting == true)
        {
            Destroy(this.gameObject);
            isInteracting = false;
        }
    }

}
