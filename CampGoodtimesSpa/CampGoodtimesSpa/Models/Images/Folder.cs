using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CampGoodtimesSpa.Models.Images
{
    public class Folder
    {
        public IEnumerable<FolderItem> Items { get; set; }
    }
}