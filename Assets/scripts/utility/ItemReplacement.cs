using UnityEngine;

public class ItemReplacement : MonoBehaviour
{
    public GameObject Replacement;
    public SDE.Data.RuntimeSet PoolSet;

    public void Replace()
    {
        PoolSet.TryApplyToFirst<SDE.GamePool.GamePool>(pool =>
        {
            gameObject.SetActive(false);
            GameObject obj = pool.Spawn(Replacement, transform.position, transform.rotation);
        });
    }
}
