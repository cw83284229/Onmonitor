using Newtonsoft.Json;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ruanmou.Redis.Exchange.Service;
using Ruanmou.Redis.Exchange.Init;

namespace Ruanmou.Redis.Exchange.Interface
{
    /// <summary>
    /// Redis操作方法基础类
    /// </summary>
    public abstract class RedisBase : IDisposable
    {

        #region 属性字段
        /// <summary>
        /// 网站Redis 系统自定义Key前缀
        /// </summary>
        protected string CustomKey = RedisManager.RedisSysCustomKey;
        /// <summary>
        /// 网站Redis 链接字符串
        /// </summary>
        protected readonly ConnectionMultiplexer _conn;
        /// <summary>
        /// Redis操作对象
        /// </summary>
        protected readonly IDatabase redis = null;
        #endregion

        #region 构造函数
        /// <summary>
        /// 初始化Redis操作方法基础类
        /// </summary>
        /// <param name="dbNum">操作的数据库索引0-64(需要在conf文件中配置)</param>
        protected RedisBase(int? dbNum = null)
        {
            _conn = RedisManager.Instance;
            if (_conn != null)
            {
                redis = _conn.GetDatabase(dbNum ?? RedisManager.RedisDataBaseIndex);
            }
            else
            {
                throw new ArgumentNullException("Redis连接初始化失败");
            }
        }

