using UnityEngine;
using SDE;

public abstract class PickupBase : MonoBehaviour
{
    public event System.Action PickedUp;
    public GameEventPure Event;

    public void Pickup()
    {
        gameObject.SetActive(false);
        Event?.Raise(this.OnPickedUp());

        this.PickedUp?.Invoke();
    }

    private void OnDestroy()
    {
        this.PickedUp?.Invoke(); // ???
        this.PickedUp.RemoveAllListeners();
    }

    protected abstract object OnPickedUp();
}
