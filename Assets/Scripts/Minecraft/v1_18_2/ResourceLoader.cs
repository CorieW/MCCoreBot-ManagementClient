using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Minecraft.v1_18_2
{
    /// <summary>Loads all of the resources into the registry.</summary>
    public class ResourceLoader : IResourceLoader
    {
        public const string DATA_FILE_PATH = "/data";

        public string Version { get; }
        public Registry Registry { get; }

        public ResourceLoader(string version)
        {
            this.Version = version;
            this.Registry = new Registry();
        }

        public void Load()
        {
            LoadBlocks();
            LoadBlockStates();
        }

        private void LoadBlocks()
        {
            // Deserializes the blocks JSON file into many Block objects.
            string filePath = $"{Application.dataPath + DATA_FILE_PATH}/{Version}/blocks.json";
            
            if (!File.Exists(filePath)) 
            {
                Debug.LogError("No data file for blocks.");
                return;
            }
            
            string json = File.ReadAllText(filePath);
            // Block[] blocks = JsonHelper.FromJson<Block>(text);
            Block[] blocks = JsonConvert.DeserializeObject<Block[]>(json);
            foreach(Block block in blocks)
            {
                Registry.BlocksRegistry.Add(block);
            }

            // This is added for incase a block blockstate can't be resolved
            // Essentially an error block
            Registry.BlocksRegistry.SetSafeReturnObject(GlobalUnresolvedResources.UNRESOLVED_BLOCK);
        }

        private void LoadBlockStates()
        {
            // Will pull the min and max block state IDs from each Block.
            // Then it will associate the Block with each block state ID between
            // the pulled min and max block state IDs.
            for (int i = 0; i < Registry.BlocksRegistry.Count; i++)
            {
                Block block = Registry.BlocksRegistry.GetObjectFor(i);
                for (int blockStateID = block.minStateID; blockStateID <= block.maxStateID; blockStateID++)
                {
                    Registry.BlockStatesRegistry.Add(blockStateID, block);
                }
            }
            
            Registry.BlockStatesRegistry.SetSafeReturnObject(GlobalUnresolvedResources.UNRESOLVED_BLOCK);
        }
    }
}