        private bool _disposed = false;
        protected virtual void Dispose(bool disposing)
        {
            if (!this._disposed)
            {
                if (disposing)
                {
                    _conn.Dispose();
                }
            }
            this._disposed = true;
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        #endregion 构造函数

        #region 外部调用静态方法
        /// <summary>
        /// 获取Redis的String数据类型操作辅助方法类
        /// </summary>
        /// <returns></returns>
        public static RedisStringService StringService => new RedisStringService();
        /// <summary>
        /// 获取Redis的Hash数据类型操作辅助方法类
        /// </summary>
        /// <returns></returns>
        public static RedisHashService HashService => new RedisHashService();
        /// <summary>
        /// 获取Redis的List数据类型操作辅助方法类
        /// </summary>
        /// <returns></returns>
        public static RedisListService ListService => new RedisListService();
        /// <summary>
        /// 获取Redis的Set无序集合数据类型操作辅助方法类
        /// </summary>
        /// <returns></returns>
        public static RedisSetService SetService => new RedisSetService();
        /// <summary>
        /// 获取Redis的SortedSet(ZSet)有序集合数据类型操作辅助方法类
        /// </summary>
        /// <returns></returns>
        public static RedisSortedSetService SortedSetService => new RedisSortedSetService();

        #endregion

        #region 公共操作方法

        #region 不建议公开这些方法，如果项目中用不到，建议注释或者删除
        /// <summary>
        /// 获取Redis事务对象
        /// </summary>
        /// <returns></returns>
        public ITransaction CreateTransaction() => redis.CreateTransaction();

        /// <summary>
        /// 获取Redis服务和常用操作对象
        /// </summary>
        /// <returns></returns>
        public IDatabase GetDatabase() => redis;

        /// <summary>
        /// 获取Redis服务
        /// </summary>
        /// <param name="hostAndPort"></param>
        /// <returns></returns>
        public IServer GetServer(string hostAndPort) => _conn.GetServer(hostAndPort);

        /// <summary>
        /// 执行Redis事务
        /// </summary>
        /// <param name="act"></param>
        /// <returns></returns>
        public bool RedisTransaction(Action<ITransaction> act)
        {
            var tran = redis.CreateTransaction();
            act.Invoke(tran);
            bool committed = tran.Execute();
            return committed;
        }
        /// <summary>
        /// Redis锁
        /// </summary>
        /// <param name="act"></param>
        /// <param name="ts">锁住时间</param>
        public void RedisLockTake(Action act, TimeSpan ts)
        {
            RedisValue token = Environment.MachineName;
            string lockKey = "lock_LockTake";
            if (redis.LockTake(lockKey, token, ts))
            {
                try
                {
                    act();
                }
                finally
                {
                    redis.LockRelease(lockKey, token);
                }
            }
        }
        #endregion 其他

        #region 常用Key操作
        /// <summary>
        /// 设置前缀
        /// </summary>
        /// <param name="customKey"></param>
        public void SetSysCustomKey(string customKey) => CustomKey = customKey;

        /// <summary>
        /// 组合缓存Key名称
        /// </summary>
        /// <param name="oldKey"></param>
        /// <returns></returns>
        public string AddSysCustomKey(string oldKey) => $"{CustomKey}{oldKey}";

        #region 同步方法

        /// <summary>
        /// 删除单个key
        /// </summary>
        /// <param name="key">要删除的key</param>
        /// <returns>是否删除成功</returns>
        public bool KeyDelete(string key)
        {
            key = AddSysCustomKey(key);
            return redis.KeyDelete(key);
        }

        /// <summary>
        /// 删除多个key
        /// </summary>
        /// <param name="keys">要删除的key集合</param>
        /// <returns>成功删除的个数</returns>
        public long KeyDelete(params string[] keys)
        {
            RedisKey[] newKeys = keys.Select(o => (RedisKey)AddSysCustomKey(o)).ToArray();
            return redis.KeyDelete(newKeys);
        }

        /// <summary>
        /// 清空当前DataBase中所有Key
        /// </summary>
        public void KeyFulsh()
        {
            //直接执行清除命令
            redis.Execute("FLUSHDB");
        }

        /// <summary>
        /// 判断key是否存在
        /// </summary>
        /// <param name="key">要判断的key</param>
        /// <returns></returns>
        public bool KeyExists(string key)
        {
            key = AddSysCustomKey(key);
            return redis.KeyExists(key);
        }

        /// <summary>
        /// 重新命名key
        /// </summary>
        /// <param name="key">就的redis key</param>
        /// <param name="newKey">新的redis key</param>
        /// <returns></returns>
        public bool KeyRename(string key, string newKey)
        {
            key = AddSysCustomKey(key);
            newKey = AddSysCustomKey(newKey);
            return redis.KeyRename(key, newKey);
        }

        /// <summary>
        /// 设置Key的过期时间
        /// </summary>
        /// <param name="key">redis key</param>
        /// <param name="expiry">过期时间</param>
        /// <returns></returns>
        public bool KeyExpire(string key, TimeSpan? expiry = default(TimeSpan?))
        {
            key = AddSysCustomKey(key);
            return redis.KeyExpire(key, expiry);
        }


        #endregion

        #region 异步方法

        /// <summary>
        /// 删除单个key
        /// </summary>
        /// <param name="key">要删除的key</param>
        /// <returns>是否删除成功</returns>
        public async Task<bool> KeyDeleteAsync(string key)
        {
            key = AddSysCustomKey(key);
            return await redis.KeyDeleteAsync(key);
        }

        /// <summary>
        /// 删除多个key
        /// </summary>
        /// <param name="keys">要删除的key集合</param>
        /// <returns>成功删除的个数</returns>
        public async Task<long> KeyDeleteAsync(params string[] keys)
        {
            RedisKey[] newKeys = keys.Select(o => (RedisKey)AddSysCustomKey(o)).ToArray();
            return await redis.KeyDeleteAsync(newKeys);
        }

        /// <summary>
        /// 清空当前DataBase中所有Key
        /// </summary>
        public async Task KeyFulshAsync()
        {
            //直接执行清除命令
            await redis.ExecuteAsync("FLUSHDB");
        }

        /// <summary>
        /// 判断key是否存在
        /// </summary>
        /// <param name="key">要判断的key</param>
        /// <returns></returns>
        public async Task<bool> KeyExistsAsync(string key)
        {
            key = AddSysCustomKey(key);
            return await redis.KeyExistsAsync(key);
        }

        /// <summary>
        /// 重新命名key
        /// </summary>
        /// <param name="key">就的redis key</param>
        /// <param name="newKey">新的redis key</param>
        /// <returns></returns>
        public async Task<bool> KeyRenameAsync(string key, string newKey)
        {
            key = AddSysCustomKey(key);
            newKey = AddSysCustomKey(newKey);
            return await redis.KeyRenameAsync(key, newKey);
        }

        /// <summary>
        /// 设置Key的过期时间
        /// </summary>
        /// <param name="key">redis key</param>
        /// <param name="expiry">过期时间</param>
        /// <returns></returns>
        public async Task<bool> KeyExpireAsync(string key, TimeSpan? expiry = default(TimeSpan?))
        {
            key = AddSysCustomKey(key);
            return await redis.KeyExpireAsync(key, expiry);
        }
        #endregion

        #endregion 

        #endregion

        #region 辅助方法

        /// <summary>
        /// 将对象转换成string字符串
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value"></param>
        /// <returns></returns>
        protected string ConvertJson<T>(T value)
        {
            string result = value is string ? value.ToString() :
                JsonConvert.SerializeObject(value, Formatting.None);
            return result;
        }
        /// <summary>
        /// 将值反系列化成对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value"></param>
        /// <returns></returns>
        protected T ConvertObj<T>(RedisValue value)
        {
            return value.IsNullOrEmpty ? default(T) : JsonConvert.DeserializeObject<T>(value);
        }

        /// <summary>
        /// 将值反系列化成对象集合
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="values"></param>
        /// <returns></returns>
        protected List<T> ConvetList<T>(RedisValue[] values)
        {
            List<T> result = new List<T>();
            foreach (var item in values)
            {
                var model = ConvertObj<T>(item);
                result.Add(model);
            }
            return result;
        }
        /// <summary>
        /// 将string类型的Key转换成 <see cref="RedisKey"/> 型的Key
        /// </summary>
        /// <param name="redisKeys"></param>
        /// <returns></returns>
        protected RedisKey[] ConvertRedisKeys(List<string> redisKeys) => redisKeys.Select(redisKey => (RedisKey)redisKey).ToArray();

        /// <summary>
        /// 将string类型的Key转换成 <see cref="RedisKey"/> 型的Key
        /// </summary>
        /// <param name="redisKeys"></param>
        /// <returns></returns>
        protected RedisKey[] ConvertRedisKeys(params string[] redisKeys) => redisKeys.Select(redisKey => (RedisKey)redisKey).ToArray();

        /// <summary>
        /// 将string类型的Key转换成 <see cref="RedisKey"/> 型的Key，并添加前缀字符串
        /// </summary>
        /// <param name="redisKeys"></param>
        /// <returns></returns>
        protected RedisKey[] ConvertRedisKeysAddSysCustomKey(params string[] redisKeys) => redisKeys.Select(redisKey => (RedisKey)AddSysCustomKey(redisKey)).ToArray();
        /// <summary>
        /// 将值集合转换成RedisValue集合
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="redisValues"></param>
        /// <returns></returns>
        protected RedisValue[] ConvertRedisValue<T>(params T[] redisValues) => redisValues.Select(o => (RedisValue)ConvertJson<T>(o)).ToArray();
        #endregion 辅助方法

    }

}
