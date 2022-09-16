using UnityEngine;

public class MenuPopupController : MonoBehaviour
{
    public MenuHandler Handler;

    private void Update()
    {
        if (Input.GetButtonDown("Options"))
        {
            if (Handler.IsPaused) Handler.Unpause();
            else Handler.Pause();
        }   
    }
}
