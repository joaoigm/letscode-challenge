using System.Threading.Tasks;
using Resistence.Entities;
using Resistence.Interfaces.UseCases.Interfaces;
using Resistence.Middleware;

namespace Resistence.UseCases
{
    public class ReportarUseCase : IReportarUseCase
    {
        private readonly EFContext _context;

        public ReportarUseCase(EFContext context)
        {
            _context = context;
        }
        public async Task<bool> Reportar(int codigoRebelde)
        {
            Rebelde rebelde = await _context.Rebeldes.FindAsync(codigoRebelde);
            rebelde.IndicacaoTraidor += 1;

            if(rebelde.IndicacaoTraidor >= 3) {
                rebelde.Traidor = true;
            }

            _context.Rebeldes.Update(rebelde);
            await _context.SaveChangesAsync();

            return true;
        }
    }
}