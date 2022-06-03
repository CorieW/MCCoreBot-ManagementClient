using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntToObjectResourceRegistry<T>
{
    private int currentIndex;
    private Dictionary<T, int> _objectToIDMap = new Dictionary<T, int>();
    private Dictionary<int, T> _idToObjectMap = new Dictionary<int, T>();

    /// <summary>Adds the object into the maps.</summary>
    public void Add(T obj)
    {
        _objectToIDMap.Add(obj, currentIndex);
        _idToObjectMap.Add(currentIndex, obj);
        currentIndex += 1;
    }

    public int GetIDFor(T obj)
    {
        return _objectToIDMap[obj];
    }

    public T GetObjectFor(int id)
    {
        return _idToObjectMap[id];
    }
}
