using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using umvel.challenge.domain.Contexts;
using umvel.challenge.infraestructure.Persistence.Contexts;

namespace umvel.challenge.infraestructure
{
    public static class DependencyInjections
    {
        public static IServiceCollection AddInfraestructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<UmvelChallengeDbContext>(options =>
                options.UseSqlServer(
                    configuration.GetConnectionString("umvel"),
                    b => b.MigrationsAssembly(typeof(UmvelChallengeDbContext).Assembly.FullName)));

            services.AddScoped<IUmvelChallengeDbContext>(provider => provider.GetService<UmvelChallengeDbContext>());

            return services;
        }
    }
}
