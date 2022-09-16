using UnityEngine;

[CreateAssetMenu(fileName = "Spawn Candiate", menuName = "Twitcher/Spawning/Candiate")]
public class SpawnCandidateSingular : SpawnCandidateBase
{
    public GameObject PoolItem;

    public override GameObject GetItem()
    {
        return this.PoolItem;
    }
}
