using UnityEngine;
using System.Collections.Generic;

public class CommandUI : MonoBehaviour
{
    public TwitchCommandListener Listener;

    public CommandElementUI[] Elements;

    private Dictionary<string, CommandElementUI> _elementMap;

    private void Start()
    {
        this._elementMap = new Dictionary<string, CommandElementUI>();
        foreach(CommandElementUI e in Elements)
            this._elementMap.Add(e.ID, e);

        Listener.OnCommandReceived += CmdReceived;
    }

    private void OnDestroy()
    {
        Listener.OnCommandReceived -= CmdReceived;
    }

    private void CmdReceived(TwitchClient.TwitchCmd cmd)
    {
        if (_elementMap.ContainsKey(cmd.Cmd))
            _elementMap[cmd.Cmd].Highlight();
    }
}
