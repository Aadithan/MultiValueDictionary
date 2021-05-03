using System;
using System.Collections.Generic;
using System.Linq;

namespace MultiValueDictionarySample
{
    public static class DictionaryOperations
    {
        public static readonly MultiValueDictionary<string, string> ApplicationDictionary = new MultiValueDictionary<string, string>();

        /// <summary>
        /// Returns all the keys in the dictionary.  Order is not guaranteed.
        /// </summary>
        public static List<string> GetKeys()
        { 
            if (!ApplicationDictionary.Keys.Any())
                Console.WriteLine("No keys found on the dictionary.");
            var i = 1;
            foreach (var key in ApplicationDictionary.Keys)
            { 
                Console.WriteLine($"{i}){key}");
                i++;
            } 
            return ApplicationDictionary.Keys.ToList();
        }

        /// <summary>
        /// Returns the collection of strings for the given key.  Return order is not guaranteed.  Returns an error if the key does not exists.
        /// </summary>
        public static List<string> GetMembers(string key)
        {
            if (!ApplicationDictionary.Values.Any())
            {
                Console.WriteLine("No members found in the dictionary.");
                return null;
            }

            if (string.IsNullOrEmpty(key))
            {
                Console.WriteLine("Please provide valid key");
                return null;
            }

            var opKey = ApplicationDictionary.Keys.SingleOrDefault(x => x.Equals(key.Trim().ToLowerInvariant(), StringComparison.CurrentCultureIgnoreCase));

            if (opKey == null)
            {
                Console.WriteLine($"ERROR, key: {key} does not exist");
                return null;
            }

            ApplicationDictionary.TryGetValue(opKey, out var dicValues);
            var valueLst = dicValues.ToList();
            foreach (var val in valueLst)
            {
                Console.WriteLine($"{val}");
            }

            return valueLst.ToList();
        }

        /// <summary>
        /// Add a member to a collection for a given key. Displays an error if the value already existed in the collection. 
        /// </summary>
        public static void Add(string key, string value)
        {
            ApplicationDictionary.TryGetValue(key, out var opValues);

            if (opValues == null || !opValues.Contains(value))
            {
                ApplicationDictionary.AddOrUpdate(key, value);
                Console.WriteLine("Added");
                return;
            }

            Console.WriteLine($"ERROR, value: {value} already exists");
        }

        /// <summary>
        /// Removes a value from a key.
        /// If the last value is removed from the key, they key is removed from the dictionary.
        /// If the key or value does not exist, displays an error. 
        /// </summary>
        public static void Remove(string key, string value)
        {
            var isKeyFound = ApplicationDictionary.ContainsKey(key);
            if (!isKeyFound)
            {
                Console.WriteLine($"ERROR, key: {key} does not exist");
                return;
            }

            ApplicationDictionary.TryGetValue(key, out var opValues);

            if (opValues == null || !opValues.Contains(value))
            {
                Console.WriteLine($"ERROR, {value} does not exist");
            }
            else if (opValues.Contains(value))
            {
                ApplicationDictionary.RemoveValue(key, value);
                ApplicationDictionary.Clean();
                Console.WriteLine("Removed");
            }
        }

        /// <summary>
        /// Removes all value for a key and removes the key from the dictionary. Returns an error if the key does not exist.
        /// </summary>
        public static void RemoveAll(string key)
        {
            ApplicationDictionary.TryGetValue(key, out var opValues);

            if (opValues == null)
            {
                Console.WriteLine($"ERROR, key: {key} does not exist");
            }
            else
            {
                var valueLst = opValues.ToList();
                foreach (var itmValues in valueLst)
                {
                    ApplicationDictionary.RemoveValue(key, itmValues);
                }

                ApplicationDictionary.Clean();
                Console.WriteLine("Removed");
            }
        }

        /// <summary>
        /// Removes all keys and all values from the dictionary.
        /// </summary>
        public static void Clear()
        {
            foreach (var key in ApplicationDictionary.Keys)
            {
                ApplicationDictionary.RemoveKey(key);
            }

            Console.WriteLine("Cleared");
        }

        /// <summary>
        /// Returns whether a key exists or not. 
        /// </summary>
        public static bool KeyExists(string key)
        {
            var isKeyFound = ApplicationDictionary.ContainsKey(key);
            Console.WriteLine(isKeyFound);
            return isKeyFound;
        }

        /// <summary>
        /// Returns whether a value exists within a key.  Returns false if the key does not exist.
        /// </summary>
        public static bool ValueExists(string key, string value)
        {
            var isKeyFound = ApplicationDictionary.ContainsKey(key);
            if (!isKeyFound)
            {
                Console.WriteLine($"ERROR, key: {key} does not exist");
            }

            ApplicationDictionary.TryGetValue(key, out var opValues);

            if (opValues == null || !opValues.Contains(value))
            {
                Console.WriteLine(false);
                return false;
            }
            else if (opValues.Contains(value))
            {
                Console.WriteLine(true);
                return true;
            }

            return false;
        }

        /// <summary>
        /// Returns all the values in the dictionary.  Returns nothing if there are none. Order is not guaranteed.
        /// </summary>
        public static void AllMembers()
        {
            if (!ApplicationDictionary.Keys.Any())
                Console.WriteLine("empty set");

            foreach (var itemKey in ApplicationDictionary.Keys)
            {
                ApplicationDictionary.TryGetValue(itemKey, out var dicValues);
                var valueLst = dicValues.ToList();
                foreach (var val in valueLst)
                {
                    Console.WriteLine($"{val}");
                }
            }
        }

        /// <summary>
        /// Returns all keys in the dictionary and all of their values.  Returns nothing if there are none.  Order is not guaranteed.
        /// </summary>
        public static void Items()
        {
            if (!ApplicationDictionary.Keys.Any() || ApplicationDictionary.Keys == null)
            {
                Console.WriteLine("Dictionary is empty.");
            }
            foreach (var itemKey in ApplicationDictionary.Keys)
            {
                ApplicationDictionary.TryGetValue(itemKey, out var dicValues);
                foreach (var val in dicValues.ToList())
                {
                    Console.WriteLine($"{itemKey}: {val}");
                }
            }
        }
    }
}
