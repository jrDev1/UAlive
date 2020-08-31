using Lasm.OdinSerializer;
using Ludiq;
using System;
using System.Collections.Generic;

namespace Lasm.UAlive
{
    [Serializable]
    public class DefinedDictionary<TKey, TValue>
    {
        [OdinSerialize]
        public Dictionary<TKey, TValue> previous = new Dictionary<TKey, TValue>();
        [OdinSerialize]
        public Dictionary<TKey, TValue> current = new Dictionary<TKey, TValue>();

        public TValue this[TKey key]
        {
            get { return current[key]; }
            set { current[key] = value; }
        }

        public void Add(TKey key, TValue value)
        {
            if (!previous.ContainsKey(key)) previous.Add(key, value);
            if (!current.ContainsKey(key)) current.Add(key, value);
        }

        public void Remove(TKey key)
        {
            if (previous.ContainsKey(key)) previous.Remove(key);
            if (current.ContainsKey(key)) current.Remove(key);
        }

        public void Clear()
        {
            current.Clear();
        }

        public IEnumerable<TKey> Keys()
        {
            return current.Keys;
        }

        public IEnumerable<TValue> Values()
        {
            return current.Values;
        }

        public TValue Define(TKey key, ref bool defined, Func<TValue, TValue> onCreate, Action<TValue> exists)
        {
            var item = current.Define(previous, key, onCreate, exists);
            if (!current.ContainsKey(key))
            {
                current.Add(key, (TValue)typeof(TValue).Default());
                defined = true;
            }
            current[key] = item;
            return item;
        } 

        public void Undefine(ref bool removed, Action<TValue> onRemoved)
        {
            var removeAmount = 10;
            var _removed = removed;

            for (int i = 0; i < removeAmount; i++)
            {
                if (i > 0 && !removed) break;
                {
                    current.Undefine(previous, (val)=> {
                        _removed = true;
                        onRemoved(val);
                    });
                }
            }
        }
    }
}