using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using PKS.Models;
using PKS.Web;
using PKS.WebAPI.Models;
using PKS.WebAPI.Services;

namespace PKS.Portal.Controllers
{
    public class RenderController : PortalBaseController
    {
        public IUserBehaviorService UserBehaviorService { get; set; }
        public IPageDataService PageDataService { get; set; }
        public ISearchService SearchService { get; set; }
        public RenderController(IUserBehaviorService userbehaviorservice,
            IPageDataService pagedataservice,
            ISearchService searchservice)
        {
            UserBehaviorService = userbehaviorservice;
            PageDataService = pagedataservice;
            SearchService = searchservice;
        }

        [AllowAnonymous]
        public new ActionResult Content(string iiid)
        {
            var request = GetSearchMetadataRequest(iiid);
            var result = SearchService.GetMetadata(request);
            if (result == null)
            {
                return View("NotFound");
            }
            var userbehavior = new UserBehavior();
            userbehavior.Referer = Request.UrlReferrer?.ToString() ?? "";
            userbehavior.IIId = result.IIId;
            userbehavior.System = result.GetValue(MetadataConsts.System).ToString();
            userbehavior.Title = result.Title;
            userbehavior.Type = "预览";
            userbehavior.LogDate = DateTime.UtcNow;
            var pagedata = PageDataService.Get(result.PageId ?? result[MetadataConsts.PageId].ToString());
            //var pagedata = PageDataService.Get(result.PageId);
            userbehavior.Url = pagedata.ContentRef;
            var principal = this.PKSUser;
            if (principal == null)
            {
                principal = this.HttpContext.GetPrincipal(null, null);
            }
            if (principal != null)
            {
                userbehavior.User = principal.Identity.Name;
                userbehavior.Role = principal.Roles.First().Name;
            }
            UserBehaviorService.Add(userbehavior);

            string[] list = pagedata.ContentRef.Split('/');
            return RedirectToAction(list[2], list[1], new { iiid = iiid, dataid = result.DataId });
        }

        private SearchMetadataRequest GetSearchMetadataRequest(string iiid)
        {
            var request = new SearchMetadataRequest();
            request.IIId = iiid;
            var filter = new SearchSourceFilter();
            filter.Includes = new List<string>() { MetadataConsts.IIId, MetadataConsts.PageId, MetadataConsts.DataId, MetadataConsts.Title, MetadataConsts.System };
            request.Fields = filter;
            return request;
        }
    }
}