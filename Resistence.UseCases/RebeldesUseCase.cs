using System.Threading.Tasks;
using Resistence.Entities.DTOs;
using Resistence.Interfaces.UseCases.Interfaces;
using Resistence.Middleware;

namespace Resistence.UseCases
{
    public class RebeldesUseCase : IRebeldesUseCase
    {

        private readonly EFContext _context;

        public RebeldesUseCase(EFContext context)
        {
            _context = context;
        }

        public RebeldesUseCase() { }
        public Task<bool> AdicionarRebelde(AdicionarRebeldeDTO rebelde)
        {
            throw new System.NotImplementedException();
        }

        public bool AtualizarLocalizacao(LocalizacaoDTO novaLocalizacao, int codigoRebelde)
        {
            throw new System.NotImplementedException();
        }

        public Task<object> BuscarRebeldes()
        {
            throw new System.NotImplementedException();
        }
    }
}