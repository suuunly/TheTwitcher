using UnityEngine;
using TMPro;

public class SpawnResponseNameTag : SpawnResponse, SDE.GamePool.IPoolable
{
    public TextMeshPro Tag;
    public Health Health;

    private Camera _camera;

    public void OnCreated()
    {
        this._camera = Camera.main;
        Health.OnDied += OnDied;
    }

    public void OnSpawned()
    {
        enabled = true;
        Tag.gameObject.SetActive(true);
    }

    public override void Notify(string command, string owner)
    {
        Tag.text = owner;
    }

    private void LateUpdate()
    {
        Tag.transform.LookAt(this._camera.transform);
    }

    private void OnDied()
    {
        enabled = false;
        Tag.gameObject.SetActive(false);
    }




}
