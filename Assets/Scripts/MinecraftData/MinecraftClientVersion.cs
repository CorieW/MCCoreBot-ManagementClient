using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinecraftClientVersion
{
    private static List<MinecraftClientVersion> Versions = new List<MinecraftClientVersion>();

    private string _versionID;
    
    public MinecraftClientVersion(string versionID)
    {
        this._versionID = versionID;

        Versions.Add(this);
    }

    public string GetVersionID()
    {
        return _versionID;
    }

    public static List<MinecraftClientVersion> GetVersions()
    {
        return Versions;
    }
}
