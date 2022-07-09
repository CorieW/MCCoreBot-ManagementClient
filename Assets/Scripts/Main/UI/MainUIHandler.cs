using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

[RequireComponent(typeof(UIDocument))]
public class MainUIHandler : MonoBehaviour
{
    public static MainUIHandler Instance;

    [SerializeField] private MainController _mainController;

    [Header("Resources")]
    [SerializeField] private VisualTreeAsset _botListingAsset;
    [SerializeField] private VisualTreeAsset _simpleBotListingAsset;

    // UI component handlers
    private BotsMenuUI _botsMenuUI;
    private SimpleBotsMenuUI _simpleBotsMenuUI;

    private VisualElement _root;

    public VisualTreeAsset BotListingAsset { get { return _botListingAsset; } }
    public VisualTreeAsset SimpleBotListingAsset { get { return _simpleBotListingAsset; } }
    public VisualElement Root { get { return _root; } }


    private void Awake()
    {
        Instance = this;

        _root = GetComponent<UIDocument>().rootVisualElement;

        // Create UI component handlers
        _botsMenuUI = new BotsMenuUI(this);
        _simpleBotsMenuUI = new SimpleBotsMenuUI(this, _botsMenuUI);
    }

    public void UpdateWebSocketConnectingUI(bool notAlive, bool reconnecting)
    {
        // if (_connectingScreen.Reconnecting != reconnecting) _connectingScreen.Reconnecting = reconnecting;
        // if (_connectingScreen.Enabled != notAlive) _connectingScreen.Enabled = notAlive;
    }

    public void UpdateWebSocketReconnectingProgressText(int reconnectAttempts, int maxReconnectAttempts)
    {
        // if (_connectingScreen.ReconnectAttempts == reconnectAttempts && _connectingScreen.MaxReconnectAttempts == maxReconnectAttempts) return;
        // _connectingScreen.UpdateReconnectingProgress(reconnectAttempts, maxReconnectAttempts);
        // _connectingScreen.SetConnectingText(_connectingScreen.GetReconnecting());
    }
}
