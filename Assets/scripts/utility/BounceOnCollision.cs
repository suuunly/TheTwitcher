using UnityEngine;

[RequireComponent(typeof(Rigidbody), typeof(OnColission))]
public class BounceOnCollision : MonoBehaviour
{
    public Vector3 BounceForce;
    private Rigidbody _body;

    private void Start()
    {
        this._body = GetComponent<Rigidbody>();
        GetComponent<OnColission>().OnCollided += (col) =>
        {
            this._body.AddForce(transform.up + BounceForce, ForceMode.Impulse);
        };
    }
}
