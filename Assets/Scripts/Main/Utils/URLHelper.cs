using System.Collections.Generic;

public static class URLHelper
{
    private static Dictionary<WebsiteRouteEnum, string> RouteToStringDict = new Dictionary<WebsiteRouteEnum, string>()
    {
        {WebsiteRouteEnum.Subscriptions, "subscriptions"}
    };

    public static string GetDomain()
    {
        return Global.ProdEnv ? "url.com" : "localhost:3000";
    }

    public static string GetAddress()
    {
        return Global.ProdEnv ? $"https://{GetDomain()}" : $"http://{GetDomain()}";
    }

    public static string GetAPIAddress()
    {
        return Global.ProdEnv ? $"https://api.{GetDomain()}" : $"http://{GetDomain()}/api";
    }

    public static string GetWebSocketAddress(bool includeQuery)
    {
        string url = Global.ProdEnv ? $"wss://{GetDomain()}/socket" : $"ws://localhost:8080/socket";
        if (includeQuery) url += $"?clientType=management&clientVersion={Global.Version}";
        return url;
    }

    public static string GetRouteStringOfRoute(WebsiteRouteEnum route)
    {
        return "/" + RouteToStringDict[route];
    }
}
