using Minecraft;

public class WebSocketBotClient
{
    public int Identity { get; }
    public IMinecraftClient MCClient { get; }
    public string Username { get; set; }
    public string ServerIP { get; set; }

    public WebSocketBotClient(int identity, IMinecraftClient mcClient, string username)
    {
        this.Identity = identity;
        this.MCClient = mcClient;
        this.Username = username;
    }
}
