using System.Collections;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using UnityEngine;
using UnityEngine.SceneManagement;
using WebSocketSharp;

public class AppController : MonoBehaviour
{
    public static AppController Instance;

    [Header("Scenes")]
    [SerializeField] private string _loginSceneName;
    [SerializeField] private string _mainSceneName;

    private string _sessionToken;
    private WebSocket _socket;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        Instance = this;

        // Retrieve Minecraft data
        new MinecraftClient(new MinecraftClientVersion("1.16.4"));
    }

    private void OnDestroy()
    {
        _socket.Close();
    }

    /// <summary>
    /// Sends a login request to the RESTful API.
    /// <para>If successful, a session token will be returned.</para>
    /// </summary>
    public void Login(string username, string password)
    {
        HttpClient client = new HttpClient();

        var values = new Dictionary<string, string>
        {
            { "username", username },
            { "password", password }
        };
        HttpContent content = new FormUrlEncodedContent(values);

        HttpResponseMessage response = client.PostAsync(GlobalValues.GetAPIAddress() + "/login", content).Result;
        // Check for session token return
        // If there was a session token returned, store it, open server connection, and open main scene.
        // Otherwise, failed login.
    }

    /// <summary>
    /// Logs the user out.
    /// </summary>
    public void Logout()
    {
        _socket.Close();
        _sessionToken = "";
        SceneManager.LoadScene(_loginSceneName);
    }

    /// <summary>
    /// Connects to the server websockets so data can be received from and sent to and the bot clients.
    /// <para>Uses the session token to connect to the websocket.</para>
    /// </summary>
    private void OpenServerConnection()
    {
        // Session token is required.
        if (_sessionToken == "") return;

        _socket = new WebSocket(GlobalValues.GetWebSocketAddress() + "?clientType=management");
        _socket.Connect();
        _socket.OnMessage += (sender, data) =>
        {
            SocketMessageReceived((WebSocket)sender, data);
        };
    }

    private void SocketMessageReceived(WebSocket sender, MessageEventArgs data)
    {
        
    }
}
