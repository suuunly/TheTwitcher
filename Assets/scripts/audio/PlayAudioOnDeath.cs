using UnityEngine;

public class PlayAudioOnDeath : MonoBehaviour
{
    public AudioPoolPlayer Player;
    public Health Health;
    public AudioCollection Collection;

    private void Start()
    {
        Health.OnDied += () =>
        {
            Player.PlayFromCollection(Collection);
        };
    }
}
