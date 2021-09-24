using Resistence.Entities.DTOs;

namespace Resistence.Interfaces.UseCases.Interfaces
{
    public interface INegociacaoUseCase
    {
        bool Negociar(NegociacaoDTO negociacao);
    }
}