using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Minecraft.v1_18_2
{
    /// <summary>
    /// Essentially a registry for a specific Minecraft client version.
    /// <para>Holds the data that is relevant to a specific version of Minecraft.</para>
    /// </summary>
    public class MinecraftClient : IMinecraftClient
    {
        public string Version { get; }
        public Registry Registry { get; }
        public ChunkRenderer ChunkRenderer { get; }

        public MinecraftClient()
        {
            this.Version = "1.18.2";

            ResourceLoader resourceLoader = new ResourceLoader(this.Version);
            resourceLoader.Load();
            Registry = resourceLoader.Registry;

            ChunkRenderer = new ChunkRenderer();
        }

    }
}