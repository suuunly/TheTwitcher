using System.Collections.Generic;
using UnityEngine;
using SDE.Data;

public class AudioPoolPlayer : MonoBehaviour
{
    public RuntimeSet AudioPoolSet;
    public AudioSourceOptions Options;

    public void Play(AudioClip clip)
    {
        AudioPoolSet.TryApplyToFirst<AudioPool>(pool =>
        {
            pool.Play(clip, transform.position, Options);
        });
    }

    public void PlayWithoutTranslation(AudioClip clip)
    {
        AudioPoolSet.TryApplyToFirst<AudioPool>(pool =>
        {
            pool.Play(clip, null, Options);
        });
    }

    public AudioClip PlayFromCollection(AudioCollection collection)
    {
        AudioClip clip = null;
        AudioPoolSet.TryApplyToFirst<AudioPool>(pool =>
        {            
            Vector3? pos = null;
            if (collection.Translate) pos = transform.position;
            pool.Play(collection.Clip, pos, collection.Options);

            clip = collection.Clip;
        });

        return clip;
    }
}
