using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Minecraft
{
    public class SafeIntToObjectResourceRegistry<T>
    {
        private int _currentIndex;
        
        private Dictionary<T, int> _objectToIDMap = new Dictionary<T, int>();
        private Dictionary<int, T> _idToObjectMap = new Dictionary<int, T>();

        private int _safeID = -1;
        private T _safeObj;

        public int Count {
            get {
                return _objectToIDMap.Count;
            }
        }

        /// <summary>Adds the object into the maps.</summary>
        public void Add(T obj)
        {
            _objectToIDMap.Add(obj, _currentIndex);
            _idToObjectMap.Add(_currentIndex, obj);
            _currentIndex += 1;
        }

        public int GetIDFor(T obj)
        {
            return _objectToIDMap[obj];
        }

        public T GetObjectFor(int id)
        {
            return _idToObjectMap[id];
        }

        public int SafeGetIDFor(T obj)
        {
            if (_objectToIDMap.TryGetValue(obj, out int id))
            {
                return id;
            }
            return _safeID;
        }

        public T SafeGetObjectFor(int id)
        {
            if (_idToObjectMap.TryGetValue(id, out T obj))
            {
                return obj;
            }
            return _safeObj;
        }

        /// <summary>Sets the object that is returned when SafeGetIDFor can't find the matching ID.</summary>
        public void SetSafeReturnID(int id)
        {
            _safeID = id;
        }

        /// <summary>Sets the object that is returned when SafeGetObjectFor can't find the matching object.</summary>
        public void SetSafeReturnObject(T obj)
        {
            _safeObj = obj;
        }
    }
}
