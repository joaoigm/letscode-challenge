using System.Collections.Generic;
using System.Text.Json;
using System.ComponentModel;
using Resistence.Entities.DTOs;
using System.Linq;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Resistence.Entities.Exceptions;
using System.Text.Json.Serialization;

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

        [JsonIgnore]
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
            var inventario = new Dictionary<ITEM_INVENTARIO, int>();
            foreach(var item in dto.Inventario) {
                inventario.Add(pegarITEM_INVENTARIOCerto(item.Key), item.Value);
            }
            this.Inventario = inventario;
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

        public void removerOuReduzirItemInventario(ITEM_INVENTARIODTO item, int quantidade) {
            var itemCerto = pegarITEM_INVENTARIOCerto(item);
            var quantidadeAtual = this.Inventario[itemCerto];
            if(quantidadeAtual == quantidade) {
                this.Inventario.Remove(itemCerto);
            } else if(quantidadeAtual > quantidade) {
                this.Inventario[itemCerto] -= quantidade;
            } else {
                throw new QuantidadeItemInventarioASerRemovidaMaiorQueQuantidadeAtualException();
            }
        }

        public void adicionarOuIncrementarItemInventario(ITEM_INVENTARIODTO item, int quantidade) {
            var itemCerto = pegarITEM_INVENTARIOCerto(item);
            if(this.Inventario.ContainsKey(itemCerto)) {
                this.Inventario[itemCerto] += quantidade;
            } else {
                this.Inventario.Add(new KeyValuePair<ITEM_INVENTARIO, int>(itemCerto, quantidade));
            }
        }
    }

    [JsonConverter(typeof(JsonStringEnumConverter))]
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