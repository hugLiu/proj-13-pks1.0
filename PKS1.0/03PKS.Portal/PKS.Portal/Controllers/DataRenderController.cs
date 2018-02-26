using System;
using System.Collections.Generic;
using System.Web.Mvc;
using PKS.Models;
using PKS.Utils;
using PKS.Web;
using PKS.WebAPI.Models;
using PKS.WebAPI.Services;
using static Newtonsoft.Json.JsonConvert;

namespace PKS.Portal.Controllers
{
    public class DataRenderController : PortalBaseController
    {
        private IAppDataService AppDataService { get; set; }
        private ISearchService SearchService { get; set; }
        public DataRenderController(IAppDataService appdataservice, ISearchService searchService)
        {
            AppDataService = appdataservice;
            SearchService = searchService;
        }
        // GET: DataRender
        public ActionResult Image(string iiid, string dataid)
        {
            //ViewBag.data = AppDataService.Get(dataid);
            ViewBag.data = GetConfig(iiid, dataid, true);
            return View();
        }

        public ActionResult PDF(string iiid, string dataid)
        {
            //ViewBag.data = AppDataService.Get(dataid);
            ViewBag.data = GetConfig(iiid, dataid, true);
            return View();
        }


        public ActionResult HTML(string iiid, string dataid)
        {
            //ViewBag.data = AppDataService.Get(dataid);
            ViewBag.data = GetConfig(iiid, dataid, false);
            return View();
        }

        public ActionResult Table(string iiid, string dataid)
        {
            //ViewBag.data = AppDataService.Get(dataid);
            ViewBag.data = GetConfig(iiid, dataid, false);
            return View();
        }
        public ActionResult Histogram(string iiid, string dataid)
        {
            //ViewBag.data = AppDataService.Get(dataid);
            ViewBag.data = GetConfig(iiid, dataid, false);
            return View();
        }

        public ActionResult PropertyGrid(string iiid, string dataid)
        {
            //ViewBag.data = AppDataService.Get(dataid);
            ViewBag.data = GetConfig(iiid, dataid, false);
            return View();
        }

        private SearchMetadataRequest GetSearchMetadataRequest(string iiid)
        {
            SearchMetadataRequest request = new SearchMetadataRequest();
            request.IIId = iiid;
            SearchSourceFilter filter = new SearchSourceFilter();
            filter.Includes = new List<string>() { MetadataConsts.IIId, MetadataConsts.PageId, MetadataConsts.DataId, MetadataConsts.Title, MetadataConsts.CreatedDate, MetadataConsts.Author, MetadataConsts.Abstract };
            request.Fields = filter;
            return request;
        }

        private string GetConfig(string iiid, string dataid, bool fromUrl)
        {
            var result = SearchService.GetMetadata(GetSearchMetadataRequest(iiid));
            var dataSource = GetFullAPIUrl(dataid);
            if (!fromUrl)
            {
                var content = (AppDataService.Get(dataid) as PKS.WebAPI.Models.IndexAppData).Content;
                dataSource = Convert.ToString(content);
            }
            return SerializeObject(new { iiid = iiid, createddate = result.GetValueBy(MetadataConsts.CreatedDate), title = result.Title, author = result.Author, Abstract = result.Abstract, dataSource = dataSource });
        }

        private string GetFullAPIUrl(string dataid)
        {
            return HttpContext.GetSubSystemUrl(PKSSubSystems.WEBAPI) + "/api/appdataservice/download?dataid=" + dataid;
        }
    }
}