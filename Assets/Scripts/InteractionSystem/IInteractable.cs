using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*  
    Interfaz que extiende a todos los objetos con los que se interactua el metodo de interactuar 
    y la descripción de la interacción
 */
public interface IInteractable 
{
    void interact();
    string GetDescription();
}
