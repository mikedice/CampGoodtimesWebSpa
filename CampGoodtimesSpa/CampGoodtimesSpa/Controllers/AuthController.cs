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
using CampGoodtimesSpa.Models.Auth;
using CampGoodtimesSpa.Models.Services;

namespace CampGoodtimesSpa.Models.Controllers
{
    public class AuthController : BaseController
    {
        public AuthController(ISignInService signInService) : 
            base(signInService)
        {

        }

        [HttpPost]
        public async Task<HttpResponseMessage> SignUserIn(SignInInfo info)
        {
            HttpResponseMessage result = null;
            string authCookie = await SignInService.SignUserInAsync(info);
            if (!string.IsNullOrEmpty(authCookie))
            {
                result = this.Request.CreateResponse(HttpStatusCode.OK);
                result.Content = new StringContent(info.UserName);
                var cookie = new CookieHeaderValue(SignInService.AuthCookieName, authCookie);
                cookie.Domain = "campgoodtimesseattle.org";
                cookie.Path = "/";
                cookie.HttpOnly = true;
                result.Headers.AddCookies(new CookieHeaderValue[] { cookie });
            }
            else
            {
                result = this.Request.CreateResponse(HttpStatusCode.Unauthorized);
            }
            return result;
        }

        [HttpGet]
        public async Task<HttpResponseMessage> IsUserSignedIn()
        {
            HttpResponseMessage result = null;
            string userName = null;

            userName = await GetSignedInUserAsync();
            if (!string.IsNullOrEmpty(userName))
            {
                result = this.Request.CreateResponse(HttpStatusCode.OK);
                result.Content = new StringContent(userName);
            }

            if (result == null)
            {
                result = this.Request.CreateResponse(HttpStatusCode.Unauthorized);
            }
            return result;
        }

        [HttpGet]
        public HttpResponseMessage SignUserOut()
        {
            var result = this.Request.CreateResponse(HttpStatusCode.OK);
            if (SignInCookie != null)
            {
                var cookie = new CookieHeaderValue(SignInService.AuthCookieName, SignInCookie.Value);
                cookie.Domain = "campgoodtimesseattle.org";
                cookie.Path = "/";
                cookie.HttpOnly = true;
                cookie.Expires = DateTimeOffset.MinValue;
                result.Headers.AddCookies(new CookieHeaderValue[] { cookie });
            }
            return result;
        }
    }
}