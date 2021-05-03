using MultiValueDictionarySample.Interfaces;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace MultiValueDictionarySample
{
    public class MultiValueDictionary<TKey, TValue> : IMultiValueDictionary<TKey, TValue>, IEnumerable
    {

        private readonly Dictionary<TKey, IEnumerable<TValue>> _multiValueDictionary = new Dictionary<TKey, IEnumerable<TValue>>();

        public IEnumerable<TValue> Values => _multiValueDictionary.Values.SelectMany(x => x);

        public IEnumerable<TKey> Keys => _multiValueDictionary.Keys;

        public int Count => _multiValueDictionary.Count;

        public void AddOrUpdate(TKey key, TValue value)
        {
            if (ContainsKey(key))
            {
                if (!_multiValueDictionary[key].Contains(value)) { (_multiValueDictionary[key] as List<TValue>)?.Add(value); }
            }
            else
            {
                _multiValueDictionary.Add(key, new List<TValue>() { value });
            }
        }

        //if we wanted to pass list
        public void AddOrUpdate(TKey key, IEnumerable<TValue> values)
        {
            if (ContainsKey(key))
            {
                (_multiValueDictionary[key] as List<TValue>)?.AddRange(values);
            }
            else
            {
                _multiValueDictionary.Add(key, (IList<TValue>)values);
            }
        }

        public void Clear(TKey key)
        {
            if (ContainsKey(key))
            {
                (_multiValueDictionary[key] as List<TValue>)?.Clear();
            }
        }

        public bool TryGetValue(TKey key, out IEnumerable<TValue> values) => _multiValueDictionary.TryGetValue(key, out values);

        public bool ContainsKey(TKey key) => _multiValueDictionary.ContainsKey(key);

        public bool RemoveKey(TKey key) => _multiValueDictionary.Remove(key);

        public bool RemoveValue(TKey key, TValue value) => ContainsKey(key) && (_multiValueDictionary[key] as List<TValue>)?.Remove(value) == true;

        public void Clean()
        {
            foreach (var item in _multiValueDictionary.Where(x => !x.Value.Any()))
            {
                _multiValueDictionary.Remove(item.Key);
            }
        }

        public IEnumerator GetEnumerator() => _multiValueDictionary.GetEnumerator();
    }
}