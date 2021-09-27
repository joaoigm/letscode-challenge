using System.Collections.Generic;
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


    public class LocalizacaoPorNomeComparer : IEqualityComparer<Localizacao>
    {
        public bool Equals(Localizacao x, Localizacao y)
        {
            return x.Nome == y.Nome;
        }

        public int GetHashCode(Localizacao obj)
        {
            return obj.GetHashCode();
        }
    }
}