using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Pard.Application.Common.Interfaces;
using Pard.Infrastructure.Identity;
using Pard.Persistence.Contexts;

namespace Pard.Persistence
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<RecordsContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("SqlServerExpressRecords")));
            services.AddDbContext<IdentityContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("SqlServerExpressIdentity")));

            services.AddScoped<IRecordsContext>(provider => provider.GetService<RecordsContext>());

            return services;
        }
    }
}