using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class BotsMenuUI
{
    public static BotsMenuUI Instance;

    private MainUIHandler _handler;
    private Dictionary<int, BotListingUI> _clientIdentityToBotListingUI = new Dictionary<int, BotListingUI>();

    // The root element of the bot menu
    private VisualElement _root;
    private VisualElement _botsContentContainer;
    private Button _hideMenuButton;

    public BotsMenuUI(MainUIHandler handler)
    {
        Instance = this;
        this._handler = handler;

        _root = handler.Root.Q("botsMenu");
        _botsContentContainer = _root.Q("unity-content-container");
        _hideMenuButton = _root.Q<Button>("hideMenuButton");

        // Register button events
        _hideMenuButton.RegisterCallback<ClickEvent>(e => CloseMenu());
    }
    
    public void OpenMenu()
    {
        _root.style.display = DisplayStyle.Flex;
    }

    private void CloseMenu()
    {
        _root.style.display = DisplayStyle.None;
    }

    public void AddBotListing(WebSocketBotClient client)
    {
        // Ensuring that it's ran on the Unity thread.
        UnityThreadCommunicator.RunOnMainThread.Enqueue(() =>
        {
            // Get a copied version of the bot listing asset
            VisualElement newBotListingUI = _handler.BotListingAsset.CloneTree();
            // Add the bot listing to the menu's listings container
            _botsContentContainer.Add(newBotListingUI);
            // Add to the dictionary to keep track of the bot listing and to allow changes possibly later
            _clientIdentityToBotListingUI.Add(client.Identity, new BotListingUI(newBotListingUI, client));
        });
    }

    public void RemoveBotListing(int identity)
    {
        // Ensuring that it's ran on the Unity thread.
        UnityThreadCommunicator.RunOnMainThread.Enqueue(() =>
        {
            BotListingUI botListingUI = _clientIdentityToBotListingUI[identity];
            // Remove the bot's UI from the menu's listings container
            _botsContentContainer.Remove(botListingUI.VisualElement);
            // Remove from the dictionary
            _clientIdentityToBotListingUI.Remove(identity);
        });
    }

    private class BotListingUI
    {
        private VisualElement _visualElement;
        private WebSocketBotClient _client;

        private Label _usernameLabel;
        private Label _mcVersionLabel;
        private Label _serverLabel;
        private Button _botDetailsButton;
        private Button _goToBotButton;
        
        public VisualElement VisualElement { get { return _visualElement; } }
        public string Username { set { _usernameLabel.text = value; } }
        private string MCVersion { set { _mcVersionLabel.text = "Minecraft Version: " + value; } }
        public string Server {
            set {
                if (value == null || value == "")
                {
                    _serverLabel.text = "Server: Not connected";
                    _serverLabel.style.color = new Color(1, 0, 0);
                }
                else
                {
                    _serverLabel.text = "Server: " + value;
                    _serverLabel.style.color = new Color(0, 1, 0);
                }
            }
        }

        public BotListingUI(VisualElement visualElement, WebSocketBotClient client)
        {
            this._visualElement = visualElement;
            this._client = client;

            _usernameLabel = visualElement.Q<Label>("usernameLabel");
            _mcVersionLabel = visualElement.Q<Label>("mcVersionLabel");
            _serverLabel = visualElement.Q<Label>("serverLabel");
            _botDetailsButton = visualElement.Q<Button>("botDetailsButton");
            _goToBotButton = visualElement.Q<Button>("goToBotButton");

            // Set labels text
            Username = client.Username;
            MCVersion = client.MCClient.Version;
            Server = client.ServerIP;

            // Register button events
            _botDetailsButton.RegisterCallback<ClickEvent>(e => OpenBotDetails());
            _goToBotButton.RegisterCallback<ClickEvent>(e => GoToBot());
        }

        private void OpenBotDetails()
        {
            // Todo
        }

        private void GoToBot()
        {
            // Todo
        }
    }
}
