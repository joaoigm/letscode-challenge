using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Resistence.Entities
{
    public class Localizacao
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
        public string Nome { get; set; }
        [ForeignKey("Rebelde")]
        public int RebeldeId { get; set; }

        public virtual Rebelde Rebelde { get; set; }
    }
}