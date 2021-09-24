using System.Collections.Generic;

namespace Resistence.Entities.DTOs
{
    public class NegociacaoDto
    {

        public int codigoRebeldeUm {get;set;}
        public int codigoRebeldeDois {get;set;}

        public IDictionary<ITEM_INVENTARIODTO, int> itensDeTrocaRebeldeUm {get;set;}

        public IDictionary<ITEM_INVENTARIODTO, int> itensDeTrocaRebeldeDois {get;set;}
    }
}