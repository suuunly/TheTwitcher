using System.Collections.Generic;
using UnityEngine;

public class AudioPlayer : MonoBehaviour
{
    public GameObject ElementParent;
    private Dictionary<AudioClip, AudioPlayerElement> _lookup;

    private void Start()
    {
        AudioPlayerElement[] elements = this.ElementParent.GetComponentsInChildren<AudioPlayerElement>();
        this._lookup = new Dictionary<AudioClip, AudioPlayerElement>();
        foreach (AudioPlayerElement element in elements)
        {
            this._lookup.Add(element.Clip, element);
        }
    }

    public void PlayClip(AudioClip clip)
    {
        if (!this._lookup.ContainsKey(clip)) return;
        AudioPlayerElement element = this._lookup[clip];
        element.Play();
    }

    public void PlayElement(AudioPlayerElement element)
    {
        element.Play();
    }
}
