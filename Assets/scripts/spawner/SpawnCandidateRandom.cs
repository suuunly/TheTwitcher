using UnityEngine;

[CreateAssetMenu(fileName = "Spawn Candiate", menuName = "Twitcher/Spawning/Candiate - Random")]
public class SpawnCandidateRandom : SpawnCandidateBase
{
    public GameObject[] Objects;

    public override GameObject GetItem()
    {
        return Objects[Random.Range(0, Objects.Length)];
    }
}
