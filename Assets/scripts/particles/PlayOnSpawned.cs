using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SDE.GamePool;


[RequireComponent(typeof(ParticleSystem))]
public class PlayOnSpawned : MonoBehaviour, IPoolable
{
    ParticleSystem _system;

    void IPoolable.OnSpawned()
    {
        this._system.Simulate(0.0f, true, true);
        this._system.Play();
    }

    void IPoolable.OnCreated()
    {
        this._system = GetComponent<ParticleSystem>();
    }
}
