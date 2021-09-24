using System.Threading.Tasks;
using Resistence.Entities.DTOs;

namespace Resistence.Interfaces.UseCases.Interfaces
{
    public interface INegociacaoUseCase
    {
        Task<bool> Negociar(NegociacaoDto negociacao);
    }
}