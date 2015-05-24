using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CampGoodtimesSpa.Models.Auth;
using CampGoodtimesSpa.Models.Camp;
using System.Text;
using System.Threading.Tasks;

namespace CampGoodtimesSpa.Models.Services
{
    public class SignInService : ISignInService
    {
        private const string AuthCookieNameInternal = "gtauth";
        private IUserData database;

        public SignInService(IUserData database)
        {
            this.database = database;
        }

        public string AuthCookieName
        {
            get { return AuthCookieNameInternal; }
        }

        public async Task<string> SignUserInAsync(SignInInfo info)
        {
            string signInCookieValue = null;
            var user = await database.GetUserByUserNameAndPasswordAsync(info.UserName, info.Password);
            if (user != null)
            {
                signInCookieValue = ComputeSignInCookie(user);
            }
            return signInCookieValue;
        }

        public async Task<string> ValidateSignInCookieAsync(string cookie)
        {
            string result = null;
            try
            {
                byte[] bytes = Convert.FromBase64String(cookie);
                var value = ASCIIEncoding.ASCII.GetString(bytes);
                var elems = value.Split(new char[] { '_' });
                if (elems.Length == 2)
                {
                    var userName = elems[0];
                    var signInDate = Int64.Parse(elems[1]);
                    var user = await this.database.GetUserByUserNameAsync(userName);
                    if (user != null)
                    {
                        result = user.UserName;
                    }
                }
            }
            catch { }
            
            return result;
        }

        private string ComputeSignInCookie(User user)
        {
            string fragment = string.Format("{0}_{1}", user.UserName, DateTime.Now.ToBinary());
            var bytes = ASCIIEncoding.ASCII.GetBytes(fragment);
            var result = Convert.ToBase64String(bytes);
            return result;
        }
    }
}