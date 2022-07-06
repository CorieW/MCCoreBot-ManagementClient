using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainUIHandler : MonoBehaviour
{
    [SerializeField] private MainController _mainController;

    [Header("Components")]
    [SerializeField] private ConnectingScreen _connectingScreen;

    public void UpdateWebSocketConnectingUI(bool notAlive, bool reconnecting)
    {
        if (_connectingScreen.Reconnecting != reconnecting) _connectingScreen.Reconnecting = reconnecting;
        if (_connectingScreen.Enabled != notAlive) _connectingScreen.Enabled = notAlive;
    }

    public void UpdateWebSocketReconnectingProgressText(int reconnectAttempts, int maxReconnectAttempts)
    {
        if (_connectingScreen.ReconnectAttempts == reconnectAttempts && _connectingScreen.MaxReconnectAttempts == maxReconnectAttempts) return;
        _connectingScreen.UpdateReconnectingProgress(reconnectAttempts, maxReconnectAttempts);
        // _connectingScreen.SetConnectingText(_connectingScreen.GetReconnecting());
    }
}
