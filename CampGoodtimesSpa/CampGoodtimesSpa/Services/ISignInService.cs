using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CampGoodtimesSpa.Models.Auth;

namespace CampGoodtimesSpa.Models.Services
{
    public interface ISignInService
    {
        string AuthCookieName { get; }
        Task<string> SignUserInAsync(SignInInfo info);
        Task<string> ValidateSignInCookieAsync(string cookie);
    }
}
