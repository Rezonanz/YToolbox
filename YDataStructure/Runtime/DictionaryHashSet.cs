using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Pool;

namespace YellowJelloGames.YDataStructure
{
    public sealed class DictionaryHashSet<TKey, TValue> : IEnumerable<KeyValuePair<TKey, HashSet<TValue>>>, IDisposable
    {
        private readonly Dictionary<TKey, HashSet<TValue>> _items = new();
        
        ~DictionaryHashSet()
        {
            Dispose();
        }

        public HashSet<TValue> this[TKey key] => _items[key];
        public Dictionary<TKey, HashSet<TValue>>.KeyCollection Keys => _items.Keys;
        public Dictionary<TKey, HashSet<TValue>>.ValueCollection Values => _items.Values;

        public bool Contains(TKey key) => _items.ContainsKey(key);

        public void Add(TKey key, TValue value)
        {
            if (_items.ContainsKey(key))
            {
                _items[key].Add(value);
            }
            else
            {
                CreateEntry(key, value);
            }
        }

        public bool Remove(TKey key, TValue value)
        {
            if (!_items.ContainsKey(key))
            {
                return false;
            }

            _items[key].Remove(value);
            if (_items[key].Count == 0)
            {
                RemoveEntry(key);
            }

            return true;
        }

        public bool Remove(TKey key)
        {
            return _items.ContainsKey(key) && RemoveEntry(key);
        }

        private void CreateEntry(TKey key, TValue value)
        {
            var set = HashSetPool<TValue>.Get();
            set.Add(value);

            _items[key] = set;
        }


        private bool RemoveEntry(TKey key)
        {
            HashSetPool<TValue>.Release(_items[key]);

            return _items.Remove(key);
        }
        
        public bool TryGetValue(TKey key, out HashSet<TValue> value)
        {
            return _items.TryGetValue(key, out value);
        }

        public void Dispose() => Clear();

        public void Clear()
        {
            foreach (var key in _items.Keys)
            {
                HashSetPool<TValue>.Release(_items[key]);
            }

            _items.Clear();
        }

        public IEnumerator<KeyValuePair<TKey, HashSet<TValue>>> GetEnumerator() => _items.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}