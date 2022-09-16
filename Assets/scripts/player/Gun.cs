using UnityEngine;

public class Gun : MonoBehaviour
{
    public event System.Action AmmoStatus;

    public GameEventPure AmmoPickupListener;
    public GameObject Projectile;
    public SDE.Data.RuntimeSet PoolSet;

    public Transform ProjectilePoint;
    public GameObject VisualAmmo;
    public ParticleSystem SmokeEffect;

    public float FireRate = 1.0f;
    public int AmmoLeft = 15;

    [Header("Boost")]
    public GameEventPure Boost;
    public GameEventPure Unboost;
    public float FireRateReduction;


    private float _lastTime = 0.0f;
    private bool _canShoot = true;

    private float _fireRateReduction = 0.0f;

    public bool CanShoot => VisualAmmo.activeSelf;

    private void Start()
    {
        this.AmmoPickupListener.OnRaised += this.OnAmmoIncrease;
        this.Boost.OnRaised += this.OnBoosted;
        this.Unboost.OnRaised += this.OnUnboosted;

        this.AmmoStatus?.Invoke();
    }

    private void OnDestroy()
    {
        this.Boost.OnRaised -= this.OnBoosted;
        this.Unboost.OnRaised -= this.OnUnboosted;
    }

    private void OnBoosted(object data)
    {
        this._fireRateReduction = this.FireRateReduction;
    }

    private void OnUnboosted(object data)
    {
        this._fireRateReduction = 0.0f;
    }

    private void OnAmmoIncrease(object data)
    {
        int ammo = (int)data;
        this.AmmoLeft += Mathf.Abs(ammo);

        Debug.Log(this.AmmoLeft);

        AmmoStatus?.Invoke();
    }

    // Update is called once per frame
    void Update()
    {
        bool canShoot = AmmoLeft > 0 && Time.time - this._lastTime > FireRate - this._fireRateReduction;
        VisualAmmo.SetActive(canShoot);
    }

    public bool Shoot()
    {
        if (!this.CanShoot) return false;

        Transform t = ProjectilePoint;
        PoolSet.TryApplyToFirst<SDE.GamePool.GamePool>(pool =>
            pool.Spawn(Projectile, t.position, t.rotation));

        AmmoLeft = Mathf.Max(AmmoLeft - 1, 0);
        this._lastTime = Time.time;

        VisualAmmo.SetActive(false);

        // particles
        SmokeEffect.Simulate(0.0f, true, true);
        SmokeEffect.Play();

        AmmoStatus?.Invoke();
        return true;
    }
}
