using UnityEngine;

public class DataParser {
    public void Parse(string data)
    {
        ActionData actionData = JsonUtility.FromJson<ActionData>(data);

        if (actionData.type == "ChunkBlockData")
        {

        }
    }
}