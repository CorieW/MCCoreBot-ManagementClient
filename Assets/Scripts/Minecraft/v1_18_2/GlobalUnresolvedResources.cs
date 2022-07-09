namespace Minecraft.v1_18_2
{
    public static class GlobalUnresolvedResources
    {
        public static Block UNRESOLVED_BLOCK {
            get {
                Block block = new Block();
                block.id = -1;
                block.displayName = "error";
                return block;
            }
        }
    }
}