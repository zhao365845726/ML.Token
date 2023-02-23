using CSRedis;
using System;

namespace ML.Token
{
    public class RedisHelper
    {
        //public static CSRedisClient rds = new CSRedisClient(UtilHelper.ReadAppSetting("Cache_Redis_Configuration"));

        //public static CSRedisClient rds = new CSRedisClient("127.0.0.1:6379,password=123456,defaultDatabase=2,prefix=ML:,poolsize=1500,tryit=0");

        //history
        //public static CSRedisClient rds = new CSRedisClient("r-2ze0lwx0hkxbcf83u5.redis.rds.aliyuncs.com:6379,defaultDatabase=1,prefix=ML:,poolsize=1500,tryit=0");

        //new
        public static CSRedisClient rds = new CSRedisClient("r-2ze0lwx0hkxbcf83u5.redis.rds.aliyuncs.com:6379,defaultDatabase=2,prefix=ML:,poolsize=1500,tryit=0");

        public RedisHelper()
        {
        }

        public static void Set(string key, string value)
        {
            rds.Set($"{key}", value);
        }

        public static void Set(string key, string value, TimeSpan span)
        {
            rds.Set($"{key}", value, span);
        }

        public static void Set<T>(string key, T value, TimeSpan span)
        {
            rds.Set($"{key}", value, span);
        }

        public static void Set(string key, object value, int tls)
        {
            rds.Set($"{key}", value, tls);
        }

        public static void Set<T>(string key, T value, int tls)
        {
            rds.Set($"{key}", value, tls);
        }

        public static void Set(string key, string value, int tls)
        {
            rds.Set($"{key}", value, tls);
        }

        public static object Get(string key)
        {
            object obj = rds.Get<object>($"{key}");
            return obj;
        }

        public static T Get<T>(string key)
        {
            T obj = rds.Get<T>($"{key}");
            return obj;
        }

        public static T Get<T>(string key, bool isFullKey)
        {
            T obj = rds.Get<T>($"{key}");
            return obj;
        }

        public static void Remove(string key)
        {
            rds.Del(new string[] { $"{key}" });
        }

        public static void Remove(string key, bool isFullKey)
        {
            rds.Del(new string[] { $"{key}" });
        }

        public static string[] Keys(string pattern)
        {
            return rds.Keys($"{pattern}*");
        }
    }
}
