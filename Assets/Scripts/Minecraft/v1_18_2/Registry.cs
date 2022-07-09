using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Minecraft.v1_18_2
{
    public class Registry : IRegistry
    {
        public static Registry Instance;

        // public StringToObjectResourceRegistry<BlockMaterial> MaterialsRegistry { get; private set; } = new StringToObjectResourceRegistry<BlockMaterial>();
        public SafeIntToObjectResourceRegistry<Block> BlocksRegistry { get; } = new SafeIntToObjectResourceRegistry<Block>();
        public SafeOneWayIntToObjectResourceRegistry<Block> BlockStatesRegistry { get; } = new SafeOneWayIntToObjectResourceRegistry<Block>();
        // public ResourceRegistry<EntityData> EntitiesRegistry = new ResourceRegistry<EntityData>();
        // public ResourceRegistry<ItemData> ItemsRegistry = new ResourceRegistry<ItemData>();

        public Registry()
        {
            Instance = this;
        }
    }
}
