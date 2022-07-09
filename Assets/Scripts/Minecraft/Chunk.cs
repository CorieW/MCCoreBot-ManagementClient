namespace Minecraft
{
    public class Chunk
    {
        public ChunkPos ChunkPos { get; set; }
        public int[] BlockStates { get; set; }

        private Chunk() { }

        public static Chunk Create(ChunkLoadMessageEventDataData data)
        {
            Chunk chunk = new Chunk();
            chunk.ChunkPos = data.pos;
            chunk.BlockStates = data.blockStates;
            return chunk;
        }
    }
}