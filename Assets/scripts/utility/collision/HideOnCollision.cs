using UnityEngine;

[RequireComponent(typeof(OnColission))]
public class HideOnCollision : MonoBehaviour
{
    private void Awake()
    {
        GetComponent<OnColission>().OnCollided += (other) => gameObject.SetActive(false);
    }
}

