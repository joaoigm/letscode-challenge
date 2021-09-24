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
        public IDictionary<ITEM_INVENTARIO, int> Inventario { get; set; }

        public string JsonInventory {
            get => JsonSerializer.Serialize(this.Inventario);
            set => Inventario = JsonSerializer.Deserialize<IDictionary<ITEM_INVENTARIO, int>>(value);
        }

        public Rebelde() {}

        public Rebelde(AdicionarRebeldeDto dto) {
            this.Nome = dto.Nome;
            this.Idade = dto.Idade;
            this.Genero = dto.Genero;
            this.Localizacao = new Localizacao {
                Latitude = dto.Localizacao.Latitude,
                Longitude = dto.Localizacao.Longitude,
                Nome = dto.Localizacao.Nome
            };
            this.Inventario = new Dictionary<ITEM_INVENTARIO, int>((IDictionary<ITEM_INVENTARIO, int>)dto.Inventario.Select(data => new KeyValuePair<ITEM_INVENTARIO, int>(pegarITEM_INVENTARIOCerto(data.Key), data.Value)));
        }

        private ITEM_INVENTARIO pegarITEM_INVENTARIOCerto(ITEM_INVENTARIODTO item){
            switch(item) {
                case ITEM_INVENTARIODTO.ARMA:
                    return ITEM_INVENTARIO.ARMA;
                case ITEM_INVENTARIODTO.MUNICAO:
                    return ITEM_INVENTARIO.MUNICAO;
                case ITEM_INVENTARIODTO.AGUA:
                    return ITEM_INVENTARIO.AGUA;
                case ITEM_INVENTARIODTO.COMIDA:
                    return ITEM_INVENTARIO.COMIDA;

                default:
                    return ITEM_INVENTARIO.AGUA;
            }
        }
    }

    public enum ITEM_INVENTARIO
    {
        ARMA,
        MUNICAO,
        AGUA,
        COMIDA
    }

    public class GeneroValidationAttribute : ValidationAttribute {
        public override bool IsValid(object value)
        {
            return value is char && (
                (char)value == 'M' || (char)value == 'F' || (char)value == 'O'
            );
        }
    }
}