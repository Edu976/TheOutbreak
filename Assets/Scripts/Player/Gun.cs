using System.Collections;
using UnityEngine;
using UnityEngine.UI;

// Esta clase controla el sistema de armas del juego
public class Gun : MonoBehaviour
{
    [Header("Weapon Specs")]
    public GameObject weapon;
    public float damage = 10f; //Daño del arma
    public float range = 100f; // Distancia efectiva
    public float fireRate = 10f; // Cadencia
    public float recoilForce = 2f; // Fuerza del retroceso 
    public float recoilTime = 0.02f;

    [Header("Ammo")]
    public int maxAmmo = 30; // Municion maxima del cargador
    public int currentAmmo; // Municion actual en el cargador
    public int magazine = 3; // Numero de cargadores
    public Text ammoDisplay; // UI de la municion
    public Text magazineDisplay; // UI del numero de cargadores
    public float reloadingTime = 0.5f; // Tiempo en el que tarda en recargar
    private bool isReloading = false;

    [Header("Effects")]
    public Camera fpsCam;
    public ParticleSystem muzzleFlash; // Fogonazo en la punta
    public GameObject metalImpactEffect; // Impacto en metal
    public GameObject zombieImpactEffect; // Impacto en zombie
    private float nextTimeToFire = 0f;
    private AudioSource myAudioSource;
    public AudioClip shootSound;
    public AudioClip lastBullet;
    public AudioClip reloadingSound;

    void Start()
    {
        // Iniciamos la municion al maximo
        currentAmmo = maxAmmo;
        myAudioSource = GetComponent<AudioSource>();
    }
    // Update is called once per frame
    void Update()
    {

        //Municion en la UI
        ammoDisplay.text = currentAmmo.ToString();
        magazineDisplay.text = (30 * magazine).ToString();
        //Recargar
        if (currentAmmo < 30 && Input.GetKeyDown(KeyCode.R))
        {
            StartCoroutine(reload());
        }
        if (isReloading)
        {
            return;
        }
        //Recarga sin la municion es 0
        if (currentAmmo <= 0)
        {
            StartCoroutine(reload());
            return;
        }
        /* BOTONES DEL RATON: 
        - 0 izquierdo
        - 1 derecho
        - 2 ruleta
        */
        if (Input.GetMouseButton(0) && Time.time >= nextTimeToFire && PauseMenu.isPaused == false)
        {
            nextTimeToFire = Time.time + 1f / fireRate;
            myAudioSource.PlayOneShot(shootSound, 1f);
            if (currentAmmo == 1)
            {
                myAudioSource.PlayOneShot(lastBullet, 1f);
                PlaySound(lastBullet);
            }
            shoot();
        }
    }


    /*
        Metodo de disparo, al disparar crea el efecto de las particulas del fogonazo, resta una bala del cargador
        lanza un rayCast que detecta donde colisiona, si impacta en el zombie salta sangre si impacta contra metal
        crea un agujero
    */
    void shoot()
    {

        StartCoroutine(recoil());
        muzzleFlash.Play();
        currentAmmo = currentAmmo - 1;
        RaycastHit hit;
        if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, range))
        {
            Target target = hit.transform.GetComponent<Target>();
            if (target != null)
            {
                target.takeDamage(damage);
            }

            // Marca en en suelo o salpica sangre y destellos
            if (hit.transform.tag.Equals("zombie"))
            {
                GameObject impactZombieGO = Instantiate(zombieImpactEffect, hit.point, Quaternion.LookRotation(hit.normal));
                Destroy(impactZombieGO, 2f);

            }
            if (hit.transform.tag.Equals("metal"))
            {
                GameObject impactMetalGO = Instantiate(metalImpactEffect, hit.point, Quaternion.LookRotation(hit.normal));
                Destroy(impactMetalGO, 2f);
            }
        }
    }

    //Metodo de recargar se llama si la municion del cargador es 0 o se presiona la letra R
    IEnumerator reload()
    {
        if (magazine > 0)
        {
            myAudioSource.PlayOneShot(reloadingSound, 1f);
            isReloading = true;
            // espera en segundos el valor que le damos en reloadingTime
            yield return new WaitForSeconds(reloadingTime);
            currentAmmo = maxAmmo;
            magazine = magazine - 1;
            isReloading = false;
        }
        else
        {
            //Debug.Log("Sin municion");
        }
    }

    
    //Metodo que controla el retroceso del arma
    IEnumerator recoil()
    {
        weapon.transform.Rotate(-recoilForce, 0f, 0f);
        transform.position = transform.position - transform.forward * (recoilForce / 50f);
        yield return new WaitForSeconds(recoilTime);
        weapon.transform.Rotate(recoilForce, 0f, 0f);
        transform.position = transform.position - transform.forward * (-recoilForce / 50f);
    }

    //Método para playear el sonido del disparo
    void PlaySound(AudioClip clip)
    {
        myAudioSource.clip = clip;
        myAudioSource.Play();
    }
}