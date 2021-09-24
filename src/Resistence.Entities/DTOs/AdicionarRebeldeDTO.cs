using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Resistence.Entities.DTOs
{
    public class AdicionarRebeldeDTO
    {
        [JsonPropertyName("nome")]
        public string Nome { get; set; }
        [JsonPropertyName("idade")]
        public int Idade { get; set; }
        [JsonPropertyName("genero")]
        public char Genero { get; set; }
        [JsonPropertyName("localizacao")]
        public LocalizacaoDTO Localizacao { get; set; }
        [JsonPropertyName("inventario")]
        public IDictionary<ItemInventarioDTO, int> Inventario { get; set; }
    }


    public enum ItemInventarioDTO
    {
        ARMA,
        MUNICAO,
        AGUA,
        COMIDA
    }
}