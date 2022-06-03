using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StringToObjectResourceRegistry<T>
{
    private Dictionary<T, string> _objectToIDMap = new Dictionary<T, string>();
    private Dictionary<string, T> _idToObjectMap = new Dictionary<string, T>();

    /// <summary>Adds the object into the maps.</summary>
    public void Add(string id, T obj)
    {
        _objectToIDMap.Add(obj, id);
        _idToObjectMap.Add(id, obj);
    }

    public string GetIDFor(T obj)
    {
        return _objectToIDMap[obj];
    }

    public T GetObjectFor(string id)
    {
        return _idToObjectMap[id];
    }
}
