using UnityEngine;

public class PlayAudioOnDamage : MonoBehaviour
{
    public DamageApplier Damager;
    public AudioPoolPlayer Player;
    public AudioCollection Collection;

    // Start is called before the first frame update
    void Start()
    {
        Damager.OnAppliedDamage += () =>Player.PlayFromCollection(Collection);
    }
}
