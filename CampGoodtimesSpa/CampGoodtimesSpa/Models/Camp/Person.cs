using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CampGoodtimesSpa.Models.Camp
{
    public class Person
    {
        public int? Id {get; set;}
        public string PersonType { get; set; }
        public DateTime? CreatedOn { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime? DeletedOn { get; set; }
        public string DeletedBy { get; set; }
        public string Name { get; set; }
        public bool? VisibleOnWebsite { get; set; }
        public string ImageSmall { get; set; }
        public string ImageLarge { get; set; }
        public string Title { get; set; }
        public int? Order { get; set; }
    }
}