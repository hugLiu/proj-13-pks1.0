using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using PKS.Core;
using PKS.Web;
using PKS.WebAPI.Models;
using System.Web;
using PKS.Models;
using PKS.Services;
using TDocument = PKS.WebAPI.Models.IndexAppData;
using TDataSave = PKS.WebAPI.Models.AppDataSaveRequest;
using System.IO;
using PKS.Utils;
using System.Linq;

namespace PKS.WebAPI.Services
{
    /// <summary>应用数据服务包装器</summary>
    public class AppDataServiceWrapper : ApiServiceWrapper, IAppDataServiceWrapper, IFileFormatService, ISingletonAppService
    {
        /// <summary>构造函数</summary>
        public AppDataServiceWrapper(string serviceUrl) : base(serviceUrl)
        {
        }

        /// <summary>构造函数</summary>
        public AppDataServiceWrapper(IApiServiceConfig config) : base(config, nameof(IAppDataService).Substring(1))
        {
        }

        /// <summary>上传文件，支持秒传和分片</summary>
        public UploadResult Upload(UploadRequest request)
        {
            return Task.Run(() => UploadAsync(request)).Result;
        }

        /// <summary>上传文件，支持秒传和分片</summary>
        public async Task<UploadResult> UploadAsync(UploadRequest request)
        {
            return await Client.PostObjectAsync<UploadResult>(GetActionUrl(nameof(Upload)), request).ConfigureAwait(false);
        }

        /// <summary>上传一个文件，支持秒传和分片(单位为K)</summary>
        public string Upload(string file, string charSet, int chunkSize)
        {
            return Task.Run(() => UploadAsync(file, charSet, chunkSize)).Result;
        }

        /// <summary>上传一个文件，支持秒传和分片(单位为K)</summary>
        public async Task<string> UploadAsync(string file, string charSet, int chunkSize)
        {
            using (var stream = File.OpenRead(file))
            {
                var request = new UploadRequest();
                var md5 = stream.ToByteArray().ToMD5();
                request.Md5 = new string[] { md5 };
                request.CharSet = charSet;
                var fileName = Path.GetFileName(file);
                request.FileName = fileName;
                stream.Position = 0;
                var result = await UploadAsync(request);
                var fileId = result.FirstFileId;
                if (!fileId.IsNullOrEmpty()) return fileId;
                var url = GetActionUrl(nameof(Upload));
                var chunkByteSize = chunkSize * 1024;
                if (stream.Length <= chunkByteSize)
                {
                    var url2 = url + nameof(request.Md5).GetFirstQueryString(md5);
                    result = await this.Client.UploadAsync<UploadResult>(url2, fileName, stream, charSet);
                    return result.FirstFileId;
                }
                var remainder = 0;
                var chunks = Math.DivRem((int)stream.Length, chunkByteSize, out remainder);
                if (remainder > 0) chunks++;
                var guid = Guid.NewGuid().ToString();
                var url3 = url + nameof(request.Guid).GetFirstQueryString(guid);
                url3 += nameof(request.Chunks).GetNextQueryString(chunks.ToString());
                for (var i = 0; i < chunks; i++)
                {
                    var buffer = new byte[chunkByteSize];
                    var readCount = await stream.ReadAsync(buffer, 0, chunkByteSize);
                    using (var memoryStream = new MemoryStream(buffer, 0, readCount))
                    {
                        var url4 = url3 + nameof(request.Chunk).GetNextQueryString(i.ToString());
                        await this.Client.UploadAsync<UploadResult>(url4, fileName, memoryStream);
                    }
                }
                request.Guid = guid;
                request.Chunks = chunks;
                request.Chunk = chunks;
                result = await UploadAsync(request);
                return result.FirstFileId;
            }
        }
        /// <summary>批量插入</summary>
        public AppDataSaveResult[] InsertMany(IndexDataSaveRequest<TDataSave> request)
        {
            return Task.Run(() => InsertManyAsync(request)).Result;
        }

        /// <summary>批量插入</summary>
        public async Task<AppDataSaveResult[]> InsertManyAsync(IndexDataSaveRequest<TDataSave> request)
        {
            return await Client.PostObjectAsync<AppDataSaveResult[]>(GetActionUrl(nameof(InsertMany)), request).ConfigureAwait(false);
        }

        /// <summary>保存</summary>
        public AppDataSaveResult Save(AppDataSaveRequest request)
        {
            return Task.Run(() => SaveAsync(request)).Result;
        }

        /// <summary>保存</summary>
        public async Task<AppDataSaveResult> SaveAsync(AppDataSaveRequest request)
        {
            return await Client.PostObjectAsync<AppDataSaveResult>(GetActionUrl(nameof(Save)), request).ConfigureAwait(false);
        }
        /// <summary>批量保存</summary>
        public AppDataSaveResult[] SaveMany(IndexDataSaveRequest<TDataSave> request)
        {
            return Task.Run(() => SaveManyAsync(request)).Result;
        }

