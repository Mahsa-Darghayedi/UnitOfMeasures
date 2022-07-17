using System.Reflection;
using UnitOfMeasures.Infrastructure.Persistents.Extensions;

namespace UnitOfMeasures.Application.Extensions
{
    public static class ServicesConfigExtensions
    {
        public static IServiceCollection AddServiceConfigs(this IServiceCollection services, IConfiguration configuration)
        {

            var assembly = Assembly.GetExecutingAssembly();
            services.AddControllers();
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();
            services.AddApplicationDbContext(configuration.GetConnectionString("MeasureDBConnectionString"));
            services.AddAutoMapper(assembly);
            return services;
        }
    }
}
