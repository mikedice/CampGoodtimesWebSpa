using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CampGoodtimesSpa.Models.Sharepoint.Feeds
{
    public class Volunteer
    {
        public string Name { get; set; }
        public string Title { get; set; }
        public string Category { get; set; }
        public bool ShowOnWebsite { get; set; }
        public int Order { get; set; }
        public string Picture { get; set; }
    }
}