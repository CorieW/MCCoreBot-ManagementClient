namespace Minecraft
{
    public struct ChunkPos
    {
        public int x;
        public int z;

        public ChunkPos(int x, int z)
        {
            this.x = x;
            this.z = z;
        }

        public override string ToString()  
        {  
            return $"[x:{this.x}, z:{this.z}]";
        }  
    }
}