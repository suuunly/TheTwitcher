using UnityEngine;

public abstract class SpawnResponse : MonoBehaviour
{
    public abstract void Notify(string command, string owner);
}
