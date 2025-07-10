using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace CNV_Connect
{
    public class HWModules
    {
        public string AircraftManufacturer { get; set; }

        public string AircraftModel { get; set; }

        public string AircraftVariant { get; set; }

        public string BoardType { get; set; }


        [JsonPropertyName("BoardData")]
        public JsonElement BoardData { get; set; }
    }
}
