using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BlockMaterial
{
    public string type;
    public bool isLiquid;
    public bool isSolid;
    public bool blocksMovement;
    public bool isOpaque;
    public bool flammable;
    public bool replaceable;
    public BlockPushReactionEnum pushReaction;

    public string GetString()
    {
        return $"type: {type}\nisLiquid: {isLiquid}\nisSolid: {isSolid}\nblocksMovement: {blocksMovement}\nisOpaque: {isOpaque}\nflammable: {flammable}\nreplaceable: {replaceable}\npushReaction: {pushReaction}";
    }
}