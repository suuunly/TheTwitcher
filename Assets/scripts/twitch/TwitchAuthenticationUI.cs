using UnityEngine;
using TMPro;
using System.Threading.Tasks;


public class TwitchAuthenticationUI : MonoBehaviour
{
    public TwitchClient Client;
    public TMP_InputField Username;
    public TMP_InputField Password;
    public TMP_InputField Channel;

    public Animator ErrorBox;
    public SceneLoader Loader;

    private void Start() {
        Username.text = PlayerPrefs.GetString("username");
        Channel.text = PlayerPrefs.GetString("channel");
        Password.text = PlayerPrefs.GetString("password");
    }
    public void Connect() {
        
        ErrorBox.SetBool("error", false);

        var task = Task.Run(() => Client.Authorize(Username.text, Password.text, Channel.text)).GetAwaiter();
        task.OnCompleted(() => {
            OnConnected(task.GetResult());
        });
    }

    private void OnConnected(TwitchClient.ConnectionStatus status) {
        Debug.Log(status);
        if(status == TwitchClient.ConnectionStatus.OK)
        {
            PlayerPrefs.SetString("username", Username.text);
            PlayerPrefs.SetString("channel", Channel.text);
            PlayerPrefs.SetString("password", Password.text);


            Loader.LoadScene("Map");
            return;
        }
        ErrorBox.SetBool("error", true);
    }
}
