using UnityEngine;
using UnityEngine.UI;

public class ControlsOptionsUI : MonoBehaviour
{
    public Toggle Controller;
    public Toggle Keyboard;
    public InputSettings Settings;

    private void Awake()
    {
        InputSettings.OnTypeSet += Refresh;
    }

    private void OnDestroy()
    {
        InputSettings.OnTypeSet -= Refresh;
    }

    public void SetKeyboard()
    {
        Settings.Set(InputType.Keyboard);
    }
    public void SetController()
    {
        Settings.Set(InputType.Controller);
    }

    private void Refresh(InputType type)
    {
        bool isController = type == InputType.Controller;
        Keyboard.SetIsOnWithoutNotify(!isController);
        Controller.SetIsOnWithoutNotify(isController);
    }
}
