using System.Threading;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using WebSocketSharp;
using WebSocketSharp.Net;

public class WebSocketConnector
{
    public const int MAX_ATTEMPTS = 10;

    private WebSocket _ws;

    public ConnectingStatus Status { get; private set; } = ConnectingStatus.None;
    public int Attempts { get; private set; }

    public enum ConnectingStatus
    {
        None, Connecting, Connected, Failed
    }

    public WebSocketConnector(WebSocket ws)
    {
        if (ws == null) Debug.LogError("Connector requires a WebSocket to work.");

        this._ws = ws;
    }

    public async void ConnectAsync()
    {
        await Task.Run(() => {
            Connect();
        });
    }

    public void Connect()
    {
        if (IsConnecting())
        {
            Debug.LogError("Connector is already attempting to connect to the server.");
            return;
        }

        Status = ConnectingStatus.Connecting;
        
        Attempts = 0;
        while (!_ws.IsAlive && Attempts < MAX_ATTEMPTS)
        {
            ++Attempts;

            if (Attempts > 1) Thread.Sleep(5000);
            _ws.Connect();

            if (_ws.IsAlive) 
            {
                Status = ConnectingStatus.Connected;
                return;
            }
        }

        Status = ConnectingStatus.Failed;
    }

    public bool IsConnecting()
    {
        return Status == ConnectingStatus.Connecting;
    }
}
