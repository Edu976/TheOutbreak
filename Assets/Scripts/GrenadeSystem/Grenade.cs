using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grenade : MonoBehaviour
{
    public int damage;
    public float bounceForce;
    public float explosionRadius;
    public float explosionDelay;
    public AudioClip explosionSound;
    public GameObject explosionPrefab;

    private Rigidbody rb;
    private bool targetHit;
    public AudioSource myAudioSource;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        myAudioSource = GetComponent<AudioSource>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        // Make sure to only stick to the first target you hit
        if (targetHit)
            return;
        else
            targetHit = true;

        // Check if you hit an enemy
        if (collision.gameObject.GetComponent<Target>() != null)
        {
            StartCoroutine(Explode());
        }
        else
        {
            // Apply bounce force to the projectile
            Vector3 bounceDirection = Vector3.Reflect(rb.velocity.normalized, collision.contacts[0].normal);
            rb.velocity = bounceDirection * bounceForce;
            StartCoroutine(Explode());
        }
    }

    IEnumerator Explode()
    {
        yield return new WaitForSeconds(explosionDelay);
        // Instantiate the explosion prefab
        PlaySound(explosionSound);
        GameObject explosion = Instantiate(explosionPrefab, transform.position, Quaternion.identity);

        // Get all colliders within the explosion radius
        Collider[] colliders = Physics.OverlapSphere(transform.position, explosionRadius);

        // Apply damage to all targets within the explosion radius
        foreach (Collider collider in colliders)
        {
            if (collider.gameObject.GetComponent<Target>() != null)
            {
                Target enemy = collider.gameObject.GetComponent<Target>();
                enemy.takeDamage(damage);
            }
        }

        // Destroy the explosion effect after a delay
        Destroy(explosion, 2f);

        // Destroy the grenade
        
        Destroy(gameObject, 2f);
    }

    void PlaySound(AudioClip clip)
    {
        myAudioSource.clip = clip;
        myAudioSource.Play();
    }

    private void Update()
    {
        if (targetHit)
        {
            // Check if the grenade has come to rest
            if (rb.IsSleeping())
            {
                // Call the Explode method after a delay
                Invoke(nameof(Explode), 0.1f);
            }
        }
    }
}
