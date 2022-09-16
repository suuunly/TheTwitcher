using UnityEngine;

public class HealthUI : MonoBehaviour
{
    public GameObject HeartPrefab;
    public Health Health;

    private GameObject[] _hearts;

    private void Start()
    {
        this.Health.OnDamageTaken += OnTakenDamage;
        this.Health.OnDied += OnTakenDamage;
        this.Health.OnHealed += OnHealed;

        this._hearts = new GameObject[Health.MaxHealth];

        for (int i = 0; i < this._hearts.Length; i++)
        {
            GameObject go = Instantiate(HeartPrefab);
            go.transform.SetParent(transform);
            this._hearts[i] = go;
        }
    }

    void OnTakenDamage()
    {
        RenderHearts();
    }

    void OnHealed()
    {
        RenderHearts();
    }

    void RenderHearts()
    {
        for (int i = 0; i < this._hearts.Length; i++)
        {
            this._hearts[i].SetActive(i + 1 <= Health.CurrentHealth);
        }
    }
}
