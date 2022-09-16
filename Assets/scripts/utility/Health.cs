using UnityEngine;
using SDE;

public class Health : MonoBehaviour
{
    public event System.Action OnDamageTaken;
    public event System.Action OnHealed;
    public event System.Action OnDied;

    public int MaxHealth = 3;

    public int CurrentHealth { get; private set; }
    public bool IsDead => CurrentHealth <= 0;

    private bool _hasDied = false;

    private void Start()
    {
        ResetHealth();
    }

    private void OnDestroy()
    {
        this.OnDamageTaken.RemoveAllListeners();
        this.OnHealed.RemoveAllListeners();
        this.OnDied.RemoveAllListeners();
    }

    public void ResetHealth()
    {
        this.CurrentHealth = this.MaxHealth;
        this._hasDied = false;
    }

    public void Damage(int amount)
    {
        if (this._hasDied) return;

        this.CurrentHealth = Mathf.Max(this.CurrentHealth - amount, 0);
        if (this.IsDead)
        {
            this._hasDied = true;
            this.OnDied?.Invoke();
        }
        else this.OnDamageTaken?.Invoke();
    }

    public void Heal(int amount)
    {
        this._hasDied = false;
        CurrentHealth = Mathf.Min(CurrentHealth + amount, MaxHealth);
        OnHealed?.Invoke();
    }
}
