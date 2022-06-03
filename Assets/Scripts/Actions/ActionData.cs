using UnityEngine;

public abstract class ActionData {
    public string type;

    /// <summary>
    /// Returns a the object as a string in the form of JSON.
    /// </summary>
    public string ToJson()
    {
        return JsonUtility.ToJson(this);
    }
}