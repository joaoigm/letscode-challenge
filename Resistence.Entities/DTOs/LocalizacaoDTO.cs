using System.Text.Json.Serialization;

namespace Resistence.Entities.DTOs
{
    public class LocalizacaoDTO
    {
        [JsonPropertyName("latitude")]
        public string Latitude { get; set; }
        [JsonPropertyName("longitude")]
        public string Longitude { get; set; }
        [JsonPropertyName("Nome")]
        public string Nome { get; set; }

    }
}