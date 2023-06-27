using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public class Crsx
{
    public string type { get; set; }
    public Propertiesx properties { get; set; }
}

public class Featurex
{
    public string type { get; set; }
    public Propertiesx properties { get; set; }
    public Geometryx geometry { get; set; }
}

public class Geometryx
{
    public string type { get; set; }
    public List<double> coordinates { get; set; }
}

public class Propertiesx
{
    public string name { get; set; }

    [JsonProperty("Abate Captain")]
    public string AbateCaptain { get; set; }
    public string Woreda { get; set; }
    public string Kebele { get; set; }
    public string Village { get; set; }

    [JsonProperty("Village Code")]
    public string VillageCode { get; set; }

    [JsonProperty("Village Sharing Water Source")]
    public string VillageSharingWaterSource { get; set; }

    [JsonProperty("Water Source Name")]
    public string WaterSourceName { get; set; }

    [JsonProperty("Associated with 2021 1+ village/NVA  (Yes1, No0)")]
    public string Associatedwith20211villageNVAYes1No0 { get; set; }
    public double Latitude { get; set; }
    public double Longitude { get; set; }

    [JsonProperty("Type of Water Source")]
    public string TypeofWaterSource { get; set; }

    [JsonProperty("Special Use by (e.g. hunters, stick collectors, farmers, fisherman)")]
    public string SpecialUsebyeghuntersstickcollectorsfarmersfisherman { get; set; }

    [JsonProperty("Reasons for using water source (e.g. drinking, bathing, washing)")]
    public string Reasonsforusingwatersourceegdrinkingbathingwashing { get; set; }
}

public class AbatePointJson
{
    public string type { get; set; }
    public string name { get; set; }
    public Crsx crs { get; set; }
    public List<Featurex> features { get; set; }
}

