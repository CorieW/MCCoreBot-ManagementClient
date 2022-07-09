using System.Collections.Generic;
using UnityEngine;

/// <summary>Wraps around the WebSocketBotClient list to provide custom functionality when adding a WebSocketBotClient.</summary>
public class WebSocketBotClients
{
    private List<WebSocketBotClient> _clients = new List<WebSocketBotClient>();

    public int Count { get => _clients.Count; }

    public void Add(WebSocketBotClient client)
    {
        int numOfSameUsernames = 0;
        foreach (WebSocketBotClient connectedClient in _clients)
        {
            // Multiple of the same identities can't exist.
            if (client.Identity == connectedClient.Identity) return;

            // Provide a unique username.
            if (client.Username == connectedClient.Username) numOfSameUsernames += 1;
            if (connectedClient.Username.Contains(client.Username + " ")) numOfSameUsernames += 1;
        }

        client.Username += numOfSameUsernames == 0 ? "" : " (" + numOfSameUsernames + ")";

        _clients.Add(client);

        // Handle UI changes
        SimpleBotsMenuUI.Instance.AddBotListing(client);
        BotsMenuUI.Instance.AddBotListing(client);
    }

    public void RemoveByIdentity(int identity)
    {
        for (int i = 0; i < Count; i++)
        {
            if (_clients[i].Identity == identity) 
            {
                _clients.RemoveAt(i);
                return;
            }
        }

        // Handle UI changes
        SimpleBotsMenuUI.Instance.RemoveBotListing(identity);
        BotsMenuUI.Instance.RemoveBotListing(identity);
    }

    public WebSocketBotClient GetByIdentity(int identity)
    {
        foreach (WebSocketBotClient client in _clients)
        {
            if (client.Identity == identity) return client;
        }
        return null;
    }
}
