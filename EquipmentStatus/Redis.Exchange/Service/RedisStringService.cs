using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ruanmou.Redis.Exchange.Interface;

namespace Ruanmou.Redis.Exchange.Service
{

    /// <summary>
    /// key-value 键值对:value可以是序列化的数据
    /// </summary>
    public class RedisStringService : RedisBase
    {
        #region 构造函数

        /// <summary>
        /// 初始化Redis的String数据结构操作
        /// </summary>
        /// <param name="dbNum">操作的数据库索引0-64(需要在conf文件中配置)</param>
        public RedisStringService(int? dbNum = null) :
            base(dbNum)
        { }
        #endregion

        #region 同步方法
        /// <summary>
        /// 添加单个key value
        /// </summary>
        /// <param name="key">Redis Key</param>
        /// <param name="value">保存的值</param>
        /// <param name="expiry">过期时间</param>
        /// <returns></returns>
        public bool StringSet(string key, string value, TimeSpan? expiry = default(TimeSpan?))
        {
            key = AddSysCustomKey(key);
            return base.redis.StringSet(key, value, expiry);
        }

        /// <summary>
        /// 添加多个key/value
        /// </summary>
        /// <param name="valueList">key/value集合</param>
        /// <returns></returns>
        public bool StringSet(Dictionary<string, string> valueList)
        {
            var newkeyValues = valueList.Select(p => new KeyValuePair<RedisKey, RedisValue>(AddSysCustomKey(p.Key), p.Value)).ToArray();
            return base.redis.StringSet(newkeyValues);
        }

        /// <summary>
        /// 保存一个对象
        /// </summary>
        /// <typeparam name="T">对象类型</typeparam>
        /// <param name="key">保存的Key名称</param>
        /// <param name="value">对象实体</param>
        /// <param name="expiry">过期时间</param>
        /// <returns></returns>
        public bool StringSet<T>(string key, T value, TimeSpan? expiry = default(TimeSpan?))
        {
            key = AddSysCustomKey(key);
            string jsonValue = ConvertJson(value);
            return base.redis.StringSet(key, jsonValue, expiry);
        }

        /// <summary>
        /// 在原有key的value值之后追加value
        /// </summary>
        /// <param name="key">追加的Key名称</param>
        /// <param name="value">追加的值</param>
        /// <returns></returns>
        public long StringAppend(string key, string value)
        {
            key = AddSysCustomKey(key);
            return base.redis.StringAppend(key, value);
        }

        /// <summary>
        /// 获取单个key的值
        /// </summary>
        /// <param name="key">要读取的Key名称</param>
        /// <returns></returns>
        public string StringGet(string key)
        {
            key = AddSysCustomKey(key);
            return base.redis.StringGet(key);
        }

        /// <summary>
        /// 获取多个key的value值
        /// </summary>
        /// <param name="keys">要获取值的Key集合</param>
        /// <returns></returns>
        public List<string> StringGet(params string[] keys)
        {
            var newKeys = ConvertRedisKeysAddSysCustomKey(keys);
            var values = base.redis.StringGet(newKeys);
            return values.Select(o => o.ToString()).ToList();
        }


        /// <summary>
        /// 获取单个key的value值
        /// </summary>
        /// <typeparam name="T">返回数据类型</typeparam>
        /// <param name="key">要获取值的Key集合</param>
        /// <returns></returns>
        public T StringGet<T>(string key)
        {
            key = AddSysCustomKey(key);
            var values = base.redis.StringGet(key);
            return ConvertObj<T>(values);
        }

        /// <summary>
        /// 获取多个key的value值
        /// </summary>
        /// <typeparam name="T">返回数据类型</typeparam>
        /// <param name="keys">要获取值的Key集合</param>
        /// <returns></returns>
        public List<T> StringGet<T>(params string[] keys)
        {
            var newKeys = ConvertRedisKeysAddSysCustomKey(keys);
            var values = base.redis.StringGet(newKeys);
            return ConvetList<T>(values);
        }

        /// <summary>
        /// 获取旧值赋上新值
        /// </summary>
        /// <param name="key">Key名称</param>
        /// <param name="value">新值</param>
        /// <returns></returns>
        public string StringGetSet(string key, string value)
        {
            key = AddSysCustomKey(key);
            return base.redis.StringGetSet(key, value);
        }

        /// <summary>
        /// 获取旧值赋上新值
        /// </summary>
        /// <typeparam name="T">数据类型</typeparam>
        /// <param name="key">Key名称</param>
        /// <param name="value">新值</param>
        /// <returns></returns>
        public T StringGetSet<T>(string key, T value)
        {
            key = AddSysCustomKey(key);
            string jsonValue = ConvertJson(value);
            var oValue = base.redis.StringGetSet(key, jsonValue);
            return ConvertObj<T>(oValue);
        }


        /// <summary>
        /// 获取值的长度
        /// </summary>
        /// <param name="key">Key名称</param>
        /// <returns></returns>
        public long StringGetLength(string key)
        {
            key = AddSysCustomKey(key);
            return base.redis.StringLength(key);
        }

        /// <summary>
        /// 数字增长val，返回自增后的值
        /// </summary>
        /// <param name="key"></param>
        /// <param name="val">可以为负</param>
        /// <returns>增长后的值</returns>
        public double StringIncrement(string key, double val = 1)
        {
            key = AddSysCustomKey(key);
            return base.redis.StringIncrement(key, val);
        }

        /// <summary>
        /// 数字减少val，返回自减少的值
        /// </summary>
        /// <param name="key"></param>
        /// <param name="val">可以为负</param>
        /// <returns>减少后的值</returns>
        public double StringDecrement(string key, double val = 1)
        {
            key = AddSysCustomKey(key);
            return base.redis.StringDecrement(key, val);
        }

