using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayAudioOnCollisionExternal : MonoBehaviour
{
    public OnColission Collision;
    public AudioPoolPlayer Player;
    public AudioCollection Collection;

    private void Start()
    {
        Collision.OnCollided += OnCollided;
    }

    private void OnCollided(Collision obj)
    {
        Player.PlayFromCollection(Collection);
    }
}
