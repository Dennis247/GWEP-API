using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catalogue.Lib.Models.Dto
{


    public class Crs
    {
        public string type { get; set; }
        public Properties properties { get; set; }
    }

    public class Feature
    {
        public int WaterBodyId { get; set; }
        public string type { get; set; }
        public Properties properties { get; set; }
        public Geometry geometry { get; set; }


        //extra data
        //extra data
        public bool IsWaterBodyPresent { get; set; }
        public bool HasBeenVisited { get; set; }
     


    }

    public class Geometry
    {
        public string type { get; set; }
        public List<double> coordinates { get; set; }
    }

    public class Properties
    {
     
        public string name { get; set; }
        public int OBJECTID { get; set; }
        public string UNIQUE_ID { get; set; }
        public string PHASE { get; set; }
        public string CONFIDENCE { get; set; }
        public double LATITUDE { get; set; }
        public double LONGITUDE { get; set; }
        public double AREA_SQM { get; set; }
        public double SHAPE_Length { get; set; }
        public double SHAPE_Area { get; set; }
        public int OBJECTID_2 { get; set; }
        public string UNIQUE_ID_2 { get; set; }
        public string PHASE_2 { get; set; }
        public string CONFIDENCE_2 { get; set; }
        public double LATITUDE_2 { get; set; }
        public double LONGITUDE_2 { get; set; }
        public double AREA_SQM_2 { get; set; }
        public double SHAPE_Length_2 { get; set; }
        public double SHAPE_Area_2 { get; set; }
        public string HubName { get; set; }
        public double HubDist { get; set; }
        public string Direction { get; set; }


        //water body status
        public bool IsWaterBodyPresent { get; set; } 
        public bool HasBeenVisited { get; set; }
    }

    public class WaterBodyData
    {
        public string type { get; set; }
        public string name { get; set; }
        public Crs crs { get; set; }
        public List<Feature> features { get; set; }
    }


    public class UpdateWaterBodyVisitation
    {
        public bool IsVisisted { get; set; }
        public int WaterBodyId { get; set; }

        public int AccountId { get; set; }
    }


    public class UpdateWaterBodyDepression
    {
        public string  Depression { get; set; }
        public int WaterBodyId { get; set; }

        public int AccountId { get; set; }
    }


    public class UpdateWaterBodyPresence
    {
        public bool IsWaterBodyPresent { get; set; }
        public int WaterBodyId { get; set; }
        public int AccountId { get; set; }
    }
}
