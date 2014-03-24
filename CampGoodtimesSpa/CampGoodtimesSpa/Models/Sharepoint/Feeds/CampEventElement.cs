using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CampGoodtimesSpa.Models.Sharepoint.Feeds
{
    public class CampEventElement
    {
        public string EventDate { get; set; }
        public bool ShowOnWebsite { get; set; }
        public string DetailsPageName { get; set; }
        public string Author { get; set; }
        public DateTime PublishedOnGmt { get; set; }
        public string Title { get; set; }
        public string WelcomeTo { get; set; }
        public string ShortDescription { get; set; }
    }
}