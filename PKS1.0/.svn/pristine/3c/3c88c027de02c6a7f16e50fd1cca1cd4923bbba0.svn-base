using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web.Mvc;
using PKS.Models;
using PKS.Web.Controllers;
using PKS.WebAPI.Services;

namespace PKS.Web.Core
{

    /// <summary>信息提供控制器</summary>
    [AllowAnonymous]
    public class CommonInfoController : PKSBaseController
    {
        /// <summary>
        /// 获取菜单及头部信息
        /// </summary>
        public virtual ActionResult GetHeadMenuInfo()
        {
            var principal = base.PKSUser;
            if (principal == null)
            {
                principal = this.HttpContext.GetPrincipal(null, null);
            }
            var infos = new Dictionary<string, string>();
            infos.Add("messagecount", "6");
            infos.Add("imgsource", "/Content/images/header/icon01.jpg");
            infos.Add("logosource", "/Content/images/header/logo.png");
            infos.Add("gisurl", HttpContext.GetSubSystemUrl(PKSSubSystems.GIS));
            // TODO : 暂时屏蔽门户管理
            //infos.Add("portalmgrurl", HttpContext.GetSubSystemUrl(PKSSubSystems.PORTALMGMT));
            infos.Add("portalmgrurl", null);
            infos.Add("logouturl", HttpContext.GetSubSystemUrl(PKSSubSystems.Portal) + "/account/logout?from=" + HttpContext.GetSubSystemCode());
            infos.Add("sooilurl", HttpContext.GetSubSystemUrl(PKSSubSystems.SOOIL) + "/search/list");
            if (principal != null)
            {
                infos.Add("currentuser", principal.Identity.Name);
                infos.Add("apipath", GetPortalMenuUrl(principal));
            }
            return NewtonJson(infos);
        }

        /// <summary>
        /// 菜单Url
        /// </summary>
        /// <returns></returns>
        public string GetPortalMenuUrl()
        {
            var principal = base.PKSUser;
            if (principal == null) principal = this.HttpContext.GetPrincipal(null, null);
            if (principal != null) return GetPortalMenuUrl(principal);
            return null;
        }
        /// <summary>
        /// 菜单Url
        /// </summary>
        /// <returns></returns>
        private string GetPortalMenuUrl(IPKSPrincipal principal)
        {
            return HttpContext.GetSubSystemUrl(PKSSubSystems.WEBAPI) + "/api/SecurityService/GetPortalMenu?roleId=" + principal.Roles.FirstOrDefault().Id;
        }
    }
}