using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class ProjectileMovement : MonoBehaviour, SDE.GamePool.IPoolable
{
    public float ForwardForce;
    private Rigidbody mBody;

    public void OnCreated()
    {
        mBody = GetComponent<Rigidbody>();
    }

    public void OnSpawned()
    {
        mBody.velocity = Vector3.zero;
        mBody.angularVelocity = Vector3.zero;
        mBody.AddForce(transform.forward * ForwardForce, ForceMode.Impulse);
    }
}
