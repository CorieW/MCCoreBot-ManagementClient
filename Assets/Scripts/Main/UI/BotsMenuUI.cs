using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotsMenuUI : MonoBehaviour
{
    public static BotsMenuUI Instance;

    [Header("Settings")]
    [SerializeField] private Vector2 _extraListingStartPos = Vector2.zero;
    [SerializeField] private Vector2 _listingStartPos = Vector2.zero;
    [SerializeField] private Vector2 _extraListingGap = new Vector2(0, -110);
    [SerializeField] private Vector2 _listingGap = new Vector2(0, 80);

    [Header("References")]
    [SerializeField] private RectTransform _extraListingsContentContainerRectTransform;
    [SerializeField] private Transform _listingsContentContainerTransform;
    [SerializeField] private ExtraBotListingUI _extraListingUIPrefab;
    [SerializeField] private BotListingUI _listingUIPrefab;

    private Dictionary<int, ExtraBotListingUI> _identityToExtraBotListingDict = new Dictionary<int, ExtraBotListingUI>();
    private Dictionary<int, BotListingUI> _identityToBotListingDict = new Dictionary<int, BotListingUI>();

    private float _defaultExtraContentContainerHeight;

    #region Unity Methods

    private void Awake()
    {
        Instance = this;

        _defaultExtraContentContainerHeight = _extraListingsContentContainerRectTransform.sizeDelta.y;
    }

    #endregion

    /// <summary>Adds the bot client listing to the bots menu.</summary>
    public void AddBotListing(WebSocketBotClient botClient)
    {
        // Create regular listing
        BotListingUI newListing = Instantiate(_listingUIPrefab, Vector3.zero, Quaternion.identity);
        newListing.transform.SetParent(_listingsContentContainerTransform);

        newListing.SetUsername(botClient.Username);

        _identityToBotListingDict.Add(botClient.Identity, newListing);

        // Create extra detail listing in menu
        ExtraBotListingUI newExtraListing = Instantiate(_extraListingUIPrefab, Vector3.zero, Quaternion.identity);
        newExtraListing.transform.SetParent(_extraListingsContentContainerRectTransform);

        newExtraListing.SetUsername(botClient.Username);
        newExtraListing.SetConnectedServerIP(botClient.ServerIP);

        _identityToExtraBotListingDict.Add(botClient.Identity, newExtraListing);
        
        UpdateListingsUI();
    }

    /// <summary>Removes the bot client listing from the bots menu.</summary>
    public void RemoveBotListing(int identity)
    {
        // Remove regular listing
        BotListingUI listing = _identityToBotListingDict[identity];

        _identityToExtraBotListingDict.Remove(identity);
        Destroy(listing.gameObject);

        // Remove extra detail listing
        ExtraBotListingUI extraListing = _identityToExtraBotListingDict[identity];

        _identityToExtraBotListingDict.Remove(identity);
        Destroy(extraListing.gameObject);

        UpdateListingsUI();
    }

    public void UpdateBotListing(WebSocketBotClient botClient)
    {
        ExtraBotListingUI listing = _identityToExtraBotListingDict[botClient.Identity];

        listing.SetUsername(botClient.Username);
        listing.SetConnectedServerIP(botClient.ServerIP);
    }

    private void UpdateListingsUI()
    {
        int i = 0;
        foreach (BotListingUI listing in _identityToBotListingDict.Values)
        {
            listing.transform.localPosition = CalculateNextListingPosition(_listingStartPos, _listingGap, i);
            i++;
        }

        i = 0;
        foreach (ExtraBotListingUI listing in _identityToExtraBotListingDict.Values)
        {
            listing.transform.localPosition = CalculateNextListingPosition(_extraListingStartPos, _extraListingGap, i);
            i++;
        }

        _extraListingsContentContainerRectTransform.sizeDelta = new Vector2(_extraListingsContentContainerRectTransform.sizeDelta.x, CalculateNextContentContainerHeight());
    }

    /// <summary>Calculates the position for where the next bot listing should be positioned.</summary>
    private Vector3 CalculateNextListingPosition(Vector2 startPos, Vector2 gap, int index)
    {
        return startPos + (gap * index);
    }

    /// <summary>Calculate the size of the content container to fit the bot listings.</summary>
    private float CalculateNextContentContainerHeight()
    {
        float height = Mathf.Abs(_extraListingGap.y) * _identityToExtraBotListingDict.Count;
        
        return height > _defaultExtraContentContainerHeight ? height : _defaultExtraContentContainerHeight;
    }
}
