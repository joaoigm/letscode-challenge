using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Resistence.Entities.DTOs
{
    public class AdicionarRebeldeDto
    {
        [JsonPropertyName("nome")]
        public string Nome { get; set; }
        [JsonPropertyName("idade")]
        public int Idade { get; set; }
        [JsonPropertyName("genero")]
        public char Genero { get; set; }
        [JsonPropertyName("localizacao")]
        public LocalizacaoDto Localizacao { get; set; }
        [JsonPropertyName("inventario")]
        public IDictionary<ITEM_INVENTARIODTO, int> Inventario { get; set; }
    }

    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum ITEM_INVENTARIODTO
    {
        ARMA,
        MUNICAO,
        AGUA,
        COMIDA
    }
}