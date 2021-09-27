using System.Threading.Tasks;
using Resistence.Entities.Results;

namespace Resistence.Interfaces.UseCases.Interfaces
{
    public interface IRelatoriosUseCase
    {
        

        Task<PercentualResult> RelatorioPercentualRebeldes();
        Task<PercentualResult> RelatorioPercentualTraidores();
        Task<PercentualQuantidadeItensPorRebeldeResult> RelatorioMediaItensPorRebelde();
        Task<QuantidadePontosPerdidosResult> RelatorioPontosPerdidosPorTraidores();
    }    
}