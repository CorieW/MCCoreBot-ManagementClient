using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Minecraft
{
    public interface IMinecraftClient
    {
        public string Version { get; }
        public IRegistry Registry { get; }
        public AbstractChunkRenderer ChunkRenderer { get; }
    }
}