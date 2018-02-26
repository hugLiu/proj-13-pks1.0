using System.Collections.Generic;
using System.Threading.Tasks;
using PKS.Core;
using PKS.WebAPI.Models;
using TIndexType = PKS.Models.Metadata;

namespace PKS.WebAPI.Services
{
    /// <summary>索引数据服务接口</summary>
    public class IndexerServiceWrapper : ApiServiceWrapper, IIndexerService, ISingletonAppService
    {
        /// <summary>构造函数</summary>
        public IndexerServiceWrapper(string serviceUrl) : base(serviceUrl)
        {
        }

        /// <summary>构造函数</summary>
        public IndexerServiceWrapper(IApiServiceConfig config) : base(config, nameof(IIndexerService).Substring(1))
        {
        }

        /// <summary>插入</summary>
        public string[] Insert(IndexInsertRequest request)
        {
            return Task.Run(() => InsertAsync(request)).Result;
        }

        /// <summary>插入</summary>
        public async Task<string[]> InsertAsync(IndexInsertRequest request)
        {
            return await Client.PostObjectAsync<string[]>(GetActionUrl(nameof(Insert)), request).ConfigureAwait(false);
        }

        /// <summary>保存</summary>
        public string[] Save(IndexSaveRequest request)
        {
            return Task.Run(() => SaveAsync(request)).Result;
        }

        /// <summary>保存</summary>
        public async Task<string[]> SaveAsync(IndexSaveRequest request)
        {
            return await Client.PostObjectAsync<string[]>(GetActionUrl(nameof(Save)), request).ConfigureAwait(false);
        }

        /// <summary>删除</summary>
        public string[] Delete(List<string> iiids)
        {
            return Task.Run(() => DeleteAsync(iiids)).Result;
        }

        /// <summary>删除</summary>
        public async Task<string[]> DeleteAsync(List<string> iiids)
        {
            return await Client.PostObjectAsync<string[]>(GetActionUrl(nameof(Delete)), iiids).ConfigureAwait(false);
        }
    }
}