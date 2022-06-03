using UnityEngine;

public abstract class ParsedData {
    protected string _type;

    public string GetActionType(string json)
    {
        return _type;
    }
}