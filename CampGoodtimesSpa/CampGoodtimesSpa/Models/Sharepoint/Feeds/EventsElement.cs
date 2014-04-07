using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CampGoodtimesSpa.Models.Sharepoint.Feeds
{
    public class EventsElement
    {
        public string Title { get; set; }
        public string Attendance { get; set; }
        public string DateString { get; set; }
        public string ShortDescription { get; set; }
        public string BannerPicture { get; set; }
        public int Order { get; set; }
        public bool ShowOnWebsite { get; set; }
        public int EventNumber { get; set; }
        public string EventInformation { get; set; }
    }
}