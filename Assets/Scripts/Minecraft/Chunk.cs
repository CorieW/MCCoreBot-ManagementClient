namespace Minecraft
{
    public class Chunk
    {
        public ChunkPos ChunkPos { get; set; }
        public int[] BlockStates { get; set; }

        public void InsertValues(ChunkLoadMessageEventDataData data)
        {
            this.ChunkPos = data.pos;
            this.BlockStates = data.blockStates;
        }
    }
}