using System.Threading.Tasks;
using Resistence.Entities.DTOs;

namespace Resistence.Interfaces.UseCases.Interfaces
{
    public interface IRebeldesUseCase
    {
        Task<object> BuscarRebeldes();
        Task<bool> AdicionarRebelde(AdicionarRebeldeDTO rebelde);
        bool AtualizarLocalizacao(LocalizacaoDTO novaLocalizacao, int codigoRebelde);
    }
}