using System.Threading.Tasks;
using Resistence.Entities;
using Resistence.Entities.DTOs;
using Resistence.Entities.Exceptions;
using Resistence.Interfaces.UseCases.Interfaces;
using Resistence.Middleware;

namespace Resistence.UseCases
{
    public class NegociacaoUseCase : INegociacaoUseCase
    {
        private readonly EFContext _context; 

        public NegociacaoUseCase(EFContext context) {
            _context = context;
        }
        public async Task<bool> Negociar(NegociacaoDto negociacao)
        {
            Rebelde rebeldeUm = await _context.Rebeldes.FindAsync(negociacao.codigoRebeldeUm);
            Rebelde rebeldeDois = await _context.Rebeldes.FindAsync(negociacao.codigoRebeldeDois);
            
            if(rebeldeUm == null || rebeldeDois == null) {
                throw new RebeldeNaoEncontradoException();
            }

            return true;
        }
    }
}