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
        IMinecraftClient mcClient = AppController.VersionToMCClientDict[e.mcVersion];
        WebSocketBotClient client = new WebSocketBotClient(e.identity, mcClient, e.mcUsername);
        _wsHandler.Clients.Add(client);
    }

    public void OnBotDisconnectedDataReceived(BotDisconnectedEventDataData e)
    {
        int identity = e.identity;
        _wsHandler.Clients.RemoveByIdentity(identity);
    }

    public void OnChunkLoadDataReceived(ChunkLoadMessageEventDataData e)
    {
        WebSocketBotClient botClient = _wsHandler.Clients.GetByIdentity(e.identity);
        botClient.MCClient.ChunkRenderer.QueueChunk(Chunk.Create(e));
    }
}
