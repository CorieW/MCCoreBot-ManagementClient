using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Minecraft.v1_18_2
{
    [System.Serializable]
    public class Block : IBlock
    {
        public int id;
        public string displayName;
        public string name;
        public float hardness;
        public int minStateID;
        public int maxStateID;
        public List<BlockState> states;
        public List<Drop> drops;
        public bool diggable;
        public bool transparent;
        public int filterLight;
        public int emitLight;
        public string boundingBox;
        public int collsionShapeID;
        public string material;
        public Dictionary<string, bool> harvestTools; 
        public int defaultState;
        public float resistance;

        public class Drop
        {
            public int itemID;
            public float dropChance;
            public int[] stackSizeRange;
            public bool silkTouch;
        }
    }
}