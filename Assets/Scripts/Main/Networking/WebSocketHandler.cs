using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;
using WebSocketSharp;
using WebSocketSharp.Net;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

public class WebSocketHandler : MonoBehaviour
{
    [SerializeField] private MainController _mainController;
    [SerializeField] private MainUIHandler _mainUIHandler;

    private WebSocket _webSocket;
    private WebSocketConnector _connector;
    private Receiver _receiver;

    public WebSocketBotClients Clients { get; } = new WebSocketBotClients();

    private bool _connectionHasBeenEstablished;

    private string _connectionCloseReason;

    #region Unity Methods

    private void Start()
    {
        SetupWebSocket(URLHelper.GetWebSocketAddress(true), AppController.Instance.Account.SessionToken);
        _receiver = new Receiver(this);

        this._connector = new WebSocketConnector(this._webSocket);
        this._connector.ConnectAsync();
    }

    private void Update()
    {
        // Failed to connect to the server, return to login, as something might have gone wrong.
        if (this._connector.Status == WebSocketConnector.ConnectingStatus.Failed) LoginController.Load("Connection with the server timed out.");
        else if (this._connectionCloseReason != null) LoginController.Load(_connectionCloseReason);

        this._mainUIHandler.UpdateWebSocketConnectingUI(this._webSocket.ReadyState != WebSocketState.Open, this._connectionHasBeenEstablished);
        this._mainUIHandler.UpdateWebSocketReconnectingProgressText(this._connector.Attempts, WebSocketConnector.MAX_ATTEMPTS);
    }

    private void OnDestroy()
    {
        UnregisterEvents();
        _webSocket = null;
    }

    #endregion

    private void SetupWebSocket(string url, string sessionToken)
    {
        // Create the websocket client
        this._webSocket = new WebSocket(url);
        this._webSocket.EmitOnPing = true;
        this._webSocket.WaitTime = TimeSpan.FromSeconds(5);

        RegisterEvents();

        // Add the cookies
        this._webSocket.SetCookie(new Cookie("sessionToken", sessionToken));
    }

    private void Close()
    {
        this._webSocket.Close();
        this._webSocket = null;
    }

    private void RegisterEvents()
    {
        this._webSocket.OnOpen += OnOpen;
        this._webSocket.OnMessage += OnMessage;
        this._webSocket.OnError += OnError;
        this._webSocket.OnClose += OnClose;
    }

    private void UnregisterEvents()
    {
        this._webSocket.OnOpen -= OnOpen;
        this._webSocket.OnMessage -= OnMessage;
        this._webSocket.OnError -= OnError;
        this._webSocket.OnClose -= OnClose;
    }

    private void OnOpen(object sender, EventArgs e)
    {
        _connectionHasBeenEstablished = true;
    }

    private void OnMessage(object sender, MessageEventArgs e)
    {
        string json = e.Data;
        MessageEventDataWrapper wrapper = JsonConvert.DeserializeObject<MessageEventDataWrapper>(json);
        // TODO: Convert to map, set type, try to parse data object as string.
        // TODO: Or create a ChunkLoadMessageEventData

        MessageEventDataType type = wrapper.type;
        JObject data = wrapper.data;

        switch (type)
        {
            case MessageEventDataType.BotConnected:
                _receiver.OnBotConnectedDataReceived(data.ToObject<BotConnectedEventDataData>());
                break;
            case MessageEventDataType.BotDisconnected:
                _receiver.OnBotDisconnectedDataReceived(data.ToObject<BotDisconnectedEventDataData>());
                break;
            case MessageEventDataType.ChunkLoad:
                _receiver.OnChunkLoadDataReceived(data.ToObject<ChunkLoadMessageEventDataData>());
                break;
        }
    }

    private void OnError(object sender, ErrorEventArgs e)
    {

    }

    private void OnClose(object sender, CloseEventArgs e)
    {
        // If an account is still available, then the closure must not have been purposeful.
        if (!e.WasClean)
        {
            if (!this._connector.IsConnecting()) this._connector.Connect();
        }
        else
        {
            _connectionCloseReason = e.Reason;
        }
    }
}
