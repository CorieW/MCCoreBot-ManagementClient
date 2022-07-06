using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ExtraBotListingUI : BotListingUI
{
    [Header("Settings")]
    [SerializeField] private Color _notConnectedToServerColor;
    [SerializeField] private Color _connectedToServerColor;

    [Header("References")]
    [SerializeField] private Text _connectedServerIPText;

    public void SetConnectedServerIP(string connectedServerIP)
    {
        connectedServerIP = connectedServerIP == null || connectedServerIP == "" ? "In menu" : connectedServerIP;

        // Set text color
        if (connectedServerIP == "") _connectedServerIPText.color = _notConnectedToServerColor;
        else _connectedServerIPText.color = _connectedToServerColor;

        _connectedServerIPText.text = "Server: " + connectedServerIP;
    }
}
