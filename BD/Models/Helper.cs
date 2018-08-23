using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.Serialization.Formatters.Binary;
using System.Web;
using Couchbase;
using Enyim.Caching;
using Enyim.Caching.Configuration;
using Enyim.Caching.Memcached;
using MemcachedTranscoder;
using StackExchange.Redis;

namespace BD.Models
{
    public static class Helper
    {
        public static IDatabase RedisDatabase;

        static Helper()
        {
            var config = ConfigurationOptions.Parse("localhost");
            config.AbortOnConnectFail = false;
            config.SyncTimeout = 3000;
            ConnectionMultiplexer redis = ConnectionMultiplexer.Connect(config);
            RedisDatabase = redis.GetDatabase();
        }

        public static byte[] ToByteArray<T>(T obj)
        {
            if (obj == null)
                return null;
            BinaryFormatter bf = new BinaryFormatter();
            using (MemoryStream ms = new MemoryStream())
            {
                bf.Serialize(ms, obj);
                return ms.ToArray();
            }
        }

        public static T FromByteArray<T>(byte[] data)
        {
            if (data == null)
                return default(T);
            BinaryFormatter bf = new BinaryFormatter();
            using (MemoryStream ms = new MemoryStream(data))
            {
                object obj = bf.Deserialize(ms);
                return (T)obj;
            }
        }
    }
}