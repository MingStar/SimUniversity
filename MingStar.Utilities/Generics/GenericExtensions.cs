using System.Collections.Generic;

namespace MingStar.Utilities.Generics
{
    public static class GenericExtensions
    {
        public static TValue GetValueOrAddNew<TKey, TValue>(this IDictionary<TKey, TValue> dict, TKey key)
            where TValue : new()
        {
            if (!dict.ContainsKey(key))
            {
                dict[key] = new TValue();
            }
            return dict[key];
        }

        public static TValue GetValueOrDefault<TKey, TValue>(this IDictionary<TKey, TValue> dict, TKey key)
            where TValue : struct
        {
            if (!dict.ContainsKey(key))
            {
                dict[key] = default(TValue);
            }
            return dict[key];
        }

        public static void InitialiseKeys<TKey, TValue>(this IDictionary<TKey, TValue> dict, params TKey[] keys)
        {
            foreach (TKey key in keys)
            {
                dict[key] = default(TValue);
            }
        }
    }
}