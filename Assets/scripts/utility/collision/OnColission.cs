using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SDE;

public class OnColission : MonoBehaviour
{
    public event System.Action<Collision> OnCollided;

    private void OnDestroy()
    {
        OnCollided.RemoveAllListeners();
    }

    private void OnCollisionEnter(Collision other)
    {
        OnCollided?.Invoke(other);
    }

    private void OnCollisionExit(Collision other)
    {
        OnCollided?.Invoke(other);
    }
}
