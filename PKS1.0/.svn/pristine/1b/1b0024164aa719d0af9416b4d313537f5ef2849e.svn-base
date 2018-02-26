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
using System.Collections.Generic;
using TDocument = PKS.WebAPI.Models.IndexPageData;

namespace PKS.WebAPI.Services
{
    /// <summary>日志服务实现</summary>
    public class PageDataServiceWrapper : ApiServiceWrapper, IPageDataService, IPageDataServiceWrapper, ISingletonAppService
    {
        /// <summary>构造函数</summary>
        public PageDataServiceWrapper(string serviceUrl) : base(serviceUrl)
        {
        }

        /// <summary>构造函数</summary>
        public PageDataServiceWrapper(IApiServiceConfig config) : base(config, "PageService")
        {
        }
        /// <summary>上传文件，支持秒传和分片</summary>
        /// <remarks>
        /// 如果Guid为空表示不分片，否则表示分片
        /// </remarks>
        public UploadResult Upload(UploadRequest request)
        {
            return Task.Run(() => UploadAsync(request)).Result;
        }
        /// <summary>上传文件，支持秒传和分片</summary>
        /// <remarks>
        /// 如果Guid为空表示不分片，否则表示分片
        /// </remarks>
        public async Task<UploadResult> UploadAsync(UploadRequest request)
        {
            return await this.Client.PostObjectAsync<UploadResult>(GetActionUrl(nameof(Upload)), request).ConfigureAwait(false);
        }
        /// <summary>批量插入</summary>
        public string[] InsertMany(IndexDataSaveRequest<TDocument> request)
        {
            return Task.Run(() => InsertManyAsync(request)).Result;
        }
        /// <summary>批量插入</summary>
        public async Task<string[]> InsertManyAsync(IndexDataSaveRequest<TDocument> request)
        {
            return await this.Client.PostObjectAsync<string[]>(GetActionUrl(nameof(InsertMany)), request).ConfigureAwait(false);
        }
        /// <summary>批量保存</summary>
        public string[] SaveMany(IndexDataSaveRequest<TDocument> request)
        {
            return Task.Run(() => SaveManyAsync(request)).Result;
        }
        /// <summary>批量保存</summary>
        public async Task<string[]> SaveManyAsync(IndexDataSaveRequest<TDocument> request)
        {
            return await this.Client.PostObjectAsync<string[]>(GetActionUrl(nameof(SaveMany)), request).ConfigureAwait(false);
        }
        /// <summary>批量删除</summary>
        public string[] DeleteMany(List<string> pageIds)
        {
            return Task.Run(() => DeleteManyAsync(pageIds)).Result;
        }
        /// <summary>批量删除</summary>
        public async Task<string[]> DeleteManyAsync(List<string> pageIds)
        {
            return await this.Client.PostObjectAsync<string[]>(GetActionUrl(nameof(DeleteMany)), pageIds).ConfigureAwait(false);
        }
        /// <summary>根据PageID获得一条应用数据</summary>
        public TDocument Get(string pageId)
        {
            return Task.Run(() => GetAsync(pageId)).Result;
        }
        /// <summary>根据PageID获得一条应用数据</summary>
        public async Task<TDocument> GetAsync(string pageId)
        {
            var queryString = $"?{nameof(pageId)}={pageId}";
            return await this.Client.GetAsync<TDocument>(GetActionUrl(nameof(Get)) + queryString).ConfigureAwait(false);
        }
        /// <summary>根据PageID数组获得对应的多条应用数据</summary>
        public Dictionary<string, TDocument> GetMany(List<string> pageIds)
        {
            return Task.Run(() => GetManyAsync(pageIds)).Result;
        }
        /// <summary>根据PageID数组获得对应的多条应用数据</summary>
        public async Task<Dictionary<string, TDocument>> GetManyAsync(List<string> pageIds)
        {
            return await this.Client.PostObjectAsync<Dictionary<string, TDocument>>(GetActionUrl(nameof(GetMany)), pageIds).ConfigureAwait(false);
        }
        /// <summary>根据条件和分页参数获得应用数据集合</summary>
        public IndexDataMatchResult<TDocument> Match(IndexDataMatchRequest request)
        {
            return Task.Run(() => MatchAsync(request)).Result;
        }

        /// <summary>根据条件和分页参数获得应用数据集合</summary>
        public async Task<IndexDataMatchResult<TDocument>> MatchAsync(IndexDataMatchRequest request)
        {
            return await Client.PostObjectAsync<IndexDataMatchResult<TDocument>>(GetActionUrl(nameof(Match)), request).ConfigureAwait(false);
        }
        /// <summary>根据DataID或ContentRef获得相关文件流</summary>
        public DownloadResult Download(DownloadRequest request)
        {
            return Task.Run(() => DownloadAsync(request)).Result;
        }
        /// <summary>根据DataID或ContentRef获得相关文件流</summary>
        public async Task<DownloadResult> DownloadAsync(DownloadRequest request)
        {
            return await this.Client.PostObjectAsync<DownloadResult>(GetActionUrl(nameof(Download)), request).ConfigureAwait(false);
        }
    }
}