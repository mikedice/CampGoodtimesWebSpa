using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CampGoodtimesSpa.Models.Camp;
using System.Threading.Tasks;

namespace CampGoodtimesSpa.Models.Services
{
    public interface IUserData
    {
        Task<IEnumerable<User>> GetUsersAsync();
        Task<IEnumerable<User>> GetPublicUsersAsync();
        Task<User> GetUserByUserNameAndPasswordAsync(string userName, string password);
        Task<User> GetUserByUserNameAsync(string userName);
    }
}