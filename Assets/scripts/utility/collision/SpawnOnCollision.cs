using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(OnColission))]
public class SpawnOnCollision : MonoBehaviour
{
    public SDE.Data.RuntimeSet PoolSet;
    public GameObject PoolItem;

    private void Awake()
    {
        GetComponent<OnColission>().OnCollided += Spawn;
    }

    private void Spawn(Collision col)
    {
        PoolSet.TryApplyToFirst<SDE.GamePool.GamePool>(pool =>
        {
            pool.Spawn(PoolItem, transform.position, transform.rotation);
        });
    }
}
