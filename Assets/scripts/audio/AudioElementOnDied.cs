using UnityEngine;

[RequireComponent(typeof(AudioPlayerElement))]
public class AudioElementOnDied : MonoBehaviour
{
    public Health health;
    private AudioPlayerElement _element;

    // Start is called before the first frame update
    void Start()
    {
        this._element = GetComponent<AudioPlayerElement>();
        this.health.OnDied += () => this._element.Play();
    }
}
