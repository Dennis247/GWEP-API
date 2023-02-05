using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catalogue.Lib.Models.Entities
{
    public class WaterBodyDetectionData : BaseEntity
    {
        //This is the group data
        public string type { get; set; }
        public string name { get; set; }
        public string crs { get; set; }

        //feature is the main data b
        public string featureType { get; set; }
        public string featureProperties { get; set; }
        public string featureGometry { get; set; }  
        public int fileId { get; set; }

        //water body status
        public bool IsWaterBodyPresent { get; set; }
        public bool HasBeenVisited { get; set; }
        public string UniqueId { get; set; }

    }
}
