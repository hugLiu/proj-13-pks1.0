using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using CacheManager.Core;
using EventBus;
using Ninject;
using PKS.Core;
using PKS.Data;
using PKS.DBModels;
using PKS.Models;
using PKS.Utils;

namespace PKS.DbServices
{
    /// <summary>子系统配置</summary>
    public class PKSSubSystemConfig : AppService, IInitializable, IPKSSubSystemConfig
    {
        /// <summary>构造函数</summary>
        public PKSSubSystemConfig()
        {
            CurrentCode = ConfigurationManager.AppSettings[PKSWebConsts.AppSettings_SubSystem];
        }
        /// <summary>初始化</summary>
        void IInitializable.Initialize()
        {
            this.EventBus.Register(this);
        }
        /// <summary>当前子系统代码</summary>
        public string CurrentCode { get; }

        /// <summary>当前子系统信息</summary>
        public IPKSSubSystemInfo CurrentInfo => GetInfo(CurrentCode);

        /// <summary>获得某个子系统信息</summary>
        public IPKSSubSystemInfo GetInfo(string code)
        {
            return GetService<IRepository<PKS_SUBSYSTEM>>().GetQuery().FirstOrDefault(p => p.Code == code);
        }

        /// <summary>子系统URL，键是系统代码，值是URL</summary>
        public Dictionary<string, string> Urls
        {
            get { return this.MemcachedCacher.TryGetOrAddValue<Dictionary<string, string>>(CacheConst.SubSystemUrlsKey, CacheConst.SubSystemRegion, GetCacheItem_Urls); }
        }
        /// <summary>子系统URL，键是系统代码，值是URL</summary>
        private CacheItem<object> GetCacheItem_Urls(string key, string region)
        {
            var value = GetService<IRepository<PKS_SUBSYSTEM>>().GetQuery().ToDictionary(e => e.Code, e => e.RootUrl);
            return new CacheItem<object>(key, region, value, ExpirationMode.None, TimeSpan.Zero);
        }
        /// <summary>获得子系统URL</summary>
        public string GetUrl(string code)
        {
            return this.Urls[code];
        }
        /// <summary>处理变化事件</summary>
        [EventSubscriber]
        public void OnChanged(EntityChangedEventArgs<PKS_SUBSYSTEM> e)
        {
            this.MemcachedCacher.TryRemove(CacheConst.SubSystemUrlsKey, CacheConst.SubSystemRegion);
        }
    }
}