using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Runtime.Caching;

namespace KuProStore.BL.Service.Cacher
{
    public sealed class AppCache
    {
        private static readonly AppCache instance = new AppCache();

        static AppCache()
        { }

        private AppCache()
        { }

        public static AppCache Instance
        {
            get
            {
                return instance;
            }
        }

        public bool Add(string name, object value, int minutes)
        {
            MemoryCache memoryCache = MemoryCache.Default;
            return memoryCache.Add(name, value, DateTime.Now.AddMinutes(minutes));
        }

        public object GetValue(string name)
        {
            MemoryCache memoryCache = MemoryCache.Default;
            return memoryCache.Get(name);
        }

        public void Update(string name, object value, int minutes)
        {
            MemoryCache memoryCache = MemoryCache.Default;
            memoryCache.Set(name, value, DateTime.Now.AddMinutes(minutes));
        }

        public void Delete(string name)
        {
            MemoryCache memoryCache = MemoryCache.Default;
            if (memoryCache.Contains(name))
            {
                memoryCache.Remove(name);
            }
        }
    }
}