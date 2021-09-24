using System.Collections.Generic;
using System.Text.Json;
using System.ComponentModel;
using Resistence.Entities.DTOs;
using System.Linq;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Resistence.Entities
{
    public class Rebelde
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        
        public string Nome { get; set; }
        public int Idade { get; set; }
        [GeneroValidation]
        public char Genero { get; set; }
        [ForeignKey("RebeldeId")]
        public virtual Localizacao Localizacao { get; set; }
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

        public Rebelde() {}

        public Rebelde(AdicionarRebeldeDTO dto) {
            this.Nome = dto.Nome;
            this.Idade = dto.Idade;
            this.Genero = dto.Genero;
            this.Localizacao = new Localizacao {
                Latitude = dto.Localizacao.Latitude,
                Longitude = dto.Localizacao.Longitude,
                Nome = dto.Localizacao.Nome
            };
            this.Inventario = new Dictionary<ItemInventario, int>((IDictionary<ItemInventario, int>)dto.Inventario.Select(data => new KeyValuePair<ItemInventario, int>(pegarItemInventarioCerto(data.Key), data.Value)));
        }

        private ItemInventario pegarItemInventarioCerto(ItemInventarioDTO item){
            switch(item) {
                case ItemInventarioDTO.ARMA:
                    return ItemInventario.ARMA;
                case ItemInventarioDTO.MUNICAO:
                    return ItemInventario.MUNICAO;
                case ItemInventarioDTO.AGUA:
                    return ItemInventario.AGUA;
                case ItemInventarioDTO.COMIDA:
                    return ItemInventario.COMIDA;

                default:
                    return ItemInventario.AGUA;
            }
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