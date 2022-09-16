using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(DamageApplier))]
public class Punching : MonoBehaviour
{
    public float Cooldown = 1.0f;
    public float AOE;
    public Transform PunchingPoint;
    public LayerMask Targets;

    float _lastTime = 0.0f;
    DamageApplier _damageApplier;


    public bool CanPunch => Time.time - this._lastTime > Cooldown;

    private void Start()
    {
        this._damageApplier = GetComponent<DamageApplier>();
    }

    private void OnDrawGizmos()
    {
        if (!PunchingPoint) return;
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(PunchingPoint.position, AOE);
    }

    public bool Punch()
    {
        if (!CanPunch) return false;
        this._lastTime = Time.time;
        return true;
    }

    public void PunchOnAnimationKeyEvent()
    {
        Collider[] hits = Physics.OverlapSphere(this.PunchingPoint.position, AOE, Targets);
        foreach (Collider hit in hits)
            this._damageApplier.TryToApplyDamageToTarget(hit.gameObject);
    }
}
