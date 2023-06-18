using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Esta clase controla el movimiento de la camara con los inputs recibidos por el ratón
public class MouseLook : MonoBehaviour
{
    // Start is called before the first frame update
    public float sensibilidadRaton = 100f;
    public Transform cuerpoJugador;
    float xRotation = 0f;
    
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }
 
    //Convierte los movimientos del teclado en los movimientos de la cara del juego    
    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * sensibilidadRaton * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * sensibilidadRaton * Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        cuerpoJugador.Rotate(Vector3.up * mouseX); 
        
    }
}
