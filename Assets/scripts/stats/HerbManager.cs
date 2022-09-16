using UnityEngine;
using SDE;

public class HerbManager : MonoBehaviour
{
    public event System.Action<int, int, HerbType> OnPickedUp;
    public event System.Action OnConsumed;

    public GameEventPure Listener;
    public Health Health;
    public int HealAmount;

    public int GreenAmount { get; private set; }
    public int RedAmount { get; private set; }

    // Start is called before the first frame update
    void Start()
    {
        GreenAmount = 0;
        RedAmount = 0;

        Listener.OnRaised += this.Process;
    }

    private void OnDestroy()
    {
        Listener.OnRaised -= this.Process;
        OnConsumed.RemoveAllListeners();
    }

    private void Process(object data)
    {
        HerbType type = (HerbType)data;

        switch (type)
        {
            case HerbType.Undefined:
                return;

            case HerbType.Green:
                GreenAmount++;
                break;

            case HerbType.Red:
                RedAmount++;
                break;
        }

        if (GreenAmount > 0 && RedAmount > 0)
        {
            this.Health.Heal(HealAmount);

            GreenAmount--;
            RedAmount--;
            OnConsumed?.Invoke();
        }

        this.OnPickedUp?.Invoke(GreenAmount, RedAmount, type);
    }

}
