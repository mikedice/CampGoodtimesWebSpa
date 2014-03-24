using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace CampGoodtimesSpa.Controllers
{
    public class DataController : ApiController
    {
        [HttpGet]
        public IEnumerable<object> NewsItems()
        {
            return new object[] { 
                new {name="object 1", city="Redmond"},
                new {name="object 2", city="Seattle"}
            };
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
