using UnitOfMeasures.Infrastructure.Persistents.DBContext;
using Microsoft.EntityFrameworkCore;
namespace UnitOfMeasures.Infrastructure.Persistents.Extensions
{
    public static class MeasureDBContextService
    {
        public static IServiceCollection AddApplicationDbContext(this IServiceCollection services, string connectionString)
        {
            services.AddDbContext<MeasureDBContext>(options => options.UseSqlServer(connectionString));
            return services;
        }
    }
}