        #endregion

        #region 异步方法
        /// <summary>
        /// 异步方法 保存单个key value
        /// </summary>
        /// <param name="key">Redis Key</param>
        /// <param name="value">保存的值</param>
        /// <param name="expiry">过期时间</param>
        /// <returns></returns>
        public async Task<bool> StringSetAsync(string key, string value, TimeSpan? expiry = default(TimeSpan?))
        {
            key = AddSysCustomKey(key);
            return await base.redis.StringSetAsync(key, value, expiry);
        }
        /// <summary>
        /// 异步方法 添加多个key/value
        /// </summary>
        /// <param name="valueList">key/value集合</param>
        /// <returns></returns>
        public async Task<bool> StringSetAsync(Dictionary<string, string> valueList)
        {
            var newkeyValues = valueList.Select(p => new KeyValuePair<RedisKey, RedisValue>(AddSysCustomKey(p.Key), p.Value)).ToArray();
            return await base.redis.StringSetAsync(newkeyValues);
        }

        /// <summary>
        /// 异步方法 保存一个对象
        /// </summary>
        /// <typeparam name="T">对象类型</typeparam>
        /// <param name="key">保存的Key名称</param>
        /// <param name="obj">对象实体</param>
        /// <param name="expiry">过期时间</param>
        /// <returns></returns>
        public async Task<bool> StringSetAsync<T>(string key, T obj, TimeSpan? expiry = default(TimeSpan?))
        {
            key = AddSysCustomKey(key);
            string jsonValue = ConvertJson(obj);
            return await base.redis.StringSetAsync(key, jsonValue, expiry);
        }

        /// <summary>
        /// 异步方法 在原有key的value值之后追加value
        /// </summary>
        /// <param name="key">追加的Key名称</param>
        /// <param name="value">追加的值</param>
        /// <returns></returns>
        public async Task<long> StringAppendAsync(string key, string value)
        {
            key = AddSysCustomKey(key);
            return await base.redis.StringAppendAsync(key, value);
        }

        /// <summary>
        /// 异步方法 获取单个key的值
        /// </summary>
        /// <param name="key">要读取的Key名称</param>
        /// <returns></returns>
        public async Task<string> StringGetAsync(string key)
        {
            key = AddSysCustomKey(key);
            return await base.redis.StringGetAsync(key);
        }

        /// <summary>
        /// 异步方法 获取多个key的value值
        /// </summary>
        /// <param name="keys">要获取值的Key集合</param>
        /// <returns></returns>
        public async Task<List<string>> StringGetAsync(params string[] keys)
        {
            var newKeys = ConvertRedisKeysAddSysCustomKey(keys);
            var values = await base.redis.StringGetAsync(newKeys);
            return values.Select(o => o.ToString()).ToList();
        }


        /// <summary>
        /// 异步方法 获取单个key的value值
        /// </summary>
        /// <typeparam name="T">返回数据类型</typeparam>
        /// <param name="key">要获取值的Key集合</param>
        /// <returns></returns>
        public async Task<T> StringGetAsync<T>(string key)
        {
            key = AddSysCustomKey(key);
            var values = await base.redis.StringGetAsync(key);
            return ConvertObj<T>(values);
        }

        /// <summary>
        /// 异步方法 获取多个key的value值
        /// </summary>
        /// <typeparam name="T">返回数据类型</typeparam>
        /// <param name="keys">要获取值的Key集合</param>
        /// <returns></returns>
        public async Task<List<T>> StringGetAsync<T>(params string[] keys)
        {
            var newKeys = ConvertRedisKeysAddSysCustomKey(keys);
            var values = await base.redis.StringGetAsync(newKeys);
            return ConvetList<T>(values);
        }

        /// <summary>
        /// 异步方法 获取旧值赋上新值
        /// </summary>
        /// <param name="key">Key名称</param>
        /// <param name="value">新值</param>
        /// <returns></returns>
        public async Task<string> StringGetSetAsync(string key, string value)
        {
            key = AddSysCustomKey(key);
            return await base.redis.StringGetSetAsync(key, value);
        }

        /// <summary>
        /// 异步方法 获取旧值赋上新值
        /// </summary>
        /// <typeparam name="T">数据类型</typeparam>
        /// <param name="key">Key名称</param>
        /// <param name="value">新值</param>
        /// <returns></returns>
        public async Task<T> StringGetSetAsync<T>(string key, T value)
        {
            key = AddSysCustomKey(key);
            string jsonValue = ConvertJson(value);
            var oValue = await base.redis.StringGetSetAsync(key, jsonValue);
            return ConvertObj<T>(oValue);
        }


        /// <summary>
        /// 异步方法 获取值的长度
        /// </summary>
        /// <param name="key">Key名称</param>
        /// <returns></returns>
        public async Task<long> StringGetLengthAsync(string key)
        {
            key = AddSysCustomKey(key);
            return await base.redis.StringLengthAsync(key);
        }

        /// <summary>
        /// 异步方法 数字增长val，返回自增后的值
        /// </summary>
        /// <param name="key"></param>
        /// <param name="val">可以为负</param>
        /// <returns>增长后的值</returns>
        public async Task<double> StringIncrementAsync(string key, double val = 1)
        {
            key = AddSysCustomKey(key);
            return await base.redis.StringIncrementAsync(key, val);
        }

        /// <summary>
        /// 异步方法 数字减少val，返回自减少的值
        /// </summary>
        /// <param name="key"></param>
        /// <param name="val">可以为负</param>
        /// <returns>减少后的值</returns>
        public async Task<double> StringDecrementAsync(string key, double val = 1)
        {
            key = AddSysCustomKey(key);
            return await base.redis.StringDecrementAsync(key, val);
        }

        #endregion
    }
}
