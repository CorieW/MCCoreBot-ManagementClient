using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Essentially a registry for a specific Minecraft client version.
/// <para>Holds the data that is relevant to a specific version of a Minecraft client.</para>
/// </summary>
public class MinecraftClient
{
    // Each of these will represent which Minecraft versions the management client support.
    private static Dictionary<MinecraftClientVersion, MinecraftClient> versionToClientDict = new Dictionary<MinecraftClientVersion, MinecraftClient>();

    private MinecraftClientVersion _version;
    private Registry _registry;

    public MinecraftClient(MinecraftClientVersion version)
    {
        this._version = version;
        versionToClientDict.Add(version, this);

        ResourceLoader resourceLoader = new ResourceLoader(version);
        resourceLoader.Load();
        _registry = resourceLoader.GetRegistry();
    }

    public Registry GetRegistry()
    {
        return _registry;
    }

    public static Dictionary<MinecraftClientVersion, MinecraftClient> GetVersionToClientDict()
    {
        return versionToClientDict;
    }
}
