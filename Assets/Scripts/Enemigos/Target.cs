﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
    Esta clase sirve para ir descontando vida al enemigo y cuando llega a 0
    muere
*/
public class Target : MonoBehaviour
{
    public float health = 50f;

    // Método que controla el daño que recibe un enemigo
    public void takeDamage(float amount)
    {
        health -= amount;
        if (health <= 0f)
        {
            StartCoroutine(die());
        }
    }

    // Método que destruye al enemigo cuando pasa el tiempo de la animacion de muerte
    IEnumerator die()
    {
        yield return new WaitForSeconds(2f);
        Destroy(gameObject);
    }


}
