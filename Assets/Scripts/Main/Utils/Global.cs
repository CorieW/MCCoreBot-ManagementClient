using System.Collections.Generic;

public static class Global
{
    /// <summary>Returns whether or not the application is in production environment or not.</summary>
    public static bool ProdEnv { 
        get {
// If is built, then use production environment
#if UNITY_STANDALONE
            return false;
#endif
            return false;
        }
    }

    private static string _Version = "";
    public static string Version {
        get {
            if (_Version == "")
            {
                // Todo: Load from project settings.
                return "test";
            }
            return _Version;
        }
    }
}
