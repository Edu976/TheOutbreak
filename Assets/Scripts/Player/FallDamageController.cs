using UnityEngine;

public class FallDamageController : MonoBehaviour {

    public float maxFallDistance = 10f;
    public float fallDamageMultiplier = 1f;
    public float minDamageThreshold = 5f;
    public AudioClip damageSoundEffect;

    private bool playerDead = false;
    private AudioSource audioSource;

    private void Start() {
        audioSource = GetComponent<AudioSource>();
    }

    private void OnCollisionEnter(Collision collision) {
        if (collision.collider.tag == "ground") {
            float distanceFallen = Mathf.Abs(transform.position.y - collision.transform.position.y);
            if (distanceFallen > maxFallDistance && !playerDead) {
                float damage = (distanceFallen - minDamageThreshold) * fallDamageMultiplier;
                if (damage > 0f) {
                    ApplyDamage(damage);
                }
            }
        }
    }

    private void ApplyDamage(float damageAmount) {
        // TODO: apply damage to player
        Debug.Log("Player received " + damageAmount + " fall damage");
        if (audioSource != null && damageSoundEffect != null) {
            audioSource.PlayOneShot(damageSoundEffect);
        }
    }
}
