using UnityEngine;

public class MenuHandler : MonoBehaviour
{
    public static event System.Action<bool> OnPauseState;

    public Animator Animator;

    public bool IsPaused => Time.timeScale <= 0.1f;

    private void Awake()
    {
        Time.timeScale = 1.0f;
    }

    private void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.None;
        Cursor.lockState = CursorLockMode.Confined;
    }

    public void Pause()
    {
        Time.timeScale = 0.0f;
        ReactToStateChanged();
    }
    public void Unpause()
    {
        Time.timeScale = 1.0f;
        ReactToStateChanged();
    }

    private void ReactToStateChanged()
    {
        bool isPaused = this.IsPaused;

        Cursor.visible = isPaused;

        Cursor.lockState = CursorLockMode.None;
        if (!isPaused) Cursor.lockState = CursorLockMode.Confined;
        Animator.SetBool("show", isPaused);
        OnPauseState?.Invoke(isPaused);
    }
}
