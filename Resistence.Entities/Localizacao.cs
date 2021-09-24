using System.ComponentModel.DataAnnotations;

namespace Resistence.Entities
{
    public class Localizacao
    {
        [Required]
        public string Latitude { get; set; }
        [Required]
        public string Longitude { get; set; }
        [Required]
        public string Nome { get; set; }
        public int RebeldeId { get; set; }
    }
}