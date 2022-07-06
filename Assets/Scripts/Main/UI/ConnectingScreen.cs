using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using WebSocketSharp;

public class ConnectingScreen : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Image _background;
    [SerializeField] private Text _connectingText;
    [SerializeField] private LoadingScreen _loadingScreen;

    private string _reconnectingProgressText;

    private bool _enabled;
    public bool Enabled {
        get { return _enabled; }
        set {
            _enabled = value;

            this._background.gameObject.SetActive(value);
            this._loadingScreen.gameObject.SetActive(value);
            this._connectingText.gameObject.SetActive(value);

            if (value)
            {
                this._loadingScreen.FadeScreenEnabled = false;
                this._loadingScreen.SetLoadingScreenVisibility(true);
            }
        }
    }

    private bool _reconnecting;
    public bool Reconnecting {
        get { return _reconnecting; }
        set {
            _reconnecting = value;

            this._connectingText.text = value ? $"Reconnecting\n{this._reconnectingProgressText}" : "Bear with me for just a moment, I'm trying to\nconnect to some servers.";
        }
    }

    public int ReconnectAttempts { get; private set; }
    public int MaxReconnectAttempts { get; private set; }

    public void UpdateReconnectingProgress(int reconnectAttempts, int maxReconnectAttempts)
    {
        this.ReconnectAttempts = reconnectAttempts;
        this.MaxReconnectAttempts = maxReconnectAttempts;
        this._reconnectingProgressText = $"(Attempt {this.ReconnectAttempts} / {this.MaxReconnectAttempts})";   
        this._connectingText.text = $"Reconnecting\n{this._reconnectingProgressText}";
    }
}