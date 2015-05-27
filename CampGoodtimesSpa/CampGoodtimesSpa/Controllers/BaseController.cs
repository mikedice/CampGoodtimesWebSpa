using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Net;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Web.Http;
using System.Net.Http;
using CampGoodtimesSpa.Models.Services;

namespace CampGoodtimesSpa.Models.Controllers
{
    public class BaseController : ApiController
    {
        private ISignInService signInService;

        public BaseController(ISignInService signInService)
        {
            this.signInService = signInService;
        }

        protected ISignInService SignInService
        {
            get
            {
                return this.signInService;
            }
        }

        protected async Task<string> GetSignedInUserAsync()
        {
            string result = null;
            if (SignInCookie != null)
            {
                result = await signInService.ValidateSignInCookieAsync(SignInCookie.Value);
            }
            return result;
        }

        protected CookieState SignInCookie
        {
            get
            {
                var cookies = this.ControllerContext.Request.Headers.GetCookies();
                var cookie = cookies.Where(c => c.Cookies.Count > 0 && c.Cookies[0].Name.Equals(signInService.AuthCookieName)).FirstOrDefault();
                if (cookie != null)
                {
                    return cookie.Cookies[0];
                }
                return null;
            }
        }

        protected static string TrimName(string name)
        {
            var result = name.Trim();
            result = name.Replace("\"", string.Empty);
            return result;
        }

        protected string GetSubdirName(string fileSystemDir)
        {
            string[] elems = fileSystemDir.Split(new char[] { '\\' });
            return elems.Last();
        }

        protected string MakeWebDir(string fileSystemDir, string rootPath)
        {
            var trimmed = fileSystemDir.Substring(rootPath.Length);
            var result = trimmed.Replace('\\', '/');
            return result;
        }

        protected string MakeFileSystemPath(string subDir)
        {
            return subDir.Replace('/', '\\');
        }

        protected string GetServerRootPath()
        {
            var context = (System.Web.HttpContextWrapper)this.Request.Properties["MS_HttpContext"];
            var variable = context.Request.ServerVariables["APPL_PHYSICAL_PATH"];
            return variable;
        }
    }
}