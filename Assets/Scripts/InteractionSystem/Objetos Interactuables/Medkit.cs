using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    public void interact()
    {
        
        Debug.Log("Interactuando medkit");
        if (player.GetComponent<PlayerMovement>().currentLife <= 75)
        {
            player.GetComponent<PlayerMovement>().currentLife = player.GetComponent<PlayerMovement>().currentLife + 25;
            isInteracting = true;
        }
        else if(player.GetComponent<PlayerMovement>().currentLife > 75 && player.GetComponent<PlayerMovement>().currentLife < 100)
        {
            player.GetComponent<PlayerMovement>().currentLife = player.GetComponent<PlayerMovement>().maxLife;
            isInteracting = true;
        }
        else
        {

        }
        if(isInteracting == true)
        {
            Destroy(this.gameObject);
            isInteracting = false;
        }
    }
}
