using System.Collections.Generic;
using UnityEngine;
using SDE;

public class AudioSourceDetails
{
    public AudioSource Source;
    public float OriginalPitch;

    public bool Play(float minOffset, float maxOffset)
    {
        if (Source.isPlaying) return false;

        Source.pitch = this.GetPitch(minOffset, maxOffset);
        Source.Play();
        return true;
    }

    public void Stop()
    {
        Source.Stop();
    }

    private float GetPitch(float offsetMin, float offsetMax)
    {
        return Random.Range(OriginalPitch - offsetMin, OriginalPitch + offsetMax);
    }
}


[RequireComponent(typeof(AudioSource))]
public class AudioPlayerElement : MonoBehaviour
{
    public AudioClip Clip => this._sources.Peek().Source.clip;
    public bool IsPlaying => this._sources.Peek().Source.isPlaying;
    public float OffsetLeft;
    public float OffsetRight;

    private Queue<AudioSourceDetails> _sources;

    // Start is called before the first frame update
    void Awake()
    {
        this._sources = new Queue<AudioSourceDetails>();
        AudioSource[] sources = GetComponents<AudioSource>();
        foreach(AudioSource source in sources)
        {
            source.playOnAwake = false;
            this._sources.Enqueue(new AudioSourceDetails()
            {
                Source = source,
                OriginalPitch = source.pitch
            });
        }
            
    }

    public void Play()
    {
        int count = 0;
        while(count++ <= this._sources.Count)
        {
            AudioSourceDetails source = this._sources.DequeueAndEnqueueToBack();
            if (source.Play(OffsetLeft, OffsetRight))
                return;
        }
    }

    public void Stop()
    {
        foreach(var source in this._sources)
            source.Stop();
    }
}
