using UnityEngine;
using SDE;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Collider))]
public class RagdollActivator : MonoBehaviour
{
    public event System.Action OnActivated;

    public Rigidbody Body { get; private set; }

    private Collider _collider;
    private Vector3 _origLocalPos;
    private Quaternion _origLocalRot;

    private void Awake()
    {
        this.Body = GetComponent<Rigidbody>();
        this._collider = GetComponent<Collider>();

        this.Body.isKinematic = true;
        this._collider.enabled = false;

        this._origLocalPos = transform.localPosition;
        this._origLocalRot = transform.localRotation;
    }

    private void OnDestroy()
    {
        OnActivated.RemoveAllListeners();
    }

    public void Activate()
    {
        this.Body.isKinematic = false;
        this._collider.enabled = true;

        this.OnActivated?.Invoke();
    }

    public void ResetActivator()
    {
        this.Body.isKinematic = true;
        this.Body.velocity = Vector3.zero;
        this.Body.angularVelocity = Vector3.zero;

        this._collider.enabled = false;

        transform.localPosition = this._origLocalPos;
        transform.localRotation = this._origLocalRot;
    }
}
