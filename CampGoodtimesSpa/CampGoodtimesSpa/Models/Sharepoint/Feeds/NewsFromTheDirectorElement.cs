using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CampGoodtimesSpa.Models.Sharepoint.Feeds
{
    public class NewsFromTheDirectorElement
    {
        public string Title { get; set; }
        public string Author { get; set; }
        public string Description { get; set; }
        public bool IsVisible { get; set; }
        public DateTime PublishedOnGmt { get; set; }
        public string EventDate { get; set; }
        public string EventImageUrl { get; set; }
    }
}