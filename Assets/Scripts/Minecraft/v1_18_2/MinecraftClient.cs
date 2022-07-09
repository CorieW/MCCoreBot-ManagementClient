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
        public static MinecraftClient Instance;

        public string Version { get; }
        public IRegistry Registry { get; }
        public AbstractChunkRenderer ChunkRenderer { get; }

        public MinecraftClient()
        {
            Instance = this;

            this.Version = "1.18.2";

            ResourceLoader resourceLoader = new ResourceLoader(this.Version);
            resourceLoader.Load();
            Registry = resourceLoader.Registry;

            ChunkRenderer = new ChunkRenderer();
        }

    }
}