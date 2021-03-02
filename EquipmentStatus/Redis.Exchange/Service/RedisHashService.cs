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
    /// Hash:类似dictionary，通过索引快速定位到指定元素的，耗时均等，跟string的区别在于不用反序列化，直接修改某个字段
    /// string的话要么是 001:序列化整个实体
    ///           要么是 001_name:  001_pwd: 多个key-value
    /// Hash的话，一个hashid-{key:value;key:value;key:value;}
    /// 可以一次性查找实体，也可以单个，还可以单个修改
    /// </summary>
    public class RedisHashService : RedisBase
    {

        #region 构造函数

        /// <summary>
        /// 初始化Redis的Hash数据结构操作
        /// </summary>
        /// <param name="dbNum">操作的数据库索引0-64(需要在conf文件中配置)</param>
        public RedisHashService(int? dbNum = null) :
            base(dbNum)
        { }
        #endregion

        #region 同步方法

        /// <summary>
        /// 判断某个数据是否已经被缓存
        /// </summary>
        /// <param name="key"></param>
        /// <param name="dataKey"></param>
        /// <returns></returns>
        public bool HashExists(string key, string dataKey)
        {
            key = AddSysCustomKey(key);
            return base.redis.HashExists(key, dataKey);
        }

        /// <summary>
        /// 存储数据到hash表
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="dataKey"></param>
        /// <param name="t"></param>
        /// <returns></returns>
        public bool HashSet<T>(string key, string dataKey, T t)
        {
            key = AddSysCustomKey(key);
            string json = ConvertJson(t);
            return base.redis.HashSet(key, dataKey, json);
        }

        /// <summary>
        /// 移除hash中的某值
        /// </summary>
        /// <param name="key"></param>
        /// <param name="dataKey"></param>
        /// <returns></returns>
        public bool HashDelete(string key, string dataKey)
        {
            key = AddSysCustomKey(key);
            return base.redis.HashDelete(key, dataKey);
        }

        /// <summary>
        /// 移除hash中的多个值
        /// </summary>
        /// <param name="key"></param>
        /// <param name="dataKeys"></param>
        /// <returns></returns>
        public long HashDelete(string key, params string[] dataKeys)
        {
            key = AddSysCustomKey(key);
            var newValues = dataKeys.Select(o => (RedisValue)o).ToArray();
            return base.redis.HashDelete(key, newValues);
        }

        /// <summary>
        /// 从hash表获取数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="dataKey"></param>
        /// <returns></returns>
        public T HashGet<T>(string key, string dataKey)
        {
            key = AddSysCustomKey(key);
            string value = base.redis.HashGet(key, dataKey);
            return ConvertObj<T>(value);
        }

        /// <summary>
        /// 数字增长val，返回自增后的值
        /// </summary>
        /// <param name="key"></param>
        /// <param name="dataKey"></param>
        /// <param name="val">可以为负</param>
        /// <returns>增长后的值</returns>
        public double HashIncrement(string key, string dataKey, double val = 1)
        {
            key = AddSysCustomKey(key);
            return base.redis.HashIncrement(key, dataKey, val);
        }

        /// <summary>
        /// 数字减少val，返回自减少的值
        /// </summary>
        /// <param name="key"></param>
        /// <param name="dataKey"></param>
        /// <param name="val">可以为负</param>
        /// <returns>减少后的值</returns>
        public double HashDecrement(string key, string dataKey, double val = 1)
        {
            key = AddSysCustomKey(key);
            return base.redis.HashDecrement(key, dataKey, val);
        }

        /// <summary>
        /// 获取hashkey所有key名称
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public string[] HashKeys(string key)
        {
            key = AddSysCustomKey(key);
            RedisValue[] values = base.redis.HashKeys(key);
            return values.Select(o=>o.ToString()).ToArray();
        }

        /// <summary>
        /// 获取hashkey所有key与值，必须保证Key内的所有数据类型一致
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <returns></returns>
        public Dictionary<string, T> HashGetAll<T>(string key)
        {
            key = AddSysCustomKey(key);
            var query = base.redis.HashGetAll(key);
            Dictionary<string, T> dic = new Dictionary<string, T>();
            foreach (var item in query)
            {
                dic.Add(item.Name, ConvertObj<T>(item.Value));
            }
            return dic;
        }

        #endregion 同步方法

        #region 异步方法

        /// <summary>
        /// 异步方法 判断某个数据是否已经被缓存
        /// </summary>
        /// <param name="key"></param>
        /// <param name="dataKey"></param>
        /// <returns></returns>
        public async Task<bool> HashExistsAsync(string key, string dataKey)
        {
            key = AddSysCustomKey(key);
            return await base.redis.HashExistsAsync(key, dataKey);
        }

        /// <summary>
        /// 异步方法 存储数据到hash表
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="dataKey"></param>
        /// <param name="t"></param>
        /// <returns></returns>
        public async Task<bool> HashSetAsync<T>(string key, string dataKey, T t)
        {
            key = AddSysCustomKey(key);
            string json = ConvertJson(t);
            return await base.redis.HashSetAsync(key, dataKey, json);
        }

        /// <summary>
        /// 异步方法 移除hash中的某值
        /// </summary>
        /// <param name="key"></param>
        /// <param name="dataKey"></param>
        /// <returns></returns>
        public async Task<bool> HashDeleteAsync(string key, string dataKey)
        {
            key = AddSysCustomKey(key);
            return await base.redis.HashDeleteAsync(key, dataKey);
        }

        /// <summary>
        /// 异步方法 移除hash中的多个值
        /// </summary>
        /// <param name="key"></param>
        /// <param name="dataKeys"></param>
        /// <returns></returns>
        public async Task<long> HashDeleteAsync(string key, params string[] dataKeys)
        {
            key = AddSysCustomKey(key);
            var newValues = dataKeys.Select(o => (RedisValue)o).ToArray();
            return await base.redis.HashDeleteAsync(key, newValues);
        }

        /// <summary>
        /// 异步方法 从hash表获取数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="dataKey"></param>
        /// <returns></returns>
        public async Task<T> HashGetAsync<T>(string key, string dataKey)
        {
            key = AddSysCustomKey(key);
            string value = await base.redis.HashGetAsync(key, dataKey);
            return ConvertObj<T>(value);
        }

        /// <summary>
        /// 异步方法 数字增长val，返回自增后的值
        /// </summary>
        /// <param name="key"></param>
        /// <param name="dataKey"></param>
        /// <param name="val">可以为负</param>
        /// <returns>增长后的值</returns>
        public async Task<double> HashIncrementAsync(string key, string dataKey, double val = 1)
        {
            key = AddSysCustomKey(key);
            return await base.redis.HashIncrementAsync(key, dataKey, val);
        }

        /// <summary>
        /// 异步方法 数字减少val，返回自减少的值
        /// </summary>
        /// <param name="key"></param>
        /// <param name="dataKey"></param>
        /// <param name="val">可以为负</param>
        /// <returns>减少后的值</returns>
        public async Task<double> HashDecrementAsync(string key, string dataKey, double val = 1)
        {
            key = AddSysCustomKey(key);
            return await base.redis.HashDecrementAsync(key, dataKey, val);
        }

        /// <summary>
        /// 异步方法 获取hashkey所有key名称
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public async Task<string[]> HashKeysAsync(string key)
        {
            key = AddSysCustomKey(key);
            RedisValue[] values = await base.redis.HashKeysAsync(key);
            return values.Select(o => o.ToString()).ToArray();
        }

        /// <summary>
        /// 获取hashkey所有key与值，必须保证Key内的所有数据类型一致
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <returns></returns>
        public async Task<Dictionary<string, T>> HashGetAllAsync<T>(string key)
        {
            key = AddSysCustomKey(key);
            var query = await base.redis.HashGetAllAsync(key);
            Dictionary<string, T> dic = new Dictionary<string, T>();
            foreach (var item in query)
            {
                dic.Add(item.Name, ConvertObj<T>(item.Value));
            }
            return dic;
        }

        #endregion 异步方法

    }
}
