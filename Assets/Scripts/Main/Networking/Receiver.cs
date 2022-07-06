using UnityEngine;
using Minecraft;

public class Receiver
{
    private WebSocketHandler _wsHandler;

    public Receiver(WebSocketHandler wsHandler)
    {
        this._wsHandler = wsHandler;
    }

    public void OnBotConnectedDataReceived(BotConnectedEventDataData e)
    {
        // WebSocketBotClient client = new WebSocketBotClient(e.identity, e.mcVersion, e.mcUsername);

        for (int i = 0; i < 20; i++)
        {
            _wsHandler.Clients.Add(new WebSocketBotClient(e.identity + i, e.mcVersion, e.mcUsername));
        }

        // _wsHandler.Clients.Add(client);
    }

    public void OnBotDisconnectedDataReceived(BotDisconnectedEventDataData e)
    {
        int identity = e.identity;
        _wsHandler.Clients.RemoveByIdentity(identity);
    }

    public void OnChunkLoadDataReceived(ChunkLoadMessageEventDataData e)
    {
        Debug.Log(e.pos.ToString());
        Debug.Log(e.blockStates.Length);
    }
}
