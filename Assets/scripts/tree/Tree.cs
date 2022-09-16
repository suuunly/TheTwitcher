using UnityEngine;

[RequireComponent(typeof(Health), typeof(Collider))]
public class Tree : MonoBehaviour, SDE.GamePool.IPoolable
{
    public Animator Animator;
    public RagdollActivator[] Logs;

    [Header("Leaves")]
    public SDE.Data.RuntimeSet PoolSet;
    public GameObject LeavesExplosionParticles;

    public GameObject[] Leaves;


    private Collider _collider;
    private Health _health;

    void Destroyed()
    {
        this._collider.enabled = false;
        foreach (RagdollActivator Log in Logs)
            Log.Activate();

        foreach (GameObject leaves in Leaves)
        {
            leaves.SetActive(false);
            PoolSet.TryApplyToFirst<SDE.GamePool.GamePool>(pool =>
            {
                pool.Spawn(LeavesExplosionParticles, leaves.transform.position);
            });
        }

    }

    public void OnSpawned()
    {
        foreach (GameObject leaves in Leaves)
            leaves.SetActive(true);

        foreach (RagdollActivator Log in Logs)
        {
            Log.ResetActivator();
            Log.gameObject.SetActive(true);
        }

        this._health.ResetHealth();

        this._collider.enabled = true;
        this.Animator.SetTrigger("grow");
    }

    public void OnCreated()
    {
        this._collider = GetComponent<Collider>();
        this._health = GetComponent<Health>();
        this._health.OnDied += Destroyed;
    }
}
