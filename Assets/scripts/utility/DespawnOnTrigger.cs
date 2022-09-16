using UnityEngine;
using SDE;

public class DespawnOnTrigger : MonoBehaviour
{
    public float DespawnDelay = 0.5f;
    private void OnTriggerEnter(Collider other)
    {
        Health health = other.GetComponent<Health>();
        if (health) health.Damage(1000);

        StartCoroutine(Timing.GenericDelay(DespawnDelay, () => other.gameObject.SetActive(false)));        
    }
}
