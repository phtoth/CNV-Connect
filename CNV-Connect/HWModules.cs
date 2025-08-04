// Classe para a deserialização do JSON
// Class for JSON deserialization

using System.Text.Json;
using System.Text.Json.Serialization;

namespace CNV_Connect
{
    public class HWModules
    {
        public string AircraftManufacturer { get; set; } = string.Empty;

        public string AircraftModel { get; set; } = string.Empty;

        public string AircraftVariant { get; set; } = string.Empty;

        public string BoardType { get; set; } = string.Empty;

        [JsonPropertyName("BoardData")]
        public JsonElement BoardData { get; set; }
    }
}
