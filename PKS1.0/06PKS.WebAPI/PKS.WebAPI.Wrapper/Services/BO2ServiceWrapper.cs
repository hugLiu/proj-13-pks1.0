using PKS.WebAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PKS.Core;
#pragma warning disable 1591

namespace PKS.WebAPI.Services
{
    /// <summary>对象服务包装器</summary>
    public class BO2ServiceWrapper : ApiServiceWrapper, IBO2Service, ISingletonAppService
    {
        /// <summary>构造函数</summary>
        public BO2ServiceWrapper(string serviceUrl) : base(serviceUrl)
        {
        }

        /// <summary>构造函数</summary>
        public BO2ServiceWrapper(IApiServiceConfig config) : base(config, nameof(IBO2Service).Substring(1))
        {
        }
        /// <summary>根据类型名称和对象名称获得对象</summary>
        public BO2 GetBO(string bot, string bo)
        {
            return Task.Run(() => GetBOAsync(bot, bo)).Result;
        }
        /// <summary>根据类型名称和对象名称获得对象</summary>
        public async Task<BO2> GetBOAsync(string bot, string bo)
        {
            var queryString = $"?{nameof(bot)}={bot}&{nameof(bo)}={bo}";
            return await Client.GetAsync<BO2>(GetActionUrl(nameof(GetBO)) + queryString).ConfigureAwait(false);
        }
        /// <summary>根据类型名称获得对象类型</summary>
        public BOT GetBOT(string bot)
        {
            return Task.Run(() => GetBOTAsync(bot)).Result;
        }
        /// <summary>根据类型名称获得对象类型</summary>
        public async Task<BOT> GetBOTAsync(string bot)
        {
            var queryString = $"?{nameof(bot)}={bot}";
            return await Client.GetAsync<BOT>(GetActionUrl(nameof(GetBOT)) + queryString).ConfigureAwait(false);
        }

        /// <summary>根据类型名称和对象名称获得对象集合</summary>
        public List<BO2> FindBOs(string bot, string bo)
        {
            return Task.Run(() => FindBOsAsync(bot, bo)).Result;
        }
        /// <summary>根据类型名称和对象名称获得对象集合</summary>
        public async Task<List<BO2>> FindBOsAsync(string bot, string bo)
        {
            var queryString = $"?{nameof(bot)}={bot}&{nameof(bo)}={bo}";
            return await Client.GetAsync<List<BO2>>(GetActionUrl(nameof(FindBOs)) + queryString).ConfigureAwait(false);
        }
        /// <summary>根据条件获得对象集合</summary>
        public List<BO2> FilterBOs(FilterRequest request)
        {
            return Task.Run(() => FilterBOsAsync(request)).Result;
        }
        /// <summary>根据条件获得对象集合</summary>
        public async Task<List<BO2>> FilterBOsAsync(FilterRequest request)
        {
            return await Client.PostObjectAsync<List<BO2>>(GetActionUrl(nameof(FilterBOs)), request).ConfigureAwait(false);
        }
        /// <summary>根据条件获得对象类型集合</summary>
        public List<BOT> FilterBOTs(FilterRequest request)
        {
            return Task.Run(() => FilterBOTsAsync(request)).Result;
        }
        /// <summary>根据条件获得对象类型集合</summary>
        public async Task<List<BOT>> FilterBOTsAsync(FilterRequest request)
        {
            return await Client.PostObjectAsync<List<BOT>>(GetActionUrl(nameof(FilterBOTs)), request).ConfigureAwait(false);
        }
        /// <summary>插入对象类型集合</summary>
        public void InsertBOTs(List<BOT> bots)
        {
            Task.Run(() => InsertBOTsAsync(bots));
        }
        /// <summary>插入对象类型集合</summary>
        public async Task InsertBOTsAsync(List<BOT> bots)
        {
            await Client.PostObjectAsync(GetActionUrl(nameof(InsertBOTs)), bots).ConfigureAwait(false);
        }
        /// <summary>保存对象类型集合</summary>
        public object SaveBOTs(List<BOT> bots)
        {
            return Task.Run(() => SaveBOTsAsync(bots)).Result;
        }
        /// <summary>保存对象类型集合</summary>
        public async Task<object> SaveBOTsAsync(List<BOT> bots)
        {
            return await Client.PostObjectAsync(GetActionUrl(nameof(SaveBOTs)), bots).ConfigureAwait(false);
        }
        /// <summary>删除对象类型集合</summary>
        public object DeleteBOTs(List<string> bots)
        {
            return Task.Run(() => DeleteBOTsAsync(bots)).Result;
        }
        /// <summary>删除对象类型集合</summary>
        public async Task<object> DeleteBOTsAsync(List<string> bots)
        {
            return await Client.PostObjectAsync(GetActionUrl(nameof(DeleteBOTs)), bots).ConfigureAwait(false);
        }
        /// <summary>插入对象集合</summary>
        public void InsertBOs(List<BO2> bos)
        {
            Task.Run(() => InsertBOsAsync(bos));
        }
        /// <summary>插入对象集合</summary>
        public async Task InsertBOsAsync(List<BO2> bos)
        {
            await Client.PostObjectAsync(GetActionUrl(nameof(InsertBOs)), bos).ConfigureAwait(false);
        }
        /// <summary>保存对象集合</summary>
        public object SaveBOs(List<BO2> bos)
        {
            return Task.Run(() => SaveBOsAsync(bos)).Result;
        }
        /// <summary>保存对象集合</summary>
        public async Task<object> SaveBOsAsync(List<BO2> bos)
        {
            return await Client.PostObjectAsync(GetActionUrl(nameof(SaveBOs)), bos).ConfigureAwait(false);
        }
        /// <summary>删除对象集合</summary>
        public object DeleteBOs(BO2DeleteRequest request)
        {
            return Task.Run(() => DeleteBOsAsync(request)).Result;
        }
        /// <summary>删除对象集合</summary>
        public async Task<object> DeleteBOsAsync(BO2DeleteRequest request)
        {
            return await Client.PostObjectAsync(GetActionUrl(nameof(DeleteBOs)), request).ConfigureAwait(false);
        }

        public long CountBOs(object query)
        {
            return Task.Run(() => CountBOsAsync(query)).Result;
        }

        public async Task<long> CountBOsAsync(object query)
        {
            return (long)await Client.PostObjectAsync(GetActionUrl(nameof(CountBOs)), query).ConfigureAwait(false);
        }

        public long CountBOTs(object query)
        {
            return Task.Run(() => CountBOTsAsync(query)).Result;
        }

        public async Task<long> CountBOTsAsync(object query)
        {
            return (long)await Client.PostObjectAsync(GetActionUrl(nameof(CountBOTs)), query).ConfigureAwait(false);
        }

        public object Near(NearRequest request)
        {
            return Task.Run(() => NearAsync(request)).Result;
        }

        public async Task<object> NearAsync(NearRequest request)
        {
            return await Client.PostObjectAsync(GetActionUrl(nameof(Near)), request).ConfigureAwait(false);
        }
    }
}
