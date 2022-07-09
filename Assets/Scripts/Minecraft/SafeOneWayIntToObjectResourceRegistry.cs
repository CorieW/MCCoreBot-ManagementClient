using System.Collections.Generic;

namespace Minecraft
{
    public class SafeOneWayIntToObjectResourceRegistry<T>
    {
        private Dictionary<int, T> _registry = new Dictionary<int, T>();

        private T _safeObj;

        public void Add(int id, T obj)
        {
            _registry.Add(id, obj);
        }

        public T GetObjectFor(int id)
        {
            return _registry[id];
        }

        public T SafeGetObjectFor(int id)
        {
            if (_registry.TryGetValue(id, out T obj))
            {
                return obj;
            }
            return _safeObj;
        }

        /// <summary>Sets the object that is returned when SafeGetObjectFor can't find the matching object.</summary>
        public void SetSafeReturnObject(T obj)
        {
            _safeObj = obj;
        }
    }
}