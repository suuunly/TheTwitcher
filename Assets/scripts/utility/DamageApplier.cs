using UnityEngine;
using SDE;

public class DamageApplier : MonoBehaviour
{
    public event System.Action OnAppliedDamage;

    public int DamageAmount = 3;

    private void OnDestroy()
    {
        OnAppliedDamage.RemoveAllListeners();
    }

    public bool TryToApplyDamageToTarget(GameObject target)
    {
        Health health = target.GetComponent<Health>();
        if (!health) return false;

        health.Damage(DamageAmount);
        OnAppliedDamage?.Invoke();
        return true;
    }
}
