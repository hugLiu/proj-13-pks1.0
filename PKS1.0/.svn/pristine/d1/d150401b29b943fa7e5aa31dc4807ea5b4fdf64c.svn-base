using System.Web;
using Jurassic.AppCenter;
using Jurassic.AppCenter.Models;
using Jurassic.CommonModels.EFProvider;
using PKS.Models;
using PKS.Web;
using PKS.WebAPI.Models;
using PKS.WebAPI.Services;

namespace PKS.PortalMgmt
{
    /// <summary>登录状态提供者</summary>
    public class PKSStateProvider : MyStateProvider
    {
        /// <summary>PKS授权验证特性</summary>
        public PKSStateProvider() { }

        /// <summary>安全服务实例</summary>
        public ISecurityService SecurityService { get; set; }
        /// <summary>认证令牌</summary>
        public override LoginState Login(LoginModel model)
        {
            var state = base.Login(model);
            if (state == LoginState.OK)
            {
                var context = new HttpContextWrapper(HttpContext.Current);
                var result = context.Login(null, model.UserName, model.Password, AuthenticationType.Form);
                if (!result.Success) return LoginState.UserOrPasswordError;
            }
            return state;
        }
    }
}