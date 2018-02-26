using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Collections;
using PKS.Models;
using PKS.Utils;
using PKS.WebAPI.Services;
using Common.Logging;
using PKS.WebAPI.Models;
using System.Web.Routing;
using PKS.Core;
using System.Threading.Tasks;
using System.Web.Security;

namespace PKS.Web
{
    /// <summary>PKS Web MVC扩展</summary>
    public static class PKSMvcExtension
    {
        /// <summary>获得注入服务</summary>
        private static TService GetService<TService>()
        {
            return (TService)DependencyResolver.Current.GetService(typeof(TService));
        }
        /// <summary>获得默认路由Url</summary>
        public static string GetDefaultRouteUrl(this HttpContextBase context)
        {
            var route = RouteTable.Routes.Cast<Route>().First(e => e.Defaults != null);
            var urlTemplate = route.Url;
            foreach (var pair in route.Defaults)
            {
                urlTemplate = urlTemplate.Replace("{" + pair.Key + "}", pair.Value.ToString());
            }
            var siteUrl = context.Request.Url.GetDomainUrl();
            return siteUrl + urlTemplate.Trim('/');
        }
        /// <summary>获得默认站点Url</summary>
        public static string GetSiteDefaultUrl(this AuthorizationContext filterContext)
        {
            var controller = filterContext.Controller.As<Controller>();
            var route = controller.RouteData.Route.As<Route>();
            var urlPath = route.Url;
            foreach (var pair in route.Defaults)
            {
                var value = pair.Value.ToString();
                urlPath = urlPath.Replace("{" + pair.Key + "}", value);
            }
            var siteUrl = controller.Request.Url.GetDomainUrl().NormalizeUrl();
            return siteUrl + urlPath.Trim('/');
        }
        /// <summary>是否Windows授权认证类型</summary>
        public static bool IsWindowsAuthentication(this HttpContextBase context)
        {
            return context.User.Identity.AuthenticationType.ToLowerInvariant().FastIn("negotiate", "ntlm", "kerberos");
        }
        /// <summary>是否Forms授权认证</summary>
        public static bool IsFormsAuthentication(this HttpContextBase context)
        {
            var authenticationType = context.User.Identity.AuthenticationType;
            return authenticationType.IsNullOrEmpty() || authenticationType == "Forms";
        }
        /// <summary>获得Windows授权认证用户名</summary>
        public static string GetWindowsIdentityName(this HttpContextBase context)
        {
            return context.User.Identity.Name;
            //var windowUserName = context.User.Identity.Name;
            //var names = windowUserName.Split('\\');
            //if (names.Length != 2) return windowUserName;
            //return $@"{names[0].ToLowerInvariant()}\{names[1]}";
        }
        /// <summary>获得当前子系统代码</summary>
        public static string GetSubSystemCode(this HttpContextBase context)
        {
            return GetService<IPKSSubSystemConfig>().CurrentCode;
        }
        /// <summary>获得子系统URL</summary>
        public static Dictionary<string, string> GetSubSystemUrls(this HttpContextBase context)
        {
            var items = context.Items;
            var urls = items[PKSWebConsts.HttpContxt_SubSystemUrls].As<Dictionary<string, string>>();
            if (urls == null)
            {
                urls = GetService<IPKSSubSystemConfig>().Urls;
                items[PKSWebConsts.HttpContxt_SubSystemUrls] = urls;
            }
            return urls;
        }
        /// <summary>获得子系统URL</summary>
        public static string GetSubSystemUrl(this HttpContextBase context, string code)
        {
            return context.GetSubSystemUrls()[code];
        }
        /// <summary>获得WebAPI子系统URL</summary>
        public static string GetWebApiSiteUrl(this HttpContextBase context)
        {
            return GetSubSystemUrl(context, PKSSubSystems.WEBAPI).TrimEnd('/');
        }
        /// <summary>获得WebAPI子系统服务URL</summary>
        public static string GetWebApiServiceUrl(this HttpContextBase context)
        {
            return GetSubSystemUrl(context, PKSSubSystems.WEBAPI).NormalizeUrl() + "API";
        }
        /// <summary>获得门户子系统URL</summary>
        public static string GetPortalSiteUrl(this HttpContextBase context)
        {
            return GetSubSystemUrl(context, PKSSubSystems.Portal).TrimEnd('/');
        }
        /// <summary>登录</summary>
        public static LoginResult Login(this HttpContextBase context, ISecurityService service, string userName, string password, AuthenticationType? authenticationType)
        {
            bool isWindowsLogin = context.IsWindowsAuthentication();
            string domain = "global";//CNOOC的域
            if (isWindowsLogin == true)
            {
                if (userName.IndexOf("\\") >= 0)
                {
                    string[] domainUser = userName.Trim().Split('\\');
                    domain = domainUser[0];
                    userName = domainUser[1];
                 }
            }
            var request = new LoginRequest();
            request.AppCode = context.GetSubSystemCode();
            request.UserName = userName;
            request.Password = password;
            if (authenticationType.HasValue)
            {
                request.AuthenticationType = authenticationType.Value;
            }
            else if (isWindowsLogin)
            {
                GetService<IADIdentityService>().Login(domain, request.UserName, request.Password);
                request.AuthenticationType = AuthenticationType.Windows;
            }
            else
            {
                request.AuthenticationType = AuthenticationType.Form;
            }
            request.UserHostAddress = context.Request.Url.Authority;
            
            if (service == null) service = GetService<ISecurityService>();
            var result = service.Login(request);
            if (result.Success) context.OnLoginSuccess(result);
            return result;
        }
        /// <summary>是否已登录</summary>
        public static bool IsLogined(this HttpContextBase context, bool useCookie, ISecurityService service, ref string token, ref IPKSPrincipal principal)
        {
            if (token.IsNullOrEmpty())
            {
                token = context.GetToken();
                if (token.IsNullOrEmpty() && useCookie && context.IsFormsAuthentication())
                {
                    token = context.User.Identity.Name;
                }
                if (token.IsNullOrEmpty()) return false;
            }
            principal = context.GetPrincipal(service, token);
            if (principal != null)
            {
                OnLoginSuccess(context, token, null);
                return true;
            }
            return false;
        }
        /// <summary>登录成功的处理</summary>
        public static void OnLoginSuccess(this HttpContextBase context, LoginResult result)
        {
            OnLoginSuccess(context, result.Token, result.Principal);
        }
        /// <summary>登录成功的处理</summary>
        public static void OnLoginSuccess(this HttpContextBase context, string token, IPKSPrincipal principal)
        {
            context.Session[PKSWebConsts.Session_Authentication] = token;
            if (principal != null) context.Items[PKSWebConsts.HttpContxt_Principal] = principal;
        }
        /// <summary>获得认证令牌</summary>
        public static string GetToken(this HttpContextBase context)
        {
            return context.Session[PKSWebConsts.Session_Authentication].As<string>();
        }
        /// <summary>获得登录用户</summary>
        public static IPKSPrincipal GetPrincipal(this HttpContextBase context, ISecurityService service = null, string token = null)
        {
            var principal = context.Items[PKSWebConsts.HttpContxt_Principal].As<IPKSPrincipal>();
            if (principal == null)
            {
                if (token.IsNullOrEmpty())
                {
                    token = GetToken(context);
                    if (token.IsNullOrEmpty()) return null;
                }
                if (service == null) service = GetService<ISecurityService>();
                principal = service.GetPrincipal(token);
                if (principal != null) context.Items[PKSWebConsts.HttpContxt_Principal] = principal;
            }
            return principal;
        }
        /// <summary>门户登录URL</summary>
        private static string PortalLoginUrl = "/Account/Login";
        ///// <summary>门户重定向URL</summary>
        //private static string PortalRedirectUrl = "/Redirect/Index";
        /// <summary>其它网站单点登录URL</summary>
        private static string SSOLoginUrl = "/SSOAccount/Login";
        /// <summary>获得门户登录URL</summary>
        public static string GetPortalLoginUrl(this HttpContextBase context)
        {
            return context.GetPortalUrl(PortalLoginUrl);
        }
        /// <summary>当前请求是否门户登录URL</summary>
        public static bool IsPortalLoginUrl(this HttpContextBase context)
        {
            var requestUrl = context.Request.Url.ToString();
            var loginUrl = context.GetPortalLoginUrl();
            return requestUrl.StartsWith(loginUrl, StringComparison.OrdinalIgnoreCase);
        }
        /// <summary>获得重定向的门户登录URL</summary>
        public static string GetRedirectUrlToPortalLogin(this HttpContextBase context, string returnUrl = null)
        {
            var portalLoginUrl = context.GetPortalLoginUrl();
            if (returnUrl.IsNullOrEmpty()) returnUrl = context.Request.Url.ToString();
            returnUrl = returnUrl.UrlEncode();
            return portalLoginUrl + nameof(returnUrl).GetFirstQueryString(returnUrl);
        }
        /// <summary>门户未授权URL</summary>
        private static string PortalNoAuthorizeUrl = "/Information/NoAuthorize";
        /// <summary>获得门户未授权URL</summary>
        public static string GetPortalNoAuthorizeUrl(this HttpContextBase context)
        {
            return context.GetPortalUrl(PortalNoAuthorizeUrl);
        }
        /// <summary>获得重定向的门户未授权URL</summary>
        public static string GetRedirectUrlToPortalNoAuthorize(this HttpContextBase context, string returnUrl = null)
        {
            var portalUrl = context.GetPortalNoAuthorizeUrl();
            if (returnUrl.IsNullOrEmpty()) returnUrl = context.Request.Url.ToString();
            returnUrl = returnUrl.UrlEncode();
            return portalUrl + nameof(returnUrl).GetFirstQueryString(returnUrl);
        }
        /// <summary>获得门户URL</summary>
        public static string GetPortalUrl(this HttpContextBase context, string path)
        {
            var portalSiteUrl = string.Empty;
            if (context.GetSubSystemCode() == PKSSubSystems.Portal)
            {
                portalSiteUrl = context.Request.Url.GetDomainUrl();
            }
            else
            {
                portalSiteUrl = context.GetSubSystemUrl(PKSSubSystems.Portal);
            }
            return portalSiteUrl.TrimEnd('/') + path;
        }
        /// <summary>获得某个角色门户菜单</summary>
        public static PortalMenu GetPortalMenu(this HttpContextBase context, ISecurityService service, IPKSPrincipal principal)
        {
            if (service == null) service = GetService<ISecurityService>();
            return service.GetPortalMenu(principal.Roles.First().Id.ToInt32());
        }
        /// <summary>获得某个角色默认地址</summary>
        public static string GetRoleDefaultUrl(this HttpContextBase context, ISecurityService service, IPKSPrincipal principal)
        {
            var portalMenu = GetPortalMenu(context, service, principal);
            return portalMenu.DefaultUrl;
        }
        /// <summary>获得重定向到某个角色默认地址</summary>
        public static string GetRedirectUrlToReturnUrl(this HttpContextBase context, ISecurityService service, string token, string returnUrl)
        {
            var ssoSiteUrl = new Uri(returnUrl).GetDomainUrl().TrimEnd('/');
            var siteUrl = context.Request.Url.GetDomainUrl().TrimEnd('/');
            if (siteUrl.Equals(ssoSiteUrl, StringComparison.OrdinalIgnoreCase))
            {
                return returnUrl;
            }
            returnUrl = returnUrl.UrlEncode();
            return $"{ssoSiteUrl}{SSOLoginUrl}?{nameof(token)}={token}&{nameof(returnUrl)}={returnUrl}";
        }
        /// <summary>获得门户登录返回地址</summary>
        public static string GetPortalLoginReturnUrl(this HttpContextBase context, ISecurityService service, string token, IPKSPrincipal principal, string returnUrl)
        {
            if (returnUrl.IsNullOrEmpty())
            {
                returnUrl = GetRoleDefaultUrl(context, service, principal);
            }
            return GetRedirectUrlToReturnUrl(context, service, token, returnUrl);
        }
        /// <summary>注销</summary>
        public static async Task<string> Logout(this HttpContextBase context, ISecurityService service)
        {
            var token = context.GetToken();
            if (!token.IsNullOrEmpty())
            {
                if (service == null) service = GetService<ISecurityService>();
                await service.LogoutAsync(token);
                context.OnLogoutSuccess();
                if (context.IsFormsAuthentication())
                {
                    FormsAuthentication.SignOut();
                }
            }
            return context.GetPortalLoginUrl();
        }
        /// <summary>注销成功的处理</summary>
        public static void OnLogoutSuccess(this HttpContextBase context)
        {
            context.Session.Remove(PKSWebConsts.Session_Authentication);
        }
    }
}