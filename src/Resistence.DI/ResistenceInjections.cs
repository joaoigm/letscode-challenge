using Microsoft.EntityFrameworkCore;
using Resistence.Interfaces.UseCases.Interfaces;
using Resistence.Middleware;
using Resistence.UseCases;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class ResistenceInjections
    {
        public static IServiceCollection AddResistenceInjections(this IServiceCollection services)
        {
            services.AddDbContext<EFContext>(options => {
                options.UseSqlite("Data Source=:memory:");
            });
            
            services.AddTransient<IRebeldesUseCase, RebeldesUseCase>();

            return services;            
        }
    }
}