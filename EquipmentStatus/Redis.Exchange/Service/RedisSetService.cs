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
    /// Set：用哈希表来保持字符串的唯一性，没有先后顺序，存储一些集合性的数据
    /// 1.共同好友、二度好友
    /// 2.利用唯一性，可以统计访问网站的所有独立 IP
    /// </summary>
    public class RedisSetService : RedisBase
    {
        #region 构造函数

        /// <summary>
        /// 初始化Redis的Set无序数据结构操作
        /// </summary>
        /// <param name="dbNum">操作的数据库索引0-64(需要在conf文件中配置)</param>
        public RedisSetService(int? dbNum = null) :
            base(dbNum)
        { }
        #endregion

        #region 同步方法
        /// <summary>
        /// 在Key集合中添加一个value值
        /// </summary>
        /// <typeparam name="T">数据类型</typeparam>
        /// <param name="key">Key名称</param>
        /// <param name="value">值</param>
        /// <returns></returns>
        public bool SetAdd<T>(string key, T value)
        {
            key = AddSysCustomKey(key);
            string jValue = ConvertJson(value);
            return base.redis.SetAdd(key, jValue);
        }
        /// <summary>
        /// 在Key集合中添加多个value值
        /// </summary>
        /// <typeparam name="T">数据类型</typeparam>
        /// <param name="key">Key名称</param>
        /// <param name="value">值列表</param>
        /// <returns></returns>
        public long SetAdd<T>(string key, List<T> value)
        {
            key = AddSysCustomKey(key);
            RedisValue[] valueList = base.ConvertRedisValue(value.ToArray());
            return base.redis.SetAdd(key, valueList);
        }

        /// <summary>
        /// 获取key集合值的数量
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public long SetLength(string key)
        {
            key = AddSysCustomKey(key);
            return base.redis.SetLength(key);
        }

        /// <summary>
        /// 判断Key集合中是否包含指定的值
        /// </summary>
        /// <typeparam name="T">值类型</typeparam>
        /// <param name="key"></param>
        /// <param name="value">要判断是值</param>
        /// <returns></returns>
        public bool SetContains<T>(string key, T value)
        {
            key = AddSysCustomKey(key);
            string jValue = ConvertJson(value);
            return base.redis.SetContains(key, jValue);
        }

        /// <summary>
        /// 随机获取key集合中的一个值
        /// </summary>
        /// <typeparam name="T">数据类型</typeparam>
        /// <param name="key"></param>
        /// <returns></returns>
        public T SetRandomMember<T>(string key)
        {
            key = AddSysCustomKey(key);
            var rValue = base.redis.SetRandomMember(key);
            return ConvertObj<T>(rValue);
        }

        /// <summary>
        /// 获取key所有值的集合
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <returns></returns>
        public List<T> SetMembers<T>(string key)
        {
            key = AddSysCustomKey(key);
            var rValue = base.redis.SetMembers(key);
            return ConvetList<T>(rValue);
        }

        /// <summary>
        /// 删除key集合中指定的value
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public long SetRemove<T>(string key, params T[] value)
        {
            key = AddSysCustomKey(key);
            RedisValue[] valueList = base.ConvertRedisValue(value);
            return base.redis.SetRemove(key, valueList);
        }

        /// <summary>
        /// 随机删除key集合中的一个值，并返回该值
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <returns></returns>
        public T SetPop<T>(string key)
        {
            key = AddSysCustomKey(key);
            var rValue = base.redis.SetPop(key);
            return ConvertObj<T>(rValue);
        }


        /// <summary>
        /// 获取几个集合的并集
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="keys">要操作的Key集合</param>
        /// <returns></returns>
        public List<T> SetCombineUnion<T>(params string[] keys)
        {
            return _SetCombine<T>(SetOperation.Union, keys);
        }
        /// <summary>
        /// 获取几个集合的交集
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="keys">要操作的Key集合</param>
        /// <returns></returns>
        public List<T> SetCombineIntersect<T>(params string[] keys)
        {
            return _SetCombine<T>(SetOperation.Intersect, keys);
        }
        /// <summary>
        /// 获取几个集合的差集
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="keys">要操作的Key集合</param>
        /// <returns></returns>
        public List<T> SetCombineDifference<T>(params string[] keys)
        {
            return _SetCombine<T>(SetOperation.Difference, keys);
        }

        /// <summary>
        /// 获取几个集合的并集,并保存到一个新Key中
        /// </summary>
        /// <param name="destination">保存的新Key名称</param>
        /// <param name="keys">要操作的Key集合</param>
        /// <returns></returns>
        public long SetCombineUnionAndStore(string destination, params string[] keys)
        {
            return _SetCombineAndStore(SetOperation.Union, destination, keys);
        }
        /// <summary>
        /// 获取几个集合的交集,并保存到一个新Key中
        /// </summary>
        /// <param name="destination">保存的新Key名称</param>
        /// <param name="keys">要操作的Key集合</param>
        /// <returns></returns>
        public long SetCombineIntersectAndStore(string destination, params string[] keys)
        {
            return _SetCombineAndStore(SetOperation.Intersect, destination, keys);
        }
        /// <summary>
        /// 获取几个集合的差集,并保存到一个新Key中
        /// </summary>
        /// <param name="destination">保存的新Key名称</param>
        /// <param name="keys">要操作的Key集合</param>
        /// <returns></returns>
        public long SetCombineDifferenceAndStore(string destination, params string[] keys)
        {
            return _SetCombineAndStore(SetOperation.Difference, destination, keys);
        }
        #endregion

        #region 异步方法
        /// <summary>
        /// 在Key集合中添加一个value值
        /// </summary>
        /// <typeparam name="T">数据类型</typeparam>
        /// <param name="key">Key名称</param>
        /// <param name="value">值</param>
        /// <returns></returns>
        public async Task<bool> SetAddAsync<T>(string key, T value)
        {
            key = AddSysCustomKey(key);
            string jValue = ConvertJson(value);
            return await base.redis.SetAddAsync(key, jValue);
        }
        /// <summary>
        /// 在Key集合中添加多个value值
        /// </summary>
        /// <typeparam name="T">数据类型</typeparam>
        /// <param name="key">Key名称</param>
        /// <param name="value">值列表</param>
        /// <returns></returns>
        public async Task<long> SetAddAsync<T>(string key, List<T> value)
        {
            key = AddSysCustomKey(key);
            RedisValue[] valueList = base.ConvertRedisValue(value.ToArray());
            return await base.redis.SetAddAsync(key, valueList);
        }

        /// <summary>
        /// 获取key集合值的数量
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public async Task<long> SetLengthAsync(string key)
        {
            key = AddSysCustomKey(key);
            return await base.redis.SetLengthAsync(key);
        }

        /// <summary>
        /// 判断Key集合中是否包含指定的值
        /// </summary>
        /// <typeparam name="T">值类型</typeparam>
        /// <param name="key"></param>
        /// <param name="value">要判断是值</param>
        /// <returns></returns>
        public async Task<bool> SetContainsAsync<T>(string key, T value)
        {
            key = AddSysCustomKey(key);
            string jValue = ConvertJson(value);
            return await base.redis.SetContainsAsync(key, jValue);
        }

        /// <summary>
        /// 随机获取key集合中的一个值
        /// </summary>
        /// <typeparam name="T">数据类型</typeparam>
        /// <param name="key"></param>
        /// <returns></returns>
        public async Task<T> SetRandomMemberAsync<T>(string key)
        {
            key = AddSysCustomKey(key);
            var rValue = await base.redis.SetRandomMemberAsync(key);
            return ConvertObj<T>(rValue);
        }

        /// <summary>
        /// 获取key所有值的集合
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <returns></returns>
        public async Task<List<T>> SetMembersAsync<T>(string key)
        {
            key = AddSysCustomKey(key);
            var rValue = await base.redis.SetMembersAsync(key);
            return ConvetList<T>(rValue);
        }

        /// <summary>
        /// 删除key集合中指定的value
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public async Task<long> SetRemoveAsync<T>(string key, params T[] value)
        {
            key = AddSysCustomKey(key);
            RedisValue[] valueList = base.ConvertRedisValue(value);
            return await base.redis.SetRemoveAsync(key, valueList);
        }

        /// <summary>
        /// 随机删除key集合中的一个值，并返回该值
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <returns></returns>
        public async Task<T> SetPopAsync<T>(string key)
        {
            key = AddSysCustomKey(key);
            var rValue = await base.redis.SetPopAsync(key);
            return ConvertObj<T>(rValue);
        }


        /// <summary>
        /// 获取几个集合的并集
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="keys">要操作的Key集合</param>
        /// <returns></returns>
        public async Task<List<T>> SetCombineUnionAsync<T>(params string[] keys)
        {
            return await _SetCombineAsync<T>(SetOperation.Union, keys);
        }
        /// <summary>
        /// 获取几个集合的交集
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="keys">要操作的Key集合</param>
        /// <returns></returns>
        public async Task<List<T>> SetCombineIntersectAsync<T>(params string[] keys)
        {
            return await _SetCombineAsync<T>(SetOperation.Intersect, keys);
        }
        /// <summary>
        /// 获取几个集合的差集
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="keys">要操作的Key集合</param>
        /// <returns></returns>
        public async Task<List<T>> SetCombineDifferenceAsync<T>(params string[] keys)
        {
            return await _SetCombineAsync<T>(SetOperation.Difference, keys);
        }



        /// <summary>
        /// 获取几个集合的并集,并保存到一个新Key中
        /// </summary>
        /// <param name="destination">保存的新Key名称</param>
        /// <param name="keys">要操作的Key集合</param>
        /// <returns></returns>
        public async Task<long> SetCombineUnionAndStoreAsync(string destination, params string[] keys)
        {
            return await _SetCombineAndStoreAsync(SetOperation.Union, destination, keys);
        }
        /// <summary>
        /// 获取几个集合的交集,并保存到一个新Key中
        /// </summary>
        /// <param name="destination">保存的新Key名称</param>
        /// <param name="keys">要操作的Key集合</param>
        /// <returns></returns>
        public async Task<long> SetCombineIntersectAndStoreAsync(string destination, params string[] keys)
        {
            return await _SetCombineAndStoreAsync(SetOperation.Intersect, destination, keys);
        }
        /// <summary>
        /// 获取几个集合的差集,并保存到一个新Key中
        /// </summary>
        /// <param name="destination">保存的新Key名称</param>
        /// <param name="keys">要操作的Key集合</param>
        /// <returns></returns>
        public async Task<long> SetCombineDifferenceAndStoreAsync(string destination, params string[] keys)
        {
            return await _SetCombineAndStoreAsync(SetOperation.Difference, destination, keys);
        }

        #endregion

        #region 内部辅助方法
        /// <summary>
        /// 获取几个集合的交叉并集合
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="operation">Union：并集  Intersect：交集  Difference：差集  详见 <see cref="SetOperation"/></param>
        /// <param name="keys">要操作的Key集合</param>
        /// <returns></returns>
        private List<T> _SetCombine<T>(SetOperation operation, params string[] keys)
        {
            RedisKey[] keyList = base.ConvertRedisKeysAddSysCustomKey(keys);
            var rValue = base.redis.SetCombine(operation, keyList);
            return ConvetList<T>(rValue);
        }

        /// <summary>
        /// 获取几个集合的交叉并集合,并保存到一个新Key中
        /// </summary>
        /// <param name="operation">Union：并集  Intersect：交集  Difference：差集  详见 <see cref="SetOperation"/></param>
        /// <param name="destination">保存的新Key名称</param>
        /// <param name="keys">要操作的Key集合</param>
        /// <returns></returns>
        private long _SetCombineAndStore(SetOperation operation, string destination, params string[] keys)
        {
            destination = AddSysCustomKey(destination);
            RedisKey[] keyList = base.ConvertRedisKeysAddSysCustomKey(keys);
            return base.redis.SetCombineAndStore(operation, destination, keyList);
        }
        /// <summary>
        /// 获取几个集合的交叉并集合
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="operation">Union：并集  Intersect：交集  Difference：差集  详见 <see cref="SetOperation"/></param>
        /// <param name="keys">要操作的Key集合</param>
        /// <returns></returns>
        private async Task<List<T>> _SetCombineAsync<T>(SetOperation operation, params string[] keys)
        {
            RedisKey[] keyList = base.ConvertRedisKeysAddSysCustomKey(keys);
            var rValue = await base.redis.SetCombineAsync(operation, keyList);
            return ConvetList<T>(rValue);
        }
        /// <summary>
        /// 获取几个集合的交叉并集合,并保存到一个新Key中
        /// </summary>
        /// <param name="operation">Union：并集  Intersect：交集  Difference：差集  详见 <see cref="SetOperation"/></param>
        /// <param name="destination">保存的新Key名称</param>
        /// <param name="keys">要操作的Key集合</param>
        /// <returns></returns>
        private async Task<long> _SetCombineAndStoreAsync(SetOperation operation, string destination, params string[] keys)
        {
            destination = AddSysCustomKey(destination);
            RedisKey[] keyList = base.ConvertRedisKeysAddSysCustomKey(keys);
            return await base.redis.SetCombineAndStoreAsync(operation, destination, keyList);
        }

        #endregion

    }
}
