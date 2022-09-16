using UnityEngine;
using TMPro;

[RequireComponent(typeof(Animator))]
public class CommandElementUI : MonoBehaviour
{
    public TextMeshProUGUI Text;
    private Animator _animator;

    public string ID => Text.text;

    private void Start()
    {
        this._animator = GetComponent<Animator>();
    }

    public void Highlight()
    {
        this._animator.SetTrigger("select");
    }
}
