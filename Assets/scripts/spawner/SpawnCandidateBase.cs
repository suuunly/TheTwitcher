using UnityEngine;

public abstract class SpawnCandidateBase : ScriptableObject
{
    public string Command;

    public Vector3 SpawnOffset;

    public abstract GameObject GetItem();
}
