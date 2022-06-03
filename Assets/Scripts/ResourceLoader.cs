using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>Loads all of the resources into the registry.</summary>
public class ResourceLoader
{
    public const string DATA_FILE_PATH = "/data";

    private MinecraftClientVersion _version;
    private Registry _registry;

    public ResourceLoader(MinecraftClientVersion version)
    {
        this._version = version;
        this._registry = new Registry();
    }

    public void Load()
    {
        LoadBlockMaterials();
        LoadBlocks();
    }

    /// <summary>Loads all the block materials from the version's block materials JSON file into registry.</summary>
    private void LoadBlockMaterials()
    {
        string filePath = $"{Application.dataPath + DATA_FILE_PATH}/{_version.GetVersionID()}/block_materials.json";
        
        if (!File.Exists(filePath)) 
        {
            Debug.LogError("No data file for block materials.");
            return;
        }

        string text = File.ReadAllText(filePath);
        BlockMaterial[] mats = JsonHelper.FromJson<BlockMaterial>(text);
        foreach(BlockMaterial mat in mats)
        {
            _registry.materialsRegistry.Add(mat.type, mat);
            Debug.Log(mat.type + ": " + mat.pushReaction);
        }
    }

    private void LoadBlocks()
    {
        string filePath = $"{Application.dataPath + DATA_FILE_PATH}/{_version.GetVersionID()}/blocks.json";
        
        if (!File.Exists(filePath)) 
        {
            Debug.LogError("No data file for block materials.");
            return;
        }
        
        string text = File.ReadAllText(filePath);
        Block[] blocks = JsonHelper.FromJson<Block>(text);
        foreach(Block block in blocks)
        {
            _registry.blocksRegistry.Add(block);
        }
    }

    public Registry GetRegistry()
    {
        return _registry;
    }
}
