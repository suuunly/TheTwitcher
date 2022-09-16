using UnityEngine;
using System.Collections.Generic;
using System;
using System.Threading;

public class TwitchCommandListener : MonoBehaviour
{
    public event Action<TwitchClient.TwitchCmd> OnCommandReceived;

    public TwitchClient Client;
    public SpawnManager Spawner;

    private CancellationTokenSource _process;

    private Stack<Action> _responses;

    private void Start() {
        this._responses = new Stack<Action>();

        Application.runInBackground = true;
        this._process = this.Client.StartAsyncCommandListener(InvokeResponse);
    }

    private void OnDestroy() {
        this._process.Cancel();
    }

    private void Update() {
        if(this._responses.Count > 0)
            this._responses.Pop()?.Invoke();
    }

    private void InvokeResponse(TwitchClient.TwitchCmd response) {
        this._responses.Push(() => OnCmdReceived(response));
    }

    private void OnCmdReceived(TwitchClient.TwitchCmd cmd) {
        bool result = Spawner.TrySpawnCandiate(cmd.Cmd, cmd.User);
        Debug.Log($"{cmd.User}: {cmd.Cmd} | {result}");
        if (result) OnCommandReceived?.Invoke(cmd);
    }
}
