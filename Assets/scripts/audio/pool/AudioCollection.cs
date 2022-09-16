using UnityEngine;

[CreateAssetMenu(fileName ="Audio Collection", menuName ="Twitcher/Aud")]
public class AudioCollection : ScriptableObject
{
    public AudioClip[] Clips;
    public bool Translate = true;
    public bool RandomizeClip = true;
    public AudioSourceOptions Options;

    public AudioClip Clip => RandomizeClip ? RandomClip : FirstClip;

    public AudioClip RandomClip => Clips[Random.Range(0, Clips.Length)];
    public AudioClip FirstClip => Clips[0];
}
