using System.Collections.Generic;
using System.Text.Json;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace Resistence.Entities
{
    public class Rebelde
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        
        public string Nome { get; set; }
        public int Idade { get; set; }
        [Required, GeneroValidation]
        public char Genero { get; set; }
        public Localizacao Localizacao { get; set; }
        [DefaultValue(0)]
        public int IndicacaoTraidor { get; set; }
        [DefaultValue(false)]
        public bool Traidor { get; set; }
        [NotMapped]
        public IDictionary<ItemInventario, int> Inventario { get; set; }

        public string JsonInventory {
            get => JsonSerializer.Serialize(this.Inventario);
            set => Inventario = JsonSerializer.Deserialize<IDictionary<ItemInventario, int>>(value);
        }
    }

    public enum ItemInventario
    {
        ARMA,
        MUNICAO,
        AGUA,
        COMIDA
    }

    public class GeneroValidation : ValidationAttribute {
        public override bool IsValid(object value)
        {
            return value.GetType() == typeof(char) && (
                (char)value == 'M' || (char)value == 'F' || (char)value == 'O'
            );
        }
    }
}