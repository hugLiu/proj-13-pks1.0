using System.Web.Mvc;
using PKS.Models;
using PKS.Utils;
using PKS.Web;

namespace PKS.Portal.Controllers
{
    /// <summary>重定向控制器</summary>
    [AllowAnonymous]
    public class RedirectController : PortalBaseController
    {
        /// <summary>重定向</summary>
        public ActionResult Index(string returnUrl)
        {
            returnUrl = returnUrl.UrlDecode();
            string token = null;
            IPKSPrincipal principal = null;
            string redirectUrl;
            if (!this.HttpContext.IsLogined(true, null, ref token, ref principal))
            {
                redirectUrl = this.HttpContext.GetRedirectUrlToPortalLogin(returnUrl);
                return Redirect(redirectUrl);
            }
            redirectUrl = this.HttpContext.GetPortalLoginReturnUrl(null, token, principal, returnUrl);
            return Redirect(redirectUrl);
        }
    }
}