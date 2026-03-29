using System.Collections.Generic;
using System.Linq;



namespace Rune
{
    public abstract class KeyedDataPool<TData> : MonoPlusSingleton<KeyedDataPool<TData>> where TData : KeyedData, new()
    {
        public static void Add(List<TData> dataList)
        {
            foreach (var data in dataList) SingletonInstance._dataLookup[data.Key] = data;
        }

        public static void Add(params TData[] data)
        {
            Add(new List<TData>(data));
        }

        public static TData Get(string key)
        {
            return SingletonInstance._dataLookup[key];
        }

        public static TData GetOrNew(string key)
        {
            return SingletonInstance._dataLookup.ContainsKey(key) ? SingletonInstance._dataLookup[key] : new();
        }

        public static TData GetOrDefault(string key)
        {
            return SingletonInstance._dataLookup.GetValueOrDefault(key);
        }

        public static List<TData> GetAll()
        {
            return SingletonInstance._dataLookup.Values.ToList();
        }

        public static bool TryGet(string key, out TData value)
        {
            return SingletonInstance._dataLookup.TryGetValue(key, out value);
        }

        public static bool Has(string key)
        {
            return SingletonInstance._dataLookup.ContainsKey(key);
        }



        public bool IsPreloaded { get; set; } = false;



        protected readonly Dictionary<string, TData> _dataLookup = new();
    }    
}