using System.Collections.Generic;

namespace Resistence.Entities.DTOs
{
    public class NegociacaoDTO
    {

        public int codigoRebeldeUm {get;set;}
        public int codigoRebeldeDois {get;set;}

        public IDictionary<ItemInventarioDTO, int> itensDeTrocaRebeldeUm {get;set;}

        public IDictionary<ItemInventarioDTO, int> itensDeTrocaRebeldeDois {get;set;}
    }
}