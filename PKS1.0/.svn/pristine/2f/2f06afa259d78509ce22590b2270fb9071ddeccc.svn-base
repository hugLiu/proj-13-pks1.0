using System;
using System.Configuration;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace PKS.Models
{
    /// <summary>WEB常数</summary>
    public static class PKSWebConsts
    {
        #region 调试常数
        /// <summary>调试用Token</summary>
        public static readonly string Token_Debug = "debug";
        #endregion

        #region 缓存过期时间常数
        /// <summary>Token过期时间,单位为分,用于外部缓存,默认为1天</summary>
        public static int TokenExpireInterval = 1440;
        /// <summary>Principal过期时间,单位为分,用于内部缓存,默认为20分钟</summary>
        public static int PrincipalExpireInterval = 20;
        #endregion

        #region AppSettings常数
        /// <summary>配置常数_子系统</summary>
        public static readonly string AppSettings_SubSystem = "PKS_SubSystem";
        /// <summary>获得子系统代码</summary>
        public static string GetSubSystemCode()
        {
            return ConfigurationManager.AppSettings[PKSWebConsts.AppSettings_SubSystem];
        }
        /// <summary>配置常数_缩略图默认尺寸</summary>
        public static readonly string AppSettings_ThumbnailDefaultSize = "ThumbnailDefaultSize";
        /// <summary>配置常数_图片最大尺寸</summary>
        public static readonly string AppSettings_ImageMaxSize = "ImageMaxSize";
        #endregion

        #region 会话常数
        /// <summary>会话常数_授权键</summary>
        public static readonly string Session_Authentication = "PKS.Session.Token";
        #endregion

        #region HttpContxt.Items常数
        /// <summary>上下文临时常数_子系统Url</summary>
        public static readonly string HttpContxt_SubSystemUrls = "PKS.Context.SubSystemUrls";
        /// <summary>上下文临时常数_登录用户</summary>
        public static readonly string HttpContxt_Principal = "PKS.Context.Principal";
        #endregion

        #region 服务常数
        /// <summary>管理服务宿主名</summary>
        public static readonly string MgmtServicesHost = "PKS_MgmtServices_Host";
        #endregion
    }
}