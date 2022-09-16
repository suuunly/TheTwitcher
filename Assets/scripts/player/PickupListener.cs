using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class PickupListener : MonoBehaviour
{
    private Collider _collider;

    private void Awake()
    {
        this._collider = GetComponent<Collider>();
        this._collider.isTrigger = true;
    }


    private void OnTriggerEnter(Collider other)
    {
        PickupBase pickup = other.GetComponent<PickupBase>();
        if (pickup) pickup.Pickup();
    }

}
