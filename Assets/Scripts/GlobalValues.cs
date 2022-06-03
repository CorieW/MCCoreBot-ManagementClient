public static class GlobalValues
{
    /// <summary>Returns whether or not the application is in production environment or not.</summary>
    private static bool IsProdEnv()
    {
        bool prodEnv = false;

// If is built, then use production environment
#if UNITY_STANDALONE
        prodEnv = true;
#endif

        return prodEnv;
    }

    public static string GetDomain()
    {
        return IsProdEnv() ? "url.com" : "localhost:3000";
    }

    public static string GetAddress()
    {
        return IsProdEnv() ? $"https://{GetDomain()}/" : $"{GetDomain()}";
    }

    public static string GetAPIAddress()
    {
        return IsProdEnv() ? $"https://api.{GetDomain()}/" : $"{GetDomain()}/api";
    }

    public static string GetWebSocketAddress()
    {
        return IsProdEnv() ? $"ws://{GetDomain()}/socket" : $"ws://localhost:8080/socket";
    }
}
