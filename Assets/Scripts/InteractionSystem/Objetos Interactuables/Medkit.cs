using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Esta clase controla la interaccion con los botiquines
public class Medkit : MonoBehaviour, IInteractable
{
    public GameObject player;
    public bool isInteracting = false;
    void Start()
    {
        player = GameObject.FindWithTag("Player");
    }
    public string GetDescription()
    {
        return "Para coger botiquín";

    }


    /* 
        Este método añade 25 puntos de vida caso de que se interactue con el botiquín
        y no tenga la vida al máximo
    */
    public void interact()
    {

        Debug.Log("Interactuando medkit");
        if (player.GetComponent<PlayerMovement>().currentLife <= 75)
        {
            player.GetComponent<PlayerMovement>().currentLife = player.GetComponent<PlayerMovement>().currentLife + 25;
            isInteracting = true;
        }
        else if (player.GetComponent<PlayerMovement>().currentLife > 75 && player.GetComponent<PlayerMovement>().currentLife < 100)
        {
            player.GetComponent<PlayerMovement>().currentLife = player.GetComponent<PlayerMovement>().maxLife;
            isInteracting = true;
        }
        else
        {
            //Añadir codigo para mostrar un mensaje diciendo que el jugador ya tiene la vida al maximo
        }
        if (isInteracting == true)
        {
            Destroy(this.gameObject);
            isInteracting = false;
        }
    }
}
