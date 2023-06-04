using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController controller;
    public float gravity = -9.8f;
    public GameObject BloddOverlay;
    Camera cam;

    [Header("Player Specs")]
    public float maxLife = 300;
    public float currentLife;
    public Text lifeDisplay;
    public float normalSpeed = 12f;
    public float sprintSpeed = 1f;
    public float jumpHeight = 3f;
    public float crouchHeigth = 1.6f;
    public float normalHeigth = 3.8f;
    public float fallHeight;
    Vector3 velocity;

    [Header("Checkers")]
    public Transform groundCheck;
    public float groundDistace = 0.4f;
    public LayerMask groundMask;
    bool isGrounded;
    public bool isDamaged;

    // Start is called before the first frame update
    void Start()
    {
        currentLife = maxLife;
        isDamaged = false;
        BloddOverlay.SetActive(false);
        cam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        lifeDisplay.text = currentLife.ToString();
        /*  
            Crea una esfera invisible al final del jugador que comprueba si este esta en contacto con el suelo
            si colisiona con algo que esta dentro de la mascara groundMask isGrounded == true
            si no colisiona isGrounded == false
        */
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistace, groundMask);
        //Debug.Log(isGrounded);

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;
        controller.Move(move * normalSpeed * sprintSpeed * Time.deltaTime);

        //Gravedad
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);

        //Sprint
        if (Input.GetKeyDown(KeyCode.LeftShift) && isGrounded)
        {
            sprintSpeed = 2.5f;
        }
        else if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            sprintSpeed = 1f;
        }

        // Check if space key is pressed and player is on the ground
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            // Calculate jump velocity using physics formula: v = sqrt(h * -2 * g)
            float jumpVelocity = Mathf.Sqrt(jumpHeight * -2f * gravity);

            // Set the vertical velocity of the player to jump velocity
            velocity.y = jumpVelocity;
        }

        /*
                //Salto
                if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
                {
                    velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
                }
        */

        //Agacharse
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            controller.height = crouchHeigth;
        }
        if (Input.GetKeyUp(KeyCode.LeftControl))
        {
            controller.height = normalHeigth;
        }

        //Muerte del personaje
        if (currentLife <= 0)
        {
            dead();
        }
    }

    public IEnumerator takeDamage()
    {
        isDamaged = true;
        if (isDamaged == true)
        {
            BloddOverlay.SetActive(true);
            yield return new WaitForSeconds(3f);
            BloddOverlay.SetActive(false);
        }
        isDamaged = false;
    }
    void dead()
    {
        if (currentLife <= 0)
        {
            SceneManager.LoadScene(2);
        }
    }
}
