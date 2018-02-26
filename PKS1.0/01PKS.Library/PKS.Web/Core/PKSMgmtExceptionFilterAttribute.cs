using System;
using System.Configuration;
using System.Net;
using System.Net.Http.Headers;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using Common.Logging;
using PKS.Core;
using PKS.Models;
using PKS.Utils;
using PKS.WebAPI.Models;
using PKS.WebAPI.Services;

namespace PKS.Web.MVC.Filter
{
    /// <summary>后台管理异常处理拦截器</summary>
    public class PKSMgmtExceptionFilterAttribute : HandleErrorAttribute
    {
        /// <summary>构造函数</summary>
        public PKSMgmtExceptionFilterAttribute(ILog logger)
        {
            this.Logger = logger;
        }

        /// <summary>日志</summary>
        private ILog Logger { get; }

        /// <summary>Called when an exception occurs.</summary>
        /// <param name="filterContext">The action-filter context.</param>
        /// <exception cref="T:System.ArgumentNullException">The <paramref name="filterContext" /> parameter is null.</exception>
        public override void OnException(ExceptionContext filterContext)
        {
            if (filterContext == null)
            {
                throw new ArgumentNullException("filterContext");
            }
            if (filterContext.IsChildAction)
            {
                return;
            }
            var exception = filterContext.Exception;
            this.Logger.Error(string.Empty, exception);
        }
    }
}