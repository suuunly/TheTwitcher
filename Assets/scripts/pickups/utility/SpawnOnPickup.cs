using UnityEngine;

[RequireComponent(typeof(PickupBase))]
public class SpawnOnPickup : MonoBehaviour
{
    public SDE.Data.RuntimeSet PoolSet;
    public GameObject ObjectToSpawn;

    private void Start()
    {
        GetComponent<PickupBase>().PickedUp += () =>
        {
            PoolSet.TryApplyToFirst<SDE.GamePool.GamePool>(pool =>
            {
                pool.Spawn(ObjectToSpawn, transform.position);
            });
        };
    }
}
