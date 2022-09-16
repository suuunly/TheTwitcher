using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class AudioSourceOptions
{
    [Header("Pitch")]
    public float pitch = 1.0f;
    public float leftOffset = 0.1f;
    public float rightOffset = 0.1f;

    [Header("Volume")]
    public float volume = 1.0f;

    [Header("3D")]
    public float spatialBlend = 1.0f;
    public float maxDistance = 2.0f;
    public float minDistance = 0.5f;

    public float RandomPitch => Random.Range(pitch - leftOffset, pitch + rightOffset);

    public static void ApplyOptionsToSource(AudioSource source, AudioSourceOptions options)
    {
        source.pitch = options.RandomPitch;
        source.volume = options.volume;
        source.spatialBlend = options.spatialBlend;
        source.maxDistance = options.maxDistance;
        source.minDistance = options.minDistance;
    }
}
