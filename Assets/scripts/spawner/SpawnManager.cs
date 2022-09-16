using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SpawnCell
{
    public Vector3 position;
}

public class SpawnManager : MonoBehaviour
{
    [Header("Spawning")]
    public SDE.Data.RuntimeSet PoolSet;
    public SpawnCandidateBase[] Candiates;

    [Header("Area")]
    public float Radius;
    public string AreaMask;
    public int CellFetchAttempts = 10;
    public LayerMask OverlapCheck;
    public float OverlapArea;
    public Vector3 OverlapOffset;

    private Dictionary<string, SpawnCandidateBase> _candiates;

    private void OnDrawGizmosSelected()
    {
        Vector3 pos = transform.position;
        Gizmos.DrawWireSphere(pos, Radius);

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(pos + OverlapOffset, OverlapArea);
    }

    private void Start()
    {
        this.GenerateSpawnCandiateLookup();
    }

    public bool TrySpawnCandiate(string commandId, string owner = "Unknown")
    {
        Vector3? pos = GetRandomAvailableCell();
        if (!pos.HasValue) return false;

        if (!this._candiates.ContainsKey(commandId)) return false;

        SpawnCandidateBase candiate = this._candiates[commandId];
        PoolSet.TryApplyToFirst<SDE.GamePool.GamePool>(pool =>
        {
            Quaternion rot = Quaternion.Euler(Vector3.up * Random.Range(0.0f, 360.0f));
            GameObject target = pool.Spawn(candiate.GetItem(), pos.Value + candiate.SpawnOffset, rot);

            SpawnResponse response = target.GetComponent<SpawnResponse>();
            if (response) response.Notify(commandId, owner);
        });

        return true;
    }

    private Vector3? GetRandomAvailableCell()
    {
        for(int i = 0; i< CellFetchAttempts; i++)
        {
            // Get Random Point inside Sphere which position is center, radius is maxDistance
            Vector3 randomPos = Random.insideUnitSphere * Radius + transform.position;

            NavMeshHit hit; // NavMesh Sampling Info Container

            // from randomPos find a nearest point on NavMesh surface in range of maxDistance
            NavMesh.SamplePosition(randomPos, out hit, Radius, NavMesh.GetAreaFromName(AreaMask));

            Vector3 pos = hit.position;
            Collider[] col = Physics.OverlapSphere(pos + OverlapOffset, OverlapArea, OverlapCheck);
            if (col.Length <= 0) return pos;
        }

        return null;
    }

    private void GenerateSpawnCandiateLookup()
    {
        this._candiates = new Dictionary<string, SpawnCandidateBase>();
        foreach (SpawnCandidateBase candiate in this.Candiates)
            this._candiates.Add(candiate.Command, candiate);
    }
}
