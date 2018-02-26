using System;
using System.Data.Entity;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using PKS.Core;
using PKS.Data;
using PKS.DbModels;
using PKS.Models;
using PKS.Utils;
using PKS.WebAPI.Models;

namespace PKS.DbServices.SysFrame
{
    /// <summary>用户身份认证服务</summary>
    public class IdentityService : AppService, IPerRequestAppService
    {
        /// <summary>登录请求验证</summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<LoginResult> LoginAsync(LoginRequest request)
        {
            var result = new LoginResult();
            var userInfo = await GetUserInfo(request.UserName);
            if (userInfo == null)
            {
                result.ErrorMessage = $"用户名称{request.UserName}不存在";
                return result;
            }
            if (request.AuthenticationType == AuthenticationType.Form && userInfo.PASSWORD != HashPassword(request.Password, userInfo.PASSWORDSALT))
            {
                result.ErrorMessage = $"用户密码错误";
                return result;
            }
            result.Principal = BuildPrincipal(userInfo, request.AuthenticationType);
            var userAuthSessionsRepository = GetService<IRepository<USERAUTHSESSIONS>>();
            var oldUserSession = await LoadSession(userAuthSessionsRepository, request.AppCode, request.UserName);
            string token = null;
            if (oldUserSession == null || oldUserSession.INVALIDTIME < DateTime.Now)
            {
                if (oldUserSession != null)
                {
                    userAuthSessionsRepository.Delete(oldUserSession, false);
                }
                token = Guid.NewGuid().ToString().ToMD5();
                var newUserSession = new USERAUTHSESSIONS
                {
                    APPKEY = request.AppCode,
                    CREATETIME = DateTime.Now,
                    INVALIDTIME = DateTime.Now.AddMinutes(PKSWebConsts.TokenExpireInterval),
                    IPADDRESS = request.UserHostAddress,
                    SESSIONKEY = token,
                    USERNAME = request.UserName
                };
                userAuthSessionsRepository.Add(newUserSession, false);
            }
            else
            {
                token = oldUserSession.SESSIONKEY;
                oldUserSession.INVALIDTIME = DateTime.Now.AddMinutes(PKSWebConsts.TokenExpireInterval);
            }
            await userAuthSessionsRepository.SubmitAsync();
            result.Token = token;
            result.Success = true;
            return result;
        }
        /// <summary>生成密码散列值</summary>
        private string HashPassword(string password, string salt)
        {
            var md5 = new MD5CryptoServiceProvider();
            var content = password + salt;
            var buffer = Encoding.UTF8.GetBytes(content);
            var hash = md5.ComputeHash(buffer);
            return BitConverter.ToString(hash).Replace("-", "");
        }

#if DEBUG
        /// <summary>调试用</summary>
        private IPKSPrincipal DebugPrincipal { get; set; }
        /// <summary>调试用</summary>
        private async Task<IPKSPrincipal> GetDebugPrincipal()
        {
            if (this.DebugPrincipal == null)
            {
                var userInfo = await GetUserInfo("admin");
                this.DebugPrincipal = BuildPrincipal(userInfo, AuthenticationType.Form);
            }
            return this.DebugPrincipal;
        }
#endif
        /// <summary>
        /// 根据token获取用户认证信息
        /// </summary>
        public async Task<IPKSPrincipal> GetPrincipalAsync(string token)
        {
#if DEBUG
            if (token == PKSWebConsts.Token_Debug)
            {
                return await GetDebugPrincipal();
            }
#endif
            var userAuthSessionsRepository = GetService<IRepository<USERAUTHSESSIONS>>();
            var userSession = await LoadSessionFromToken(userAuthSessionsRepository, token);
            if (userSession == null) return null;
            var userInfo = await GetUserInfo(userSession.USERNAME);
            if (userInfo == null) return null;
            var principal = BuildPrincipal(userInfo, AuthenticationType.Session);
            userSession.INVALIDTIME = DateTime.Now.AddMinutes(PKSWebConsts.TokenExpireInterval);
            await userAuthSessionsRepository.SubmitAsync();
            return principal;
        }

        /// <summary>
        /// 注销用户
        /// </summary>
        public async Task LogoutAsync(string token)
        {
            var userAuthSessionsRepository = GetService<IRepository<USERAUTHSESSIONS>>();
            var userSession = await LoadSessionFromToken(userAuthSessionsRepository, token);
            if (userSession != null) await userAuthSessionsRepository.DeleteAsync(userSession);
        }
        /// <summary>
        /// 根据用户名获取用户综合信息
        /// </summary>
        private IPKSPrincipal BuildPrincipal(VI_USERINFO userInfo, AuthenticationType authenticationType)
        {
            var principal = new PKSPrincipal();
            principal.Roles = new[]
            {
                new PKSRole {Id = userInfo.ROLEID.ToString(), Name = userInfo.ROLENAME, Description = userInfo.ROLEDESC}
            };
            principal.Identity = new PKSIdentity
            {
                Id = userInfo.USERID.ToString(),
                Name = userInfo.USERNAME,
                AuthenticationType = authenticationType.ToString(),
                IsAuthenticated = true
            };
            return principal;
        }
        /// <summary>
        /// 根据用户名获取用户综合信息
        /// </summary>
        /// <param name="userName">LoginRequest->UserName</param>
        /// <returns></returns>
        private async Task<VI_USERINFO> GetUserInfo(string userName)
        {
            return await GetService<IRepository<VI_USERINFO>>()
                .GetQuery()
                .FirstOrDefaultAsync(v => v.USERNAME.ToLower() == userName.ToLower());
        }
        /// <summary>
        /// 从数据库中验证用户认证Session是否存在并且在有效期内
        /// </summary>
        /// <param name="appKey"></param>
        /// <param name="userName"></param>
        /// <returns></returns>
        private async Task<USERAUTHSESSIONS> LoadSession(IRepository<USERAUTHSESSIONS> userAuthSessionsRepository, string appKey, string userName)
        {
            return await userAuthSessionsRepository
                .GetQuery()
                .OrderByDescending(e => e.CREATETIME)
                .FirstOrDefaultAsync(p => p.APPKEY == appKey && p.USERNAME == userName);
        }
        /// <summary>
        /// 从数据库中验证用户认证Session是否存在并且在有效期内
        /// </summary>
        private async Task<USERAUTHSESSIONS> LoadSessionFromToken(IRepository<USERAUTHSESSIONS> userAuthSessionsRepository, string token)
        {
            var now = DateTime.Now;
            return await userAuthSessionsRepository
                .GetQuery()
                .FirstOrDefaultAsync(p => p.SESSIONKEY == token && p.INVALIDTIME > now);
        }

        /// <summary>
        /// 根据用户名称获取UserProfile
        /// </summary>
        /// <param name="username">LoginRequest->UserName</param>
        /// <returns></returns>
        private USERPROFILE GetUserProfile(string username)
        {
            return GetService<IRepository<USERPROFILE>>().GetQuery().Where(u => u.USERNAME == username)
                .FirstOrDefault();
        }

        /// <summary>
        /// 根据用户id获取MemberShip
        /// </summary>
        /// <param name="userid">USERPROFILE->USERID</param>
        /// <returns></returns>
        private WEBPAGES_MEMBERSHIP GetMemberShip(decimal userid)
        {
            return GetService<IRepository<WEBPAGES_MEMBERSHIP>>().GetQuery().Where(m => m.USERID == userid)
                .FirstOrDefault();
        }
    }
}