        /// <summary>批量保存</summary>
        public async Task<AppDataSaveResult[]> SaveManyAsync(IndexDataSaveRequest<TDataSave> request)
        {
            return await Client.PostObjectAsync<AppDataSaveResult[]>(GetActionUrl(nameof(SaveMany)), request).ConfigureAwait(false);
        }

        /// <summary>批量删除</summary>
        public string[] DeleteMany(List<string> dataIds)
        {
            return Task.Run(() => DeleteManyAsync(dataIds)).Result;
        }

        /// <summary>批量删除</summary>
        public async Task<string[]> DeleteManyAsync(List<string> dataIds)
        {
            return await Client.PostObjectAsync<string[]>(GetActionUrl(nameof(DeleteMany)), dataIds).ConfigureAwait(false);
        }

        /// <summary>根据DataID获得一条应用数据</summary>
        public TDocument Get(string dataId)
        {
            return Task.Run(() => GetAsync(dataId)).Result;
        }

        /// <summary>根据DataID获得一条应用数据</summary>
        public async Task<TDocument> GetAsync(string dataId)
        {
            var queryString = $"?{nameof(dataId)}={dataId}";
            return await Client.GetAsync<TDocument>(GetActionUrl(nameof(Get)) + queryString).ConfigureAwait(false);
        }

        /// <summary>根据DataID数组获得对应的多条应用数据</summary>
        public Dictionary<string, TDocument> GetMany(List<string> dataIds)
        {
            return Task.Run(() => GetManyAsync(dataIds)).Result;
        }

        /// <summary>根据DataID数组获得对应的多条应用数据</summary>
        public async Task<Dictionary<string, TDocument>> GetManyAsync(List<string> dataIds)
        {
            return await Client.PostObjectAsync<Dictionary<string, TDocument>>(GetActionUrl(nameof(GetMany)), dataIds).ConfigureAwait(false);
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
        /// <summary>获得上传文件夹下全部子文件夹</summary>
        public List<UploadFolder> GetUploadFolders()
        {
            return Task.Run(() => GetUploadFoldersAsync()).Result;
        }

        /// <summary>获得上传文件夹下全部子文件夹</summary>
        public async Task<List<UploadFolder>> GetUploadFoldersAsync()
        {
            return await Client.GetAsync<List<UploadFolder>>(GetActionUrl(nameof(GetUploadFolders))).ConfigureAwait(false);
        }
        /// <summary>获得某个上传文件夹下全部文件列表</summary>
        public List<string> GetUploadFolderFiles(string fullName)
        {
            return Task.Run(() => GetUploadFolderFilesAsync(fullName)).Result;
        }

        /// <summary>获得某个上传文件夹下全部文件列表</summary>
        public async Task<List<string>> GetUploadFolderFilesAsync(string fullName)
        {
            fullName = HttpUtility.UrlEncode(fullName);
            var queryString = $"?{nameof(fullName)}={fullName}";
            return await Client.GetAsync<List<string>>(GetActionUrl(nameof(GetUploadFolderFiles)) + queryString).ConfigureAwait(false);
        }
        /// <summary>清除临时文件夹中的过期文件</summary>
        public void ClearTempFiles()
        {
            Task.Run(() => ClearTempFilesAsync());
        }

        /// <summary>清除临时文件夹中的过期文件</summary>
        public async Task ClearTempFilesAsync()
        {
            await Client.GetAsync(GetActionUrl(nameof(ClearTempFiles))).ConfigureAwait(false);
        }
        /// <summary>根据DataID获得一条应用数据</summary>
        public DownloadResult Download(DownloadRequest request)
        {
            return Task.Run(() => DownloadAsync(request)).Result;
        }

        /// <summary>根据DataID或FileID获得相关文件流</summary>
        public async Task<DownloadResult> DownloadAsync(DownloadRequest request)
        {
            return await Client.PostObjectAsync<DownloadResult>(GetActionUrl(nameof(Download)), request).ConfigureAwait(false);
        }

        /// <summary>获得全部文件格式信息</summary>
        public List<FileFormat> GetFileFormats()
        {
            return Task.Run(() => GetFileFormatsAsync()).Result;
        }

        /// <summary>获得全部文件格式信息</summary>
        public async Task<List<FileFormat>> GetFileFormatsAsync()
        {
            return await Client.GetAsync<List<FileFormat>>(GetActionUrl(nameof(GetFileFormats))).ConfigureAwait(false);
        }
        /// <summary>获得全部文件格式信息</summary>
        List<FileFormat> IFileFormatService.GetAll()
        {
            return GetFileFormats();
        }
    }
}