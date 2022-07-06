using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Minecraft.v1_18_2
{
    public class Registry : IRegistry
    {
        // public StringToObjectResourceRegistry<BlockMaterial> MaterialsRegistry { get; private set; } = new StringToObjectResourceRegistry<BlockMaterial>();
        public IntToObjectResourceRegistry<Block> BlocksRegistry { get; } = new IntToObjectResourceRegistry<Block>();
        public Dictionary<int, Block> BlockStatesRegistry { get; } = new Dictionary<int, Block>();
        // public ResourceRegistry<EntityData> EntitiesRegistry = new ResourceRegistry<EntityData>();
        // public ResourceRegistry<ItemData> ItemsRegistry = new ResourceRegistry<ItemData>();
    }
}
