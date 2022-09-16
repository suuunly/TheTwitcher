using System.Collections;
using UnityEngine;

public class AudioCollectionPlayer : MonoBehaviour
{
    public AudioSource Source;
    public AudioCollection Collection;
    [Min(0)] public float DelayMinTime;
    [Min(0)] public float DelayMaxTime;

    private void Start()
    {
        //AudioSourceOptions.ApplyOptionsToSource(Source, Collection.Options);
    }

    private void OnDestroy()
    {
        Stop();
    }

    public void Play()
    {
        StopAllCoroutines();
        StartCoroutine(PlayRoutine());
    }

    public void Stop()
    {
        StopAllCoroutines();
        Source.Stop();
    }

    IEnumerator PlayRoutine()
    {
        while(enabled)
        {
            AudioClip clip = Collection.Clip;
            Source.clip = clip;
            Source.pitch = Collection.Options.RandomPitch;
            Source.Play();

            float time = Random.Range(DelayMinTime, DelayMaxTime);
            yield return new WaitForSeconds(time + clip.length);
        }
    }
}
