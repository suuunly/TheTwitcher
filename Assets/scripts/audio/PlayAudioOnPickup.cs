using UnityEngine;

public class PlayAudioOnPickup : MonoBehaviour
{
    public PickupBase Pickup;
    public AudioPoolPlayer Player;
    public AudioCollection Collection;
    // Start is called before the first frame update
    void Start()
    {
        Pickup.PickedUp += OnPickedUp;
    }

    private void OnPickedUp()
    {
        Player.PlayFromCollection(Collection);
    }
}
