using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Registry
{
    public StringToObjectResourceRegistry<BlockMaterial> materialsRegistry = new StringToObjectResourceRegistry<BlockMaterial>();
    public IntToObjectResourceRegistry<Block> blocksRegistry = new IntToObjectResourceRegistry<Block>();
    // public ResourceRegistry<EntityData> EntitiesRegistry = new ResourceRegistry<EntityData>();
    // public ResourceRegistry<ItemData> ItemsRegistry = new ResourceRegistry<ItemData>();

    public StringToObjectResourceRegistry<BlockMaterial> GetMaterialsRegistry()
    {
        return materialsRegistry;
    }

    public IntToObjectResourceRegistry<Block> GetBlocksRegistry()
    {
        return blocksRegistry;
    }
}
