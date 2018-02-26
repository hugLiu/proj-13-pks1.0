using System;
using System.Collections;
using System.Collections.Generic;
using System.DirectoryServices;
using System.DirectoryServices.AccountManagement;
using System.Linq;
using System.Runtime.InteropServices;

namespace PKS.Core
{
    /// <summary>AD身份认证接口</summary>
    public class ADIdentityService : IADIdentityService, ISingletonAppService
    {
        /// <summary>登录认证</summary>
        [DllImport("advapi32.dll", SetLastError = true, CharSet = CharSet.Unicode)]
        public static extern bool LogonUser(String lpszUsername, String lpszDomain, String lpszPassword,
            int dwLogonType, int dwLogonProvider, ref IntPtr phToken);

        /// <summary>登录认证</summary>
        public List<string> Login(string domain, string userName, string password)
        {
            IntPtr userHandle = IntPtr.Zero;
            const int LOGON32_PROVIDER_DEFAULT = 0;
            const int LOGON32_LOGON_INTERACTIVE = 2;
            //domain = userName.Split('\\')[0];
            //string user = userName.Split('\\')[1];

            bool loggedOn = LogonUser(userName, domain, password, LOGON32_LOGON_INTERACTIVE, LOGON32_PROVIDER_DEFAULT, ref userHandle);
            if (!loggedOn)
            {
                ExceptionCodes.LoginFailed.ThrowUserFriendly("登录失败", "Exception impersonating user, error code: " + Marshal.GetLastWin32Error());
            }
            return new List<string>() { userName };
        }
        /// <summary>登录认证</summary>
        public List<string> ADLogin(string domain, string userName, string password)
        {
            using (var entry = new DirectoryEntry(@"LDAP://" + domain, userName, password, AuthenticationTypes.None))
            {
                var properties = new List<string>();
                var searcher = new DirectorySearcher(entry);
                searcher.Filter = $"(&(&(objectCategory=person)(objectClass=user))(SAMAccountName={userName}))";
                searcher.PropertiesToLoad.Add("cn");
                searcher.SearchRoot = entry;
                searcher.SearchScope = SearchScope.Subtree;
                var result = searcher.FindOne();
                if (result == null)
                {
                    ExceptionCodes.LoginFailed.ThrowUserFriendly("登录失败", "AD域登录失败");
                }
                var logonEntry = result.GetDirectoryEntry();
                foreach (var property in logonEntry.Properties.PropertyNames)
                {
                    var propName = property.ToString();
                    properties.Add($"{propName}:{logonEntry.Properties[propName].Cast<object>().First().ToString()}");
                }
                return properties;
            }
        }
    }
}
