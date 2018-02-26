﻿using System;
using System.Collections.Generic;
using System.Net.Security;
using System.Security.Principal;
using System.Threading.Tasks;
using Nest;
using PKS.Core;
using PKS.Models;
using PKS.Utils;
using PKS.Web;
using PKS.WebAPI.Models;
using PKS.WebAPI.Services;

namespace PKS.WebAPI.Services
{
    /// <summary>API服务包装器</summary>
    public abstract class ApiServiceWrapper : AppService, IApiServiceWrapper
    {
        /// <summary>构造函数</summary>
        protected ApiServiceWrapper(string serviceUrl)
        {
            this.ServiceUrl = serviceUrl.NormalizeUrl();
            this.Client = new HttpClientWrapper();
            this.Client.JsonSendSerializerSettings = JsonUtil.CamelCaseJsonSerializerSettings;
            this.Client.JsonRecvSerializerSettings = JsonUtil.CamelCaseJsonSerializerSettings;
        }
        /// <summary>构造函数</summary>
        protected ApiServiceWrapper(IApiServiceConfig config, string serviceName) : this(config.Url + serviceName)
        {
            this.ServiceConfig = config;
            this.ServiceName = serviceName;
            this.Client.TokenProvider = config;
        }
        /// <summary>服务配置</summary>
        private IApiServiceConfig ServiceConfig { get; set; }
        /// <summary>服务名称</summary>
        private string ServiceName { get; set; }
        /// <summary>服务地址</summary>
        private string ServiceUrl { get; set; }
        /// <summary>客户端</summary>
        public HttpClientWrapper Client { get; private set; }
        /// <summary>重置服务URL</summary>
        public void ResetServiceUrl()
        {
            this.ServiceUrl = this.ServiceConfig.Url.NormalizeUrl() + this.ServiceName + "/";
        }
        /// <summary>获得某个服务接口URL</summary>
        protected string GetActionUrl(string actionName)
        {
            return this.ServiceUrl + actionName;
        }
    }
}