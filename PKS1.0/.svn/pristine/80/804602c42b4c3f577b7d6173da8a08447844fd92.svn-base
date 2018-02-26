using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.Security;
using PKS.Models;
using PKS.Web;
using PKS.WebAPI.Services;

namespace PKS.Portal.Controllers
{
    /// <summary>账户控制器</summary>
    [AllowAnonymous]
    public class AccountController : PortalBaseController
    {
        /// <summary>构造函数</summary>
        public AccountController(ISecurityService securityService) : base()
        {
            _securityService = securityService;
        }
        /// <summary>安全服务</summary>
        private ISecurityService _securityService;
        /// <summary>登录</summary>
        public ActionResult Login(string returnUrl)
        {
            returnUrl = returnUrl.UrlDecode();
            string token = null;
            IPKSPrincipal principal = null;
            if (this.HttpContext.IsLogined(true, _securityService, ref token, ref principal))
            {
                var redirectUrl = this.HttpContext.GetPortalLoginReturnUrl(_securityService, token, principal, returnUrl);
                return Redirect(redirectUrl);
            }
            ViewBag.token = token;
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        /// <summary>登录</summary>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginModel model)
        {
            var loginResult = new LoginResult();
            var result = this.HttpContext.Login(_securityService, model.UserName, model.Password, null);
            if (result.Success)
            {
                loginResult.Success = true;
                loginResult.RedirectUrl = this.HttpContext.GetPortalLoginReturnUrl(_securityService, result.Token, result.Principal, model.ReturnUrl);
                if (this.HttpContext.IsFormsAuthentication())
                {
                    FormsAuthentication.SetAuthCookie(result.Token, model.RememberMe);
                }
            }
            else
            {
                loginResult.Error = result.ErrorMessage;
            }
            return NewtonJson(loginResult, JsonRequestBehavior.DenyGet);
        }
        /// <summary>注销</summary>
        public async Task<ActionResult> Logout()
        {
            var loginUrl = await this.HttpContext.Logout(_securityService);
            return Redirect(loginUrl);
        }
    }

    /// <summary>登录数据</summary>
    public class LoginModel
    {
        /// <summary>用户名</summary>
        public string UserName { get; set; }
        /// <summary>密码</summary>
        public string Password { get; set; }
        /// <summary>记住我</summary>
        public bool RememberMe { get; set; }
        /// <summary>登录成功后跳转Url</summary>
        public string ReturnUrl { get; set; }
    }

    public class LoginResult
    {
        public bool Success { get; set; }
        /// <summary>
        /// 登录成功后跳转Url
        /// </summary>
        public string RedirectUrl { get; set; }
        /// <summary>
        /// 错误信息
        /// </summary>
        public string Error { get; set; }
    }
}