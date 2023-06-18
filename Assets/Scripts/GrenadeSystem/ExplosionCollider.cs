using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionCollider : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        // Check if the collider belongs to a target
        if (other.gameObject.GetComponent<Target>() != null)
        {
            // Apply damage to the target
            EnemyAI enemy = other.gameObject.GetComponent<EnemyAI>();
        }
    }
}
