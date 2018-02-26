using System;
using System.Net.Security;
using System.Security.Principal;
using System.Threading.Tasks;
using PKS.Core;
using PKS.Models;
using PKS.Utils;
using PKS.Web;
using PKS.WebAPI.Models;
using PKS.WebAPI.Services;
using Newtonsoft.Json.Linq;

namespace PKS.WebAPI.Services
{
    /// <summary>搜索服务实现</summary>
    public class SearchServiceWrapper : ApiServiceWrapper, ISearchService, ISearchServiceWrapper, ISingletonAppService
    {
        /// <summary>构造函数</summary>
        public SearchServiceWrapper(string serviceUrl) : base(serviceUrl)
        {
        }

        /// <summary>构造函数</summary>
        public SearchServiceWrapper(IApiServiceConfig config) : base(config, nameof(ISearchService).Substring(1))
        {
        }
        /// <summary>按短语搜索</summary>
        public SearchResult Search(SearchRequest request)
        {
            return Task.Run(() => SearchAsync(request)).Result;
        }
        /// <summary>按短语搜索</summary>
        public async Task<SearchResult> SearchAsync(SearchRequest request)
        {
            return await this.Client.PostObjectAsync<SearchResult>(GetActionUrl(nameof(Search)), request).ConfigureAwait(false);
        }
        /// <summary>按ES语法搜索</summary>
        public string ESSearch(string request)
        {
            return Task.Run(() => ESSearchAsync(request)).Result;
        }
        /// <summary>按ES语法搜索</summary>
        public async Task<string> ESSearchAsync(string request)
        {
            return await this.Client.PostAsync<string>(GetActionUrl(nameof(ESSearch)), request).ConfigureAwait(false);
        }
        /// <summary>按完全匹配条件搜索</summary>
        public MatchResult Match(MatchRequest request)
        {
            return Task.Run(() => MatchAsync(request)).Result;
        }

        /// <summary>按完全匹配条件搜索</summary>
        public async Task<MatchResult> MatchAsync(MatchRequest request)
        {
            return await this.Client.PostObjectAsync<MatchResult>(GetActionUrl(nameof(Match)), request).ConfigureAwait(false);
        }
        /// <summary>按多个完全匹配条件搜索</summary>
        public MatchResult[] MatchMany(MatchRequest[] request)
        {
            return Task.Run(() => MatchManyAsync(request)).Result;
        }

        /// <summary>按多个完全匹配条件搜索</summary>
        public async Task<MatchResult[]> MatchManyAsync(MatchRequest[] request)
        {
            return await this.Client.PostObjectAsync<MatchResult[]>(GetActionUrl(nameof(Match)), request).ConfigureAwait(false);
        }
        /// <summary>根据iiid搜索</summary>
        public Metadata GetMetadata(SearchMetadataRequest request)
        {
            return Task.Run(() => GetMetadataAsync(request)).Result;
        }

        /// <summary>根据iiid搜索</summary>
        public async Task<Metadata> GetMetadataAsync(SearchMetadataRequest request)
        {
            return await this.Client.PostObjectAsync<Metadata>(GetActionUrl(nameof(GetMetadata)), request).ConfigureAwait(false);
        }
        /// <summary>根据iiid数组搜索</summary>
        public MetadataCollection GetMetadatas(SearchMetadatasRequest request)
        {
            return Task.Run(() => GetMetadatasAsync(request)).Result;
        }

        /// <summary>根据iiid数组搜索</summary>
        public async Task<MetadataCollection> GetMetadatasAsync(SearchMetadatasRequest request)
        {
            return await this.Client.PostObjectAsync<MetadataCollection>(GetActionUrl(nameof(GetMetadatas)), request).ConfigureAwait(false);
        }
        /// <summary>根据聚合条件获取统计信息</summary>
        public SearchStatisticsResult Statistics(SearchStatisticsRequest request)
        {
            return Task.Run(() => StatisticsAsync(request)).Result;
        }

        /// <summary>根据聚合条件获取统计信息</summary>
        public async Task<SearchStatisticsResult> StatisticsAsync(SearchStatisticsRequest request)
        {
            return await this.Client.PostObjectAsync<SearchStatisticsResult>(GetActionUrl(nameof(Statistics)), request).ConfigureAwait(false);
        }
        /// <summary>查询元数据定义信息</summary>
        public MetadataDefinition[] GetMetadataDefinitions()
        {
            return Task.Run(() => GetMetadataDefinitionsAsync()).Result;
        }

        /// <summary>查询元数据定义信息</summary>
        public async Task<MetadataDefinition[]> GetMetadataDefinitionsAsync()
        {
            return await this.Client.GetAsync<MetadataDefinition[]>(GetActionUrl(nameof(GetMetadataDefinitions))).ConfigureAwait(false);
        }
        /// <summary>ES搜索</summary>
        public object ESSearchEx(object query)
        {
            return Task.Run(() => ESSearchExAsync(query)).Result;
        }
        /// <summary>ES搜索</summary>
        public async Task<object> ESSearchExAsync(object query)
        {
            return await this.Client.PostObjectAsync<JObject>(GetActionUrl(nameof(ESSearchEx)), query).ConfigureAwait(false);
        }
    }
}