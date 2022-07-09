using System.Threading;
using System.Collections.Generic;
using System.Collections.Concurrent;
using UnityEngine;

namespace Minecraft
{
    public abstract class AbstractChunkRenderer : TickingThread
    {
        public const int CHUNK_WIDTH_N_LENGTH = 16;

        protected ConcurrentQueue<Chunk> _in;

        public AbstractChunkRenderer() : base()
        {
            _in = new ConcurrentQueue<Chunk>();
        }

        protected override void Tick()
        {
            // Render any queued chunks
            if(!_in.IsEmpty)
            {
                if (_in.TryDequeue(out var nextRender))
                {
                    RenderChunk(nextRender);
                }
            }
        }

        private void RenderChunk(Chunk chunk)
        {
            // Generate the mesh data using the data from the chunk.
            ChunkMeshData chunkMeshData = GenerateChunkMeshData(chunk.BlockStates);
            // Send the data to the unity thread for mesh creation.
            SendMeshDataToMeshGenerator(chunk.ChunkPos, chunkMeshData);
        }

        protected abstract ChunkMeshData GenerateChunkMeshData(int[] blockStates);

        /// <summary>Sends the mesh data to the mesh generator.</summary>
        private void SendMeshDataToMeshGenerator(ChunkPos chunkPos, ChunkMeshData chunkMeshData)
        {
            ChunkMeshData tempChunkMeshData = chunkMeshData;
            Vector3 meshPos = new Vector3(chunkPos.x * CHUNK_WIDTH_N_LENGTH, 0, chunkPos.z * CHUNK_WIDTH_N_LENGTH);

            // If the MeshGenerator no longer exists, return.
            if (!MeshGenerator.Instance) return;

            MeshGenerator.Instance.GenerateMesh(meshPos, tempChunkMeshData.Vertices, tempChunkMeshData.Triangles, null);
        }

        /// <summary>Doesn't need to be performed on Unity thread.</summary>
        public void QueueChunk(Chunk chunk)
        {
            _in.Enqueue(chunk);
        }

        /// <summary>Holds the data that will be used to create the mesh.</summary>
        protected class ChunkMeshData
        {
            public Vector3[] Vertices { get; }
            public int[] Triangles { get; }
            public float[] UV { get; }

            public ChunkMeshData(Vector3[] vertices, int[] triangles, float[] uv)
            {
                this.Vertices = vertices;
                this.Triangles = triangles;
                this.UV = uv;
            }
        }
    }
}
