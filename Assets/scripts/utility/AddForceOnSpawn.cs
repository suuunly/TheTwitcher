using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class AddForceOnSpawn : MonoBehaviour, SDE.GamePool.IPoolable
{
    public Vector3 Force;
    private Rigidbody _body;

    public void OnCreated()
    {
        this._body = GetComponent<Rigidbody>();
    }

    public void OnSpawned()
    {
        this._body.AddForce(this.Force, ForceMode.Impulse);
    }
}
