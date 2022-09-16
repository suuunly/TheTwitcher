using UnityEngine;

[RequireComponent(typeof(DamageApplier))]
public class DamageOnCollision : MonoBehaviour
{
    private DamageApplier _damageApplier;

    private void Awake()
    {
        this._damageApplier = GetComponent<DamageApplier>();
    }

    private void OnCollisionEnter(Collision other)
    {
        this._damageApplier.TryToApplyDamageToTarget(other.gameObject);
    }
}
