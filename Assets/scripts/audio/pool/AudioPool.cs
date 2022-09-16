using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using SDE.Data;
using SDE;


public class AudioPool : MonoBehaviour, IRuntime
{
    public int PoolSize;
    public RuntimeSet Set;
    public AudioMixerGroup Mixer;

    private Queue<AudioSource> _sources;

    // Start is called before the first frame update
    void Awake()
    {
        this._sources = new Queue<AudioSource>();
        for (int i = 0; i < PoolSize; i++)
        {
            GameObject go = new GameObject("audio-source");
            go.transform.SetParent(transform);
            AudioSource source = go.AddComponent<AudioSource>();

            source.playOnAwake = false;
            source.outputAudioMixerGroup = Mixer;
            source.spatialBlend = 1.0f;

            this._sources.Enqueue(source);
        }
    }

    private void OnEnable() => Set.Add(this);
    private void OnDisable() => Set.Remove(this);

    public AudioSource Play(AudioClip clip, Vector3? position = null, AudioSourceOptions details = null)
    {
        AudioSource source = this._sources.DequeueAndEnqueueToBack();

        if(position != null) source.transform.position = position.Value;
        source.clip = clip;

        if(details != null)
            AudioSourceOptions.ApplyOptionsToSource(source, details);
        source.Play();
        return source;
    }
}
