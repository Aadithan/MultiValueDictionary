using System.Collections.Generic;

namespace MultiValueDictionarySample.Interfaces
{
    public interface IMultiValueDictionary<TKey, TValue>
    {   
        IEnumerable<TKey> Keys { get; }

        IEnumerable<TValue> Values { get; }

        int Count { get; }

        bool TryGetValue(TKey key, out IEnumerable<TValue> value);

        void AddOrUpdate(TKey key, TValue value);

        void AddOrUpdate(TKey key, IEnumerable<TValue> values);

        bool ContainsKey(TKey key);
         
        bool RemoveKey(TKey key);
         
        bool RemoveValue(TKey key, TValue value); 

        // Removes the key when there is no value for it
        void Clean(); 

        //Clear All Key values but keep the key
        void Clear(TKey key); 
    }
}
