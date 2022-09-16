using UnityEngine;

public enum HerbType
{
    Undefined = 0,
    Red = 1,
    Green = 2
}

public class PickupHealth : PickupBase, SDE.GamePool.IPoolable
{
    public Animator Animator;
    public HerbType Herb;

    public void OnCreated()
    {
    }

    public void OnSpawned()
    {
        Animator.Rebind();
    }

    protected override object OnPickedUp()
    {
        return Herb;
    }
}
