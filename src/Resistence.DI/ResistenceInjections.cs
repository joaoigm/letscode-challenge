using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Resistence.Interfaces.UseCases.Interfaces;
using Resistence.Middleware;
using Resistence.UseCases;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class ResistenceInjections
    {
        public static IServiceCollection AddResistenceInjections(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<EFContext>(options => {
                options.UseSqlServer(configuration["DATABASE_CONNECTIONSTRING"]);
            });
            
            services.AddTransient<IRebeldesUseCase, RebeldesUseCase>();
            services.AddTransient<INegociacaoUseCase, NegociacaoUseCase>();
            services.AddTransient<IRelatoriosUseCase, RelatoriosUseCase>();
            services.AddTransient<IReportarUseCase, ReportarUseCase>();

            return services;            
        }
    }
}