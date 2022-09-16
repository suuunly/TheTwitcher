using UnityEngine;

[RequireComponent(typeof(RagdollActivator), typeof(ItemReplacement))]
public class ReplaceItemOnRagdollActivation : MonoBehaviour
{
    public float MinDelayInSeconds = 1.0f;
    public float MaxDelayInSeconds = 3.0f;
    private ItemReplacement _replacer;


    private void Awake()
    {
        GetComponent<RagdollActivator>().OnActivated += this.OnActivated;
        this._replacer = GetComponent<ItemReplacement>();
    }

    private void OnActivated()
    {
        if (!gameObject.activeSelf) return;

        float delay = Random.Range(this.MinDelayInSeconds, this.MaxDelayInSeconds);

        StartCoroutine(SDE.Timing.GenericDelay(delay, () =>
            this._replacer.Replace()));
    }
}
