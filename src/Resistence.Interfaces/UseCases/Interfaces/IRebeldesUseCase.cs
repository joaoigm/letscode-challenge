using System.Threading.Tasks;
using Resistence.Entities.DTOs;
using Resistence.Entities.Results;

namespace Resistence.Interfaces.UseCases.Interfaces
{
    public interface IRebeldesUseCase
    {
        Task<AdicionarRebeldeResult> AdicionarRebelde(AdicionarRebeldeDTO rebelde);
        Task<AtualizarLocalizacaoResult> AtualizarLocalizacao(LocalizacaoDTO novaLocalizacao, int codigoRebelde);
    }
}