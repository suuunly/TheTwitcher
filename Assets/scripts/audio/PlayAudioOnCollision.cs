using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayAudioOnCollision : MonoBehaviour
{
    public OnColission Collision;
    public AudioCollectionPlayer Player;
    public AudioSource Source;

    private void Start()
    {
        Collision.OnCollided += OnCollided;
    }

    private void OnCollided(Collision obj)
    {
        Player.Play();
    }
}
