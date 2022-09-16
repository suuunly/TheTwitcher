using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayAudioOnTriggerEnter : MonoBehaviour
{
    public AudioCollection Collection;
    public AudioPoolPlayer Player;

    private void OnTriggerEnter(Collider collision)
    {
        Player.PlayFromCollection(Collection);
    }
}
