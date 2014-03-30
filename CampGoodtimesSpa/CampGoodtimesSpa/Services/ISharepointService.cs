using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using CampGoodtimesSpa.Models.Sharepoint.Feeds;

namespace CampGoodtimesSpa.Services
{
    public interface ISharepointService
    {
        Task<IEnumerable<NewsFromTheDirectorElement>> GetDirectorNewsFeedAsync(string newsFeedUrl);
        Task<IEnumerable<CampEventElement>> GetCampEventsAsync(string newsFeedUrl);
        Task<IEnumerable<SponsorsFeedElement>> GetSponsorsAsync(string sponsorsFeedUrl);
        Task<IEnumerable<Employee>> GetStaffAsync(string staffFeedUrl);
        Task<IEnumerable<Employee>> GetBoardAsync(string boardFeedUrl);
    }
}
