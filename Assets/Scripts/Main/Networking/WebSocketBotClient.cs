public class WebSocketBotClient
{
    public int Identity { get; }
    public string MCClientVersion { get; }
    public string Username { get; set; }
    public string ServerIP { get; set; }

    public WebSocketBotClient(int identity, string mcClientVersion, string username)
    {
        this.Identity = identity;
        this.MCClientVersion = mcClientVersion;
        this.Username = username;
    }
}
