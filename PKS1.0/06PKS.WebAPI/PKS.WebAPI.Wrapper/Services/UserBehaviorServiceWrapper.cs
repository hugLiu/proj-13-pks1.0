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

namespace PKS.WebAPI.Services
{
    /// <summary>日志服务实现</summary>
    public class UserBehaviorServiceWrapper : ApiServiceWrapper, IUserBehaviorService, IUserBehaviorServiceWrapper, ISingletonAppService
    {
        /// <summary>构造函数</summary>
        public UserBehaviorServiceWrapper(string serviceUrl) : base(serviceUrl)
        {
        }

        /// <summary>构造函数</summary>
        public UserBehaviorServiceWrapper(IApiServiceConfig config) : base(config, nameof(IUserBehaviorService).Substring(1))
        {
        }
        /// <summary>
        /// 添加日志
        /// </summary>
        /// <param name="request"></param>
        public void Add(UserBehavior request)
        {
            Task.Run(() => AddLog(request));
        }
        /// <summary>
        /// 添加日志
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task AddLog(UserBehavior request)
        {
            await this.Client.PostObjectAsync(GetActionUrl(nameof(Add)), request).ConfigureAwait(false);
        }
        ///日志查询
        public string Search(string request)
        {
            return Task.Run(() => SearchAsync(request)).Result;
        }
        /// <summary>
        /// 日志查询
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<string> SearchAsync(string request)
        {
            return await this.Client.PostAsync<string>(GetActionUrl(nameof(Search)), request).ConfigureAwait(false);
        }
    }
}