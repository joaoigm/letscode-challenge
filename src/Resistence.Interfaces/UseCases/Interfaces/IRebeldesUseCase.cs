using System.Threading.Tasks;
using Resistence.Entities.DTOs;
using Resistence.Entities.Results;

namespace Resistence.Interfaces.UseCases.Interfaces
{
    public interface IRebeldesUseCase
    {
        Task<AdicionarRebeldeResult> AdicionarRebelde(AdicionarRebeldeDto rebelde);
        Task<AtualizarLocalizacaoResult> AtualizarLocalizacao(LocalizacaoDto novaLocalizacao, int codigoRebelde);
        Task<object> Todos();
    }
}