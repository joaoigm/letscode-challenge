using System.Threading.Tasks;

namespace Resistence.Interfaces.UseCases.Interfaces
{
    public interface IReportarUseCase
    {
        Task<bool> Reportar(int codigoRebelde);
    }
}