using UnityEngine;
using System.Net.Sockets;
using System.IO;
using System.Threading.Tasks;
using System.Threading;


[CreateAssetMenu(fileName="Twitch Client", menuName="Twitcher/Twitch/Client")]
public class TwitchClient : ScriptableObject
{
    public enum ConnectionStatus {
        INCORRECT_USERNAME_PASSWORD,
        CONNECTION_ISSUES,
        OK
    }

    public struct TwitchCmd {
        public string Cmd;
        public string User;
    }

    static readonly string ENDPOINT = "irc.chat.twitch.tv";
    static readonly int PORT = 6667;

    private static readonly string[] INCORRECT_PASSWORD_VALIDATION = new string[] { "Null", "Improperly formatted", "Invalid NICK" };

    private string _username;
    private string _password;
    private string _channel;

    private TcpClient _client;
    private StreamReader _reader;
    private StreamWriter _writer; 

    public async Task<TwitchCmd?> ReadLatestCommand() {
        try {
            if(this._client.Available <= 0) return null;

            string response = await _reader.ReadLineAsync();

            if(response.Contains("PING"))
            {
                this._writer.WriteLine("PONG tmi.twitch.tv\r\n");
                this._writer.Flush();
                Debug.Log("PONGED!");
                return null;
            }

            if(!response.Contains("PRIVMSG")) return null;
            
            int msgIndex = response.IndexOf(":", 1); 
            string message = response.Substring(msgIndex + 1);

            if(!message.StartsWith("!")) return null;
            string command = message.Substring(1);

            int nameIndex = response.IndexOf("!", 1);
            string username = response.Substring(0, nameIndex).Substring(1);

            return new TwitchCmd(){
                User = username,
                Cmd = command,
            };
        } catch {
            Debug.LogWarning("Twitch Client - Command Parsing - Something went wrong");
            return null;
        }
    }

    public CancellationTokenSource StartAsyncCommandListener(System.Action<TwitchCmd> response) {
        CancellationTokenSource source = new CancellationTokenSource();
        CancellationToken token = source.Token;

        Task.Run(async () => {
            if(!this._client.Connected) {
                Debug.LogError("Twitch Command Listener Not connected, termating async listner");
                return;
            }

            try {
                while(!token.IsCancellationRequested) {

                    if (!this._client.Connected)
                    {
                        Debug.LogWarning("[Twitch]: Not Connected! Attempting to connect!");
                        ConnectionStatus status = await this.Connect();
                        if(status != ConnectionStatus.OK)
                        {
                            Debug.LogError("[Twitch]: Failed to connect, Termating async listener");
                            break;
                        }
                    }

                    TwitchCmd? result = await ReadLatestCommand();
                    if(result == null) continue;
                    Debug.Log(result.Value);
                    response(result.Value);
                }
            } catch(System.Exception e) {
                Debug.LogError(e);
            }
                Debug.Log("Twitch Command Listener Terminated");

        }, token);

        return source;
    }

    public async Task<ConnectionStatus> Authorize(string username, string password, string channel) {
        this._username = username;
        this._password = password;
        this._channel = channel;

        return await this.Connect();
    }

    public async Task<ConnectionStatus> Connect() {          
        this._client = new TcpClient(ENDPOINT, PORT);
        this._reader = new StreamReader(this._client.GetStream());
        this._writer = new StreamWriter(this._client.GetStream());

        // NOTE: CAN'T CONFIRM IF THIS WORKS
        if(!this._client.Connected)
            return ConnectionStatus.CONNECTION_ISSUES;

        this._writer.WriteLine("PASS " + this._password);
        this._writer.WriteLine("NICK " + this._username);
        this._writer.WriteLine("USER " + this._username + " 8 * :" + this._username);
        this._writer.WriteLine("JOIN #" + this._channel);
        this._writer.Flush();

        string message = await this._reader.ReadLineAsync();

        ConnectionStatus status = this.ProcessConnectionStatus(message);
        return status;
    }

    public void Disconnect() {
        this._client.Dispose();
    }

    private ConnectionStatus ProcessConnectionStatus(string message) {
        Debug.Log("Connection Result: " + message);
        if(message != null && message.Contains("Welcome")) return ConnectionStatus.OK;
        return ConnectionStatus.INCORRECT_USERNAME_PASSWORD;
    }
}
