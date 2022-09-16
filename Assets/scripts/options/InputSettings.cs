using UnityEngine;

public enum InputType
{
    Controller,
    Keyboard
}

public class InputSettings : MonoBehaviour
{
    public static event System.Action<InputType> OnTypeSet;

    private void Start()
    {
        string type = PlayerPrefs.GetString("option_input", "Controller");
        OnTypeSet?.Invoke(type == "Controller" ? InputType.Controller : InputType.Keyboard);
    }

    public void Set(InputType type)
    {
        PlayerPrefs.SetString("option_input", type.ToString());
        OnTypeSet?.Invoke(type);
    }
}
