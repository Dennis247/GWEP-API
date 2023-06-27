using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catalogue.Lib.Models.Entities
{
    public class AbatePoint : BaseEntity
    {
        public string? type { get; set; }
        public string? name { get; set; }

        //crs properties
        public string? PropertyName { get; set; }
        public string? AbateCaptain { get; set; }
        public string? Woreda { get; set; }
        public string? Kebele { get; set; }
        public string? Village { get; set; }
        public string? VillageCode { get; set; }
        public string? VillageSharingWaterSource { get; set; }
        public string? WaterSourceName { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public string? TypeofWaterSource { get; set; }
        public string? SpecialUse{ get; set; }
        public string? Reasonsforusingwatersource{ get; set; }
    }
}
