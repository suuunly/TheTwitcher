using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(Health), typeof(Collider), typeof(NavMeshAgent))]
public class EnemyController : MonoBehaviour, SDE.GamePool.IPoolable
{
    public Animator Animator;
    public AudioCollectionPlayer RoamingAudio;
    public RagdollActivator[] BodyParts;
    public ParticleSystem SpawningParticles;

    [Header("AI")]
    public SDE.Data.RuntimeSet Target;
    public Punching Punching;
    public float StrikingDistance = 10.0f;

    private Collider _collider;
    private Health _health;
    private NavMeshAgent _agent;
    private Transform _target = null;

    public bool CanReachTarget
    {
        get
        {
            Vector3 target = this._target.position;
            Vector3 pos = this.Punching.PunchingPoint.position;
            float dist = (target - pos).sqrMagnitude;
            return dist <= StrikingDistance;
        }

    }

    public void StartMobilizing()
    {
        enabled = this._target;
        if (enabled) MoveToTarget();
    }

    private void MoveToTarget()
    {
        this._agent.SetDestination(this._target.position);
    }

    private void AttackTarget()
    {
        if (this.Punching.Punch())
            this.Animator.SetTrigger("attack");
    }

    private void LateUpdate()
    {
        this.Animator.SetFloat("movement", this._agent.velocity.sqrMagnitude);

        bool canReach = this.CanReachTarget;
        this._agent.isStopped = canReach;
        if (this.CanReachTarget) this.AttackTarget();
        this.MoveToTarget();
    }

    private void OnDied()
    {
        RoamingAudio.Stop();

        _collider.enabled = false;
        Animator.enabled = false;
        foreach (RagdollActivator part in BodyParts)
            part.Activate();

        enabled = false;
        this._agent.isStopped = true;

        if (SpawningParticles.isPlaying)
            SpawningParticles.Stop();
    }

    private void OnDamageTaken()
    {
        Animator.SetTrigger("damaged");
    }

    public void OnSpawned()
    {
        this._health.ResetHealth();
        this._collider.enabled = true;
        foreach (RagdollActivator part in BodyParts)
        {
            part.gameObject.SetActive(true);
            part.ResetActivator();
        }

        this.Animator.enabled = true;
        this.Animator.Rebind();

        this.enabled = false;
        this._agent.isStopped = true;

        RoamingAudio.Play();
    }

    public void OnCreated()
    {
        this._collider = GetComponent<Collider>();
        this._health = GetComponent<Health>();
        this._agent = GetComponent<NavMeshAgent>();

        enabled = false;
        this._health.OnDamageTaken += this.OnDamageTaken;
        this._health.OnDied += this.OnDied;

        this.Target.TryApplyToFirst<SDE.GenericRuntimeSetter>(target =>
            this._target = target.transform
        );
    }
}
