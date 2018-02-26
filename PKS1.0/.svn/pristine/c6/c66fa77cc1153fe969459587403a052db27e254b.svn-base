using System.Collections.Generic;
using System.Threading.Tasks;
using PKS.Core;
using PKS.Models;
using PKS.Utils;
using PKS.Web;
using PKS.WebAPI.Models;

namespace PKS.WebAPI.Services
{
    /// <summary>安全服务实现包装器</summary>
    public class SecurityServiceWrapper : ApiServiceWrapper, ISecurityService, ISecurityServiceWrapper, ISingletonAppService
    {
        /// <summary>构造函数</summary>
        public SecurityServiceWrapper(string serviceUrl) : base(serviceUrl)
        {
        }

        /// <summary>构造函数</summary>
        public SecurityServiceWrapper(IApiServiceConfig config) : base(config, nameof(ISecurityService).Substring(1))
        {
        }
        /// <summary>登录</summary>
        public LoginResult Login(LoginRequest request)
        {
            return Task.Run(() => LoginAsync(request)).Result;
        }

        /// <summary>登录</summary>
        public async Task<LoginResult> LoginAsync(LoginRequest request)
        {
            return await this.Client.PostObjectAsync<LoginResult>(GetActionUrl(nameof(Login)), request).ConfigureAwait(false);
        }
        /// <summary>获取登录用户信息</summary>
        public IPKSPrincipal GetPrincipal(string token)
        {
            return Task.Run(() => GetPrincipalAsync(token)).Result;
        }

        /// <summary>获取登录用户信息</summary>
        public async Task<IPKSPrincipal> GetPrincipalAsync(string token)
        {
            var cacheItem = this.MemcachedCacher?.GetCacheItem(token, CacheConst.AuthenticationRegion);
            if (cacheItem == null)
            {
                var url = GetActionUrl(nameof(GetPrincipal)) + nameof(token).GetFirstQueryString(token);
                return await this.Client.GetAsync<IPKSPrincipal>(url).ConfigureAwait(false);
            }
            return cacheItem.Value.As<IPKSPrincipal>();
        }
        /// <summary>注销用户</summary>
        public void Logout(string token)
        {
            Task.Run(() => LogoutAsync(token));
        }

        /// <summary>注销用户</summary>
        public async Task LogoutAsync(string token)
        {
            await this.Client.GetAsync(GetActionUrl(nameof(Logout))).ConfigureAwait(false);
        }
        /// <summary>获得指定角色的门户菜单</summary>
        public PortalMenu GetPortalMenu(int roleId)
        {
            return Task.Run(() => GetPortalMenuAsync(roleId)).Result;
        }

        /// <summary>获得指定角色的门户菜单</summary>
        public async Task<PortalMenu> GetPortalMenuAsync(int roleId)
        {
            var key = CacheConst.RoleKey + roleId.ToString();
            var result = this.MemcachedCacher?.TryGet<PortalMenu>(key, CacheConst.PermissionRegion);
            if (result == null)
            {
                var url = GetActionUrl(nameof(GetPortalMenu)) + nameof(roleId).GetFirstQueryString(roleId.ToString());
                result = await this.Client.GetAsync<PortalMenu>(url).ConfigureAwait(false);
            }
            return result;
        }

        /// <summary>获得指定用户的权限集合</summary>
        public Dictionary<string, bool> GetPermissions(int userId)
        {
            return Task.Run(() => GetPermissionsAsync(userId)).Result;
        }
        /// <summary>获得指定用户的权限集合</summary>
        public async Task<Dictionary<string, bool>> GetPermissionsAsync(int userId)
        {
            var key = CacheConst.UserKey + userId.ToString();
            var result = this.MemcachedCacher?.TryGet<Dictionary<string, bool>>(key, CacheConst.PermissionRegion);
            if (result == null)
            {
                var url = GetActionUrl(nameof(GetPermissions)) + nameof(userId).GetFirstQueryString(userId.ToString());
                result = await this.Client.GetAsync<Dictionary<string, bool>>(url).ConfigureAwait(false);
            }
            return result;
        }
        /// <summary>判断指定用户对指定Url是否有权限</summary>
        public bool HasMenuPermission(int userId, string url)
        {
            return Task.Run(() => HasMenuPermissionAsync(userId, url)).Result;
        }
        /// <summary>判断指定用户对指定Url是否有权限</summary>
        public async Task<bool> HasMenuPermissionAsync(int userId, string url)
        {
            var permissions = await GetPermissionsAsync(userId);
            var url2 = url.RemoveQueryString().TrimEnd('/').ToLowerInvariant();
            return permissions.GetValueOrDefaultBy(url2, false);
        }
    }
}