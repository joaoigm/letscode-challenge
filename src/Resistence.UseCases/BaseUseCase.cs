using Microsoft.EntityFrameworkCore;
using Resistence.Middleware;

namespace Resistence.UseCases
{
    public abstract class BaseUseCase {
        
        static int countTimesToMigrate = 0;
        
        public BaseUseCase(EFContext context) {
            context.Database.EnsureCreated();
            // if(countTimesToMigrate == 0) {
            //     context.Database.Migrate();
            //     countTimesToMigrate++;
            // }
        }
    }
}