using System.Text.Json.Serialization;

namespace Resistence.Entities.DTOs
{
    public class LocalizacaoDto
    {
        [JsonPropertyName("latitude")]
        public string Latitude { get; set; }
        [JsonPropertyName("longitude")]
        public string Longitude { get; set; }
        [JsonPropertyName("nome")]
        public string Nome { get; set; }

    }
}