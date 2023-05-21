using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    [Header("Objetivo")]
    public GameObject personaje;

    [Header("Objeto hijo")]
    public GameObject enemigo;

    [Header("Parametros")]
    public NavMeshAgent nm;
    public Animator animController;

    [Header("variables")]
    public Transform target;
    public float detectionDistance = 25f;
    public bool playerDetected = false;
    public float chaseDistance = 2.5f;
    public float damage = 5;

    [Header("Sonidos")]
    public AudioClip attack;
    public AudioClip Detect;
    public AudioClip roaming;
    public AudioClip chasing;



    // Start is called before the first frame update
    void Start()
    {
        personaje = GameObject.FindWithTag("Player");
        target = personaje.transform;
        nm = GetComponent<NavMeshAgent>();
        animController = GameObject.FindWithTag("enemigo").GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        // Calculamos la dsitancia al objetivo
        float distance = Vector3.Distance(transform.position, target.position);
        // accedemos al script del elemento hijo
        enemigo = transform.GetChild(0).gameObject;
        if (this.enemigo.GetComponent<Target>().health > 0f)
        {
            if (playerDetected == false)
            {
                Debug.Log("Player no detectado");
                nm.SetDestination(this.gameObject.transform.position);
            }
            if (distance < detectionDistance)
            {
                Debug.Log("Player detectado");
                playerDetected = true;
            }
            //Debug.Log("health > 0");
            if (playerDetected == true)
            {
                Debug.Log("Zombie va a por el player");
                nm.SetDestination(target.position);
            }
            if (distance < chaseDistance)
            {
                Debug.Log("Atacamos al player");
                StartCoroutine(attackPlayer());
            }
        }
        else if (this.enemigo.GetComponent<Target>().health <= 0)
        {
            StartCoroutine(die());
        }
    }

    // Metodo para eliminar el objeto cuando la vida del enemigo llega a 0 
    IEnumerator die()
    {
        nm = GetComponent<NavMeshAgent>();
        nm.SetDestination(gameObject.transform.position);
        this.gameObject.GetComponent<Animator>().Play("Dying");
        yield return new WaitForSeconds(2f);
        Destroy(gameObject);
    }

    // Metodo para atacar al jugador
    public float attackInterval = 1f;
    private float lastAttackTime = 0f;

    IEnumerator attackPlayer()
    {
        Animator animator = gameObject.GetComponent<Animator>();
        animator.Play("Attack");
        float currentTime = Time.time;
        if (currentTime - lastAttackTime > attackInterval)
        {
            lastAttackTime = currentTime;
            PlayerMovement player = personaje.GetComponent<PlayerMovement>();
            player.currentLife -= damage;
            StartCoroutine(player.takeDamage());
        }
        float distance = Vector3.Distance(transform.position, target.position);
        if (distance <= chaseDistance)
        {
            yield return new WaitForSeconds(attackInterval);
        }
    }

}