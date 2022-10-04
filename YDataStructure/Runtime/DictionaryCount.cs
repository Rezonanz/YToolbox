using System.Collections;
using System.Collections.Generic;

namespace YellowJelloGames.YDataStructure
{
    public class DictionaryCount<TKey> : IEnumerable<KeyValuePair<TKey, int>>
    {
        private readonly Dictionary<TKey, int> _items = new();

        public int this[TKey key]
        {
            get => _items[key];
            set => _items[key] = value;
        }

        public bool Contains(TKey key) => _items.ContainsKey(key);

        public int Count => _items.Count;

        public ICollection<TKey> Items => _items.Keys;

        public bool TryGetValue(TKey key, out int i)
        {
            return _items.TryGetValue(key, out i);
        }

        public int Increase(TKey key, int value = 1)
        {
            if (_items.ContainsKey(key))
            {
                _items[key] += value;
            }
            else
            {
                _items[key] = value;
            }

            return _items[key];
        }

        public int Decrease(TKey key, int value = 1)
        {
            if (!_items.ContainsKey(key))
            {
                return -1;
            }

            _items[key] -= value;
            if (_items[key] > 0)
            {
                return _items[key];
            }

            _items.Remove(key);
            return 0;
        }
        
        public bool Remove(TKey key)
        {
            return _items.Remove(key);
        }

        public void ShiftCount(TKey key, int delta)
        {
            if (delta > 0)
            {
                Increase(key, delta);
            }
            else
            {
                Decrease(key, delta);
            }
        }

        public void Clear()
        {
            _items.Clear();
        }

        public IEnumerator<KeyValuePair<TKey, int>> GetEnumerator() => _items.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}