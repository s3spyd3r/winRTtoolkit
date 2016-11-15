using System.Collections.Generic;

namespace winRTtoolkit.Helpers
{
    public static class DictionaryHelpers
    {
        public static void AddOrUpdate<T, TQ>(this IDictionary<T, TQ> dictionary, T key, TQ value)
        {
            if (dictionary.ContainsKey(key) == false)
                dictionary.Add(key, value);
            else
                dictionary[key] = value;
        }

        public static void AddOrUpdate<T, TQ>(this IDictionary<T, List<TQ>> dictionary, T key, TQ value)
        {
            if (dictionary.ContainsKey(key) == false)
                dictionary.Add(key, new List<TQ> { value });
            else
                dictionary[key].Add(value);
        }
    }
}
