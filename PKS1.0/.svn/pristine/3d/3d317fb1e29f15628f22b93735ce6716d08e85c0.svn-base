using System.Threading.Tasks;
using System.Web.Mvc;
using PKS.Models;
using PKS.Utils;
using PKS.WebAPI.Services;

namespace PKS.Web.Controllers
{
    /// <summary>单点登录控制器</summary>
    [AllowAnonymous]
    public class SSOAccountController : PKSBaseController
    {
        /// <summary>构造函数</summary>
        public SSOAccountController(ISecurityService securityService)
        {
            _securityService = securityService;
        }
        /// <summary>安全服务</summary>
        private ISecurityService _securityService;
        /// <summary>单点登录</summary>
        public ActionResult Login(string token, string returnUrl)
        {
            returnUrl = returnUrl.UrlDecode();
            IPKSPrincipal principal = null;
            if (token.IsNullOrEmpty() || !this.HttpContext.IsLogined(false, _securityService, ref token, ref principal))
            {
                var redirectUrl = this.HttpContext.GetRedirectUrlToPortalNoAuthorize(returnUrl);
                return Redirect(redirectUrl);
            }
            //if (this.HttpContext.IsFormsAuthentication())
            //{
            //    FormsAuthentication.SetAuthCookie(token, false);
            //}
            if (returnUrl.IsNullOrEmpty()) returnUrl = "/";
            return Redirect(returnUrl);
        }
        /// <summary>注销</summary>
        public async Task<ActionResult> Logout()
        {
            var loginUrl = await this.HttpContext.Logout(_securityService);
            return Redirect(loginUrl);
        }
    }
}