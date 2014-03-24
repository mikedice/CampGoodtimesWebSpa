using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using CampGoodtimesSpa.Services;
using CampGoodtimesSpa.Models.Sharepoint.Feeds;
namespace CampGoodtimesSpa.Controllers
{
    public class DataController : ApiController
    {
        ISharepointService sharepointService;

        public DataController(ISharepointService sharepointService)
        {
            this.sharepointService = sharepointService;
        }

        [HttpGet]
        public Task<IEnumerable<NewsFromTheDirectorElement>> NewsItems()
        {
            string url = ConfigurationManager.AppSettings["CampDirectorNewsItemsRssFeed"];
            return sharepointService.GetDirectorNewsFeedAsync(url);
        }

        //// GET api/api/5
        //public string Get(int id)
        //{
        //    return "value";
        //}

        //// POST api/api
        //public void Post([FromBody]string value)
        //{
        //}

        //// PUT api/api/5
        //public void Put(int id, [FromBody]string value)
        //{
        //}

        //// DELETE api/api/5
        //public void Delete(int id)
        //{
        //}
    }
}
