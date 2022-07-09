using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class SimpleBotsMenuUI
{
    public static SimpleBotsMenuUI Instance;

    private MainUIHandler _handler;
    private BotsMenuUI _botsMenuUI;
    private Dictionary<int, SimpleBotListingUI> _clientIdentityToBotListingUI = new Dictionary<int, SimpleBotListingUI>();

    // The root element of the bot menu
    private VisualElement _root;
    private VisualElement _botsContentContainer;
    private Button _showMenuButton;

    public SimpleBotsMenuUI(MainUIHandler handler, BotsMenuUI botsMenuUI)
    {
        Instance = this;
        this._handler = handler;
        this._botsMenuUI = botsMenuUI;

        _root = handler.Root.Q("simpleBotsMenu");
        _botsContentContainer = _root.Q("unity-content-container");
        _showMenuButton = _root.Q<Button>("showMenuButton");

        // Register button events
        _showMenuButton.RegisterCallback<ClickEvent>(e => _botsMenuUI.OpenMenu());
    }

    public void AddBotListing(WebSocketBotClient client)
    {
        // Ensuring that it's ran on the Unity thread.
        UnityThreadCommunicator.RunOnMainThread.Enqueue(() =>
        {
            // Get a copied version of the bot listing asset
            VisualElement newBotListingUI = _handler.SimpleBotListingAsset.CloneTree();
            // Add the bot listing to the menu's listings container
            _botsContentContainer.Add(newBotListingUI);
            // Add to the dictionary to keep track of the bot listing and to allow changes possibly later
            _clientIdentityToBotListingUI.Add(client.Identity, new SimpleBotListingUI(newBotListingUI, client));
        });
    }

    public void RemoveBotListing(int identity)
    {
        // Ensuring that it's ran on the Unity thread.
        UnityThreadCommunicator.RunOnMainThread.Enqueue(() =>
        {
            SimpleBotListingUI botListingUI = _clientIdentityToBotListingUI[identity];
            // Remove the bot's UI from the menu's listings container
            _botsContentContainer.Remove(botListingUI.VisualElement);
            // Remove from the dictionary
            _clientIdentityToBotListingUI.Remove(identity);
        });
    }

    public class SimpleBotListingUI
    {
        private VisualElement _visualElement;
        private WebSocketBotClient _client;

        private VisualElement _skinImage;

        public VisualElement VisualElement { get { return _visualElement; } }

        public SimpleBotListingUI(VisualElement visualElement, WebSocketBotClient client)
        {
            this._visualElement = visualElement;
            this._client = client;

            _skinImage = visualElement.Q("skinImage");
        }
    }
}
