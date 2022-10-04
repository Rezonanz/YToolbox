using System;
using System.Collections.Generic;

namespace YellowJelloGames.YDataStructure
{
    public class DictionaryDelegate<TKey, TDelegate> where TDelegate : Delegate
    {
        private readonly Dictionary<TKey, TDelegate> _eventTable = new();

        public TDelegate this[TKey key] => _eventTable.TryGetValue(key, out var callback) ? callback : null;

        public int Count => _eventTable.Count;
        public Dictionary<TKey, TDelegate>.KeyCollection Keys => _eventTable.Keys;

        public bool TryGetValue(TKey key, out TDelegate callback) => _eventTable.TryGetValue(key, out callback);

        public void AddListener(TKey key, TDelegate callback)
        {
            if (_eventTable.TryGetValue(key, out var value))
            {
                _eventTable[key] = (TDelegate)Delegate.Combine(value, callback);
                return;
            }

            _eventTable.Add(key, callback);
        }

        public void RemoveListener(TKey key, TDelegate callback)
        {
            if (!_eventTable.ContainsKey(key)) return;

            _eventTable[key] = (TDelegate)Delegate.Remove(_eventTable[key], callback);
            
            if (_eventTable[key] is null)
            {
                _eventTable.Remove(key);
            }
        }
    }
}