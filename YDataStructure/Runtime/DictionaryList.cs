using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Pool;

namespace YellowJelloGames.YDataStructure
{
    public sealed class DictionaryList<TKey, TValue> : IReadOnlyCollection<KeyValuePair<TKey, List<TValue>>>, IDisposable
    {
        private readonly Dictionary<TKey, List<TValue>> _items = new();
        
        ~DictionaryList()
        {
            Dispose();
        }

        public List<TValue> this[TKey key] => _items[key];
        public Dictionary<TKey, List<TValue>>.KeyCollection Keys => _items.Keys;
        public Dictionary<TKey, List<TValue>>.ValueCollection Values => _items.Values;
        public int Count => _items.Count;

        public bool Contains(TKey key) => _items.ContainsKey(key);

        public void Add(TKey key, TValue value)
        {
            if (_items.ContainsKey(key))
            {
                _items[key].Add(value);
            }
            else
            {
                CreateList(key, value);
            }
        }

        public void AddUnique(TKey key, TValue value)
        {
            if (_items.ContainsKey(key))
            {
                if (!_items[key].Contains(value)) _items[key].Add(value);
            }
            else
            {
                CreateList(key, value);
            }
        }

        public bool Remove(TKey key, TValue value)
        {
            if (!_items.ContainsKey(key))
            {
                return false;
            }

            _items[key].Remove(value);
            if (_items[key].Count == 0) RemoveList(key);

            return true;
        }

        public bool Remove(TKey key)
        {
            return _items.ContainsKey(key) && RemoveList(key);
        }

        private void CreateList(TKey key, TValue value)
        {
            var list = ListPool<TValue>.Get();
            list.Add(value);

            _items[key] = list;
        }


        private bool RemoveList(TKey key)
        {
            ListPool<TValue>.Release(_items[key]);

            return _items.Remove(key);
        }
        
        public bool TryGetValue(TKey key, out List<TValue> value)
        {
            return _items.TryGetValue(key, out value);
        }

        public void Dispose()
        {
            Clear();
        }

        public void Clear()
        {
            // return to pool
            foreach (var key in _items.Keys) ListPool<TValue>.Release(_items[key]);

            _items.Clear();
        }

        public IEnumerator<KeyValuePair<TKey, List<TValue>>> GetEnumerator() => _items.